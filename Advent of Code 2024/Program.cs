using System.Diagnostics;

namespace Advent_of_Code_2024;

class Program
{
    static void Main(string[] args)
    {
        // run todays method
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Day2(true);
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
}