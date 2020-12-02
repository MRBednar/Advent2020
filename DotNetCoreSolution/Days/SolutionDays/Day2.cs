using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day2 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {
            var p1Result = Part1().Result;
            return string.Format("Part1: {0}", p1Result);
        }

        private async Task<string> Part1 ()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(2);
            }

            var validPWCount = 0;
            var part2Count = 0;
            foreach(string inputRow in dayInput)
            {
                var policyAndPW = inputRow.Split(':');
                var potentialPW = policyAndPW.Last();
                var countAndLetter = policyAndPW.First().Split(' ');
                var letterCheck = countAndLetter.Last();
                var letterChar = letterCheck.FirstOrDefault();
                var letterCount = potentialPW.Count(x => x == letterChar);
                var minAndMax = countAndLetter.First().Split('-');
                var min = int.Parse(minAndMax.First());
                var max = int.Parse(minAndMax.Last());

                if (letterCount >= min)
                {
                    if (letterCount <= max)
                    {
                        validPWCount++;
                    }
                }


                if (potentialPW[min] == letterChar)
                {
                    if (potentialPW[max] == letterChar)
                    {
                        continue;
                    } else
                    {
                        part2Count++;
                    }
                } else
                {
                    if (potentialPW[max] == letterChar)
                    {
                        part2Count++;
                    }
                }
            }

            return string.Format("Part1: {0}, Part2: {1}", validPWCount.ToString(), part2Count.ToString());
        }
    }
}
