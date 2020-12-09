using System;
using System.Collections.Generic;
using System.Linq;

enum InstructionType
{
    Acc,
    Jmp,
    Nop,
}

record Instruction(InstructionType Type, int Value);

class Day8 : IDay
{
    private readonly List<Instruction> _instructions;

    public Day8(string[] lines)
    {
        _instructions = lines.Select(line =>
        {
            var parts = line.Split(' ');
            InstructionType type = parts[0] switch
            {
                "acc" => InstructionType.Acc,
                "jmp" => InstructionType.Jmp,
                _ => InstructionType.Nop,
            };

            return new Instruction(type, int.Parse(parts[1]));
        }).ToList();
    }

    public object Part1()
    {
        // This one is supposed to fail,
        // so there is no need to check if it succeeded.
        // Use the value anyway.
        TryExecute(out int acc);

        return acc;
    }

    public object Part2()
    {
        for (int i = 0; i < _instructions.Count; i++)
        {
            if (_instructions[i].Type == InstructionType.Acc) continue;
            ChangeInstruction(i);
            if (TryExecute(out int acc)) return acc;
            else ChangeInstruction(i); // Change it back, since it didn't to the trick.
        }

        return 0;
    }

    private bool TryExecute(out int acc)
    {
        acc = 0;
        var executedIndexes = new HashSet<int>();
        for (int i = 0; i < _instructions.Count;)
        {
            if (i < 0 || i >= _instructions.Count) return false;
            var instruction = _instructions[i];
            if (executedIndexes.Contains(i)) return false;
            executedIndexes.Add(i);

            if (instruction.Type == InstructionType.Acc) acc += instruction.Value;
            if (instruction.Type == InstructionType.Jmp) i += instruction.Value;
            else i++;
        }

        return true;
    }


    private void ChangeInstruction(int index)
    {
        var instruction = _instructions[index];
        _instructions[index] = instruction with
        {
            Type = instruction.Type switch
            {
                InstructionType.Jmp => InstructionType.Nop,
                InstructionType.Nop => InstructionType.Jmp,
                _ => instruction.Type,
            }
        };
    }
}