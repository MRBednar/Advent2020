using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day10 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {

            var p1Return = Part1().Result;
            var p2Return = Part2();
            return string.Format(p1Return + p2Return);
        }

        private async Task<string> Part1()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(10);
            }

            var joltages = dayInput.Select(x => int.Parse(x)).ToList();
            joltages.Sort();

            var oneJoltDiff = 0;
            var threeJoltDiff = 0;
            var lastJoltage = 0;
            foreach(var joltRating in joltages)
            {
                var voltDiff = joltRating - lastJoltage;
                lastJoltage = joltRating;

                if (voltDiff == 1)
                {
                    oneJoltDiff++;
                    continue;
                }
                if (voltDiff == 3)
                {
                    threeJoltDiff++;
                    continue;
                }
            }

            var joltResult = oneJoltDiff * (threeJoltDiff+1);


            return string.Format("Part1: {0}", joltResult);
        }

        private string Part2()
        {
            var joltages = dayInput.Select(x => int.Parse(x)).ToList();
            joltages.Add(0);
            var adapterBoost = joltages.Max() + 3;
            joltages.Add(adapterBoost);
            joltages.Sort();
            joltages.Reverse();

            var joltPathDict = new Dictionary<int, long>();
            joltPathDict.Add(adapterBoost, 1);

            foreach(var joltRating in joltages)
            {
                joltPathDict.TryGetValue(joltRating + 1, out long plusOneJolt);
                joltPathDict.TryGetValue(joltRating + 2, out long plusTwoJolt);
                joltPathDict.TryGetValue(joltRating + 3, out long plusThreeJolt);

                joltPathDict.TryAdd(joltRating, plusOneJolt + plusTwoJolt + plusThreeJolt);
            }

            return string.Format(" Condensed options: {0}", joltPathDict[0]); ;
        }
    }
}
