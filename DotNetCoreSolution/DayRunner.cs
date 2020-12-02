using Advent2020.DotNetCoreSolution.Days;
using Advent2020.DotNetCoreSolution.Days.SolutionDays;
using System.Collections.Generic;

namespace Advent2020.DotNetCoreSolution
{
    class DayRunner
    {
        public string RunDay(int day)
        {
            var returnData = dayArgument[day].Run();
            return returnData;
        }

        public static Dictionary<int, IDay>
            dayArgument = new Dictionary<int, IDay>
            {
                {1, new Day1() },
                {2, new Day2() },
                {3, new Day3() },
                {4, new Day4() },
                {5, new Day5() },
                {6, new Day6() },
                {7, new Day7() },
                {8, new Day8() },
                {9, new Day9() },
                {10, new Day10() },
                {11, new Day11() },
                {12, new Day12() },
                {13, new Day13() },
                {14, new Day14() },
                {15, new Day15() },
                {16, new Day16() },
                {17, new Day17() },
                {18, new Day18() },
                {19, new Day19() },
                {20, new Day20() },
                {21, new Day21() },
                {22, new Day22() },
                {23, new Day23() },
                {24, new Day24() },
                {25, new Day25() },
            };
    }
}
