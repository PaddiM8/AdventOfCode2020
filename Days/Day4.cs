using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable enable
class Passport
{
    public BirthYear? BirthYear { get; set; }
    public IssueYear? IssueYear { get; set; }
    public ExpirationYear? ExpirationYear { get; set; }
    public Height? Height { get; set; }
    public HairColor? HairColor { get; set; }
    public EyeColor? EyeColor { get; set; }
    public PassportId? PassportId { get; set; }

    public bool HasValues =>
        BirthYear != null &&
        IssueYear != null &&
        ExpirationYear != null &&
        Height != null &&
        HairColor != null &&
        EyeColor != null &&
        PassportId != null;

    public bool IsValid =>
        HasValues &&
        BirthYear!.IsValid &&
        IssueYear!.IsValid &&
        ExpirationYear!.IsValid &&
        Height!.IsValid &&
        HairColor!.IsValid &&
        EyeColor!.IsValid &&
        PassportId!.IsValid;
}

record BirthYear(int Year)
{
    public bool IsValid => Year >= 1920 && Year <= 2002;
}

record IssueYear(int Year)
{
    public bool IsValid => Year >= 2010 && Year <= 2020;
}

record ExpirationYear(int Year)
{
    public bool IsValid => Year >= 2020 && Year <= 2030;
}

enum HeightUnit
{
    Centimeters,
    Inches,
    Other,
}

record Height(int Value, HeightUnit Unit)
{
    public bool IsValid => Unit switch
    {
        HeightUnit.Centimeters => Value >= 150 && Value <= 193,
        HeightUnit.Inches => Value >= 59 && Value <= 76,
        _ => false
    };

    public static Height Parse(string heightString)
    {
        _ = int.TryParse(heightString[..^2], out int value);

        var unit = heightString[^2..] switch
        {
            "cm" => HeightUnit.Centimeters,
            "in" => HeightUnit.Inches,
            _ => HeightUnit.Other,
        };

        return new Height(value, unit);
    }
}

record HairColor(string Value)
{
    public bool IsValid => new Regex(@"#[0-9a-f]{6}").IsMatch(Value);
}

enum EyeColorType
{
    Unknown,
    Amb,
    Blu,
    Brn,
    Gry,
    Grn,
    Hzl,
    Oth,
}

record EyeColor(EyeColorType Color)
{
    public bool IsValid => Color != EyeColorType.Unknown;

    public static EyeColor Parse(string value)
    {
        Enum.TryParse(typeof(EyeColorType), value, true, out object? eyeColor);

        return new EyeColor((EyeColorType?)eyeColor ?? EyeColorType.Unknown);
    }
}

record PassportId(string Value)
{
    public bool IsValid => Value.Length == 9 && Value.All(char.IsNumber);
}

class Day4 : IDay
{
    private readonly string _input;
    private readonly List<Passport> _passports = new();

    public Day4(string[] lines)
    {
        _input = string.Join("\n", lines);

        var passportStrings = _input.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var passportString in passportStrings)
        {
            var fields = passportString.Split(' ', '\n');
            var passport = new Passport();
            foreach (var field in fields)
            {
                var parts = field.Split(':');
                string key = parts[0];
                string value = parts[1];
                if (key == "byr") passport.BirthYear = new BirthYear(int.Parse(value));
                if (key == "iyr") passport.IssueYear = new IssueYear(int.Parse(value));
                if (key == "eyr") passport.ExpirationYear = new ExpirationYear(int.Parse(value));
                if (key == "hgt") passport.Height = Height.Parse(value);
                if (key == "hcl") passport.HairColor = new HairColor(value);
                if (key == "ecl") passport.EyeColor = EyeColor.Parse(value);
                if (key == "pid") passport.PassportId = new PassportId(value);
            }

            _passports.Add(passport);
        }
    }

    public object Part1()
    {
        int validPassports = 0;
        foreach (var passport in _passports)
        {
            if (passport.HasValues) validPassports++;
        }

        return validPassports;
    }

    public object Part2()
    {
        int validPassports = 0;
        foreach (var passport in _passports)
        {
            if (passport.IsValid) validPassports++;
        }

        return validPassports;
    }
}