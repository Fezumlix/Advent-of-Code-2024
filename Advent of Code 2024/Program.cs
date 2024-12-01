using System.Diagnostics;

namespace Advent_of_Code_2024;

class Program
{
    static void Main(string[] args)
    {
        // run todays method
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Day1(true);
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
}