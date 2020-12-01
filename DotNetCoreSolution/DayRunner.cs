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
            };
    }
}
