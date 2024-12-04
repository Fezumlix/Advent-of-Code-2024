using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2024;

class Program
{
    static void Main(string[] args)
    {
        // run todays method
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Day4(true);
        stopwatch.Stop();
        Console.WriteLine("-----------------------------------\n" +
                          "Runtime: " + stopwatch.Elapsed);
    }

    static string[] ReadInput(int dayNumber)
    {
        return File.ReadAllLines(@"C:\Users\Fezum\RiderProjects\Advent of Code 2024\Advent of Code 2024\Input\Day" + dayNumber + ".txt");
    }

    static void Day1(bool part2)
    {
        var input = ReadInput(1);
        var first = input.Select(i => int.Parse(i.Split("   ")[0])).ToList();
        var second = input.Select(i => int.Parse(i.Split("   ")[1])).ToList();
        first.Sort();
        second.Sort();
        if (!part2) {
            int diff = 0;
            for (int i = 0; i < first.Count; i++)
            {
                diff += Math.Abs(first[i] - second[i]);
            }

            Console.WriteLine(diff);
        }
        else
        {
            int similarity = 0;
            foreach (var i in first)
            {
                similarity += i * second.Count(e => e == i);
            }
            Console.WriteLine(similarity);
        }
    }

    static void Day2(bool part2)
    {
        var input = ReadInput(2);
        int safeLines = 0;
        foreach (var line in input)
        {
            var numbers = line.Split(" ").Select(int.Parse).ToList();
            if (IsSafeLine(numbers))
                safeLines++;
            else if (part2)
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    List<int> temp = new List<int>();
                    temp.AddRange(numbers.Take(i));
                    temp.AddRange(numbers[(i+1)..]);
                    if (IsSafeLine(temp))
                    {
                        safeLines++;
                        break;
                    }
                }
            }
        }
        
        Console.WriteLine(safeLines);

        bool IsSafeLine(List<int> numbers)
        {
            bool increasing = true;
            bool decreasing = true;
            for (int i = 1; i < numbers.Count; i++)
            {
                var last = numbers[i - 1];
                var current = numbers[i];
                if (current >= last || last - current > 3)
                {
                    decreasing = false;
                }

                if (last >= current || current - last > 3)
                {
                    increasing = false;
                }
            }

            return increasing || decreasing;
        }
    }

    static void Day3(bool part2)
    {
        var input = ReadInput(3);
        var fullText = input.Aggregate("", (current, s) => current + (s + Environment.NewLine));
        var regex = new Regex(@"mul\([0-9]{1,3},[0-9]{1,3}\)");
        
        if (part2)
        {
            var doString = "";
            var splittedDonts = fullText.Split("don't()");
            doString += splittedDonts[0];
            foreach (var dontString in splittedDonts[1..])
            {
                doString += dontString.Split("do()")[1..].Aggregate("", (current, s) => current + s);
            }
            fullText = doString;
        }
        
        var matches = regex.Matches(fullText);

        int sum = matches.ToList().Select(s =>
            int.Parse(s.Value[4..].Split(",")[0]) * int.Parse(s.Value[4..].Split(",")[1][..^1])).Sum();

        Console.WriteLine(sum);
    }

    static void Day4(bool part2)
    {
        var input = ReadInput(4);
        var grid = input.Select(i => i.ToCharArray()).ToArray();
        int occurences = 0;
        for (int x = 0; x < grid.Length; x++)
        {
            for (int y = 0; y < grid[x].Length; y++)
            {
                if (!part2) {
                    if (grid[x][y] == 'X')
                    {
                        Vector2Int dir = new Vector2Int();
                        for (int i = 0; i < 8; i++)
                        {
                            dir = i switch
                            {
                                0 => new Vector2Int(1, 0),
                                1 => new Vector2Int(1, -1),
                                2 => new Vector2Int(0, -1),
                                3 => new Vector2Int(-1, -1),
                                4 => new Vector2Int(-1, 0),
                                5 => new Vector2Int(-1, 1),
                                6 => new Vector2Int(0, 1),
                                7 => new Vector2Int(1, 1),
                            };
                            try
                            {
                                if (grid[x + dir.X][y + dir.Y] == 'M' &&
                                    grid[x + dir.X * 2][y + dir.Y * 2] == 'A' &&
                                    grid[x + dir.X * 3][y + dir.Y * 3] == 'S')
                                {
                                    occurences++;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                            }
                        }
                    }
                }

                else {
                    if (grid[x][y] == 'A')
                    {
                        try
                        {
                            if ((grid[x + 1][y + 1] == 'M' &&
                                 grid[x + 1][y - 1] == 'M' &&
                                 grid[x - 1][y - 1] == 'S' &&
                                 grid[x - 1][y + 1] == 'S') ||
                                (grid[x + 1][y + 1] == 'M' &&
                                 grid[x + 1][y - 1] == 'S' &&
                                 grid[x - 1][y - 1] == 'S' &&
                                 grid[x - 1][y + 1] == 'M') ||
                                (grid[x + 1][y + 1] == 'S' &&
                                 grid[x + 1][y - 1] == 'S' &&
                                 grid[x - 1][y - 1] == 'M' &&
                                 grid[x - 1][y + 1] == 'M') ||
                                (grid[x + 1][y + 1] == 'S' &&
                                 grid[x + 1][y - 1] == 'M' &&
                                 grid[x - 1][y - 1] == 'M' &&
                                 grid[x - 1][y + 1] == 'S'))
                            {
                                occurences++;
                            }
                        }
                        catch (IndexOutOfRangeException) { }
                    }
                }
            }
        }
        Console.WriteLine(occurences);
    }
}

public class Vector2Int
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2Int()
    {
        X = 0;
        Y = 0;
    }
}