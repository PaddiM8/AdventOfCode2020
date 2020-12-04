using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable enable
class Passport
{
    public int? BirthYear { get; set; }
    public int? IssueYear { get; set; }
    public int? ExpirationYear { get; set; }
    public Height? Height { get; set; }
    public string? HairColor { get; set; }
    public EyeColor? EyeColor { get; set; }
    public string? PassportId { get; set; }

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
        BirthYear >= 1920 && BirthYear <= 2002 &&
        IssueYear >= 2010 && IssueYear <= 2020 &&
        ExpirationYear >= 2020 && ExpirationYear <= 2030 &&
        Height!.IsValid &&
        new Regex("#[0-9a-f]{6}").IsMatch(HairColor!) &&
        EyeColor!.Value != global::EyeColor.Unknown &&
        PassportId!.Length == 9 && PassportId.All(char.IsNumber);
}

enum HeightUnit
{
    Other,
    Centimeters,
    Inches,
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

enum EyeColor
{
    Unknown,
    Amber,
    Blue,
    Brown,
    Gray,
    Green,
    Hazel,
    Other,
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
                if (key == "byr") passport.BirthYear = ParseInt(value);
                if (key == "iyr") passport.IssueYear = ParseInt(value);
                if (key == "eyr") passport.ExpirationYear = ParseInt(value);
                if (key == "hgt") passport.Height = Height.Parse(value);
                if (key == "hcl") passport.HairColor = value;
                if (key == "ecl") passport.EyeColor = ParseEyeColor(value);
                if (key == "pid") passport.PassportId = value;
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

    private static int? ParseInt(string input)
    {
        bool success = int.TryParse(input, out int output);

        return success ? output : null;
    }

    private static EyeColor? ParseEyeColor(string input)
    {
        return input switch
        {
            "amb" => EyeColor.Amber,
            "blu" => EyeColor.Blue,
            "brn" => EyeColor.Brown,
            "gry" => EyeColor.Gray,
            "grn" => EyeColor.Green,
            "hzl" => EyeColor.Hazel,
            "oth" => EyeColor.Other,
            _ => EyeColor.Unknown
        };
    }
}