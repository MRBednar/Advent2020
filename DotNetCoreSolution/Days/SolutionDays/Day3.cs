using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day3 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {
            var p1Result = Part1().Result;
            var p2Result = Part2(p1Result);
            return string.Format("Part1: {0}, Part2: {1}", p1Result, p2Result);
        }

        private async Task<int> Part1 ()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(3);
            }

            var treeCount = TreeChecker(3, 1);
            return treeCount;
        }

        private long Part2(int run2)
        {
            var run1 = TreeChecker(1, 1);
            long productResult = run1 * run2;
            var run3 = TreeChecker(5, 1);
            productResult = productResult * run3;
            var run4 = TreeChecker(7, 1);
            productResult = productResult * run4;
            var run5 = TreeChecker(1, 2);
            productResult = productResult * run5;
            return productResult;
        }

        private int TreeChecker(int rightMove, int downMove)
        {
            var treeCount = 0;
            var xAxis = 0;
            for (var i = downMove; i <= dayInput.Count - 1; i = i+downMove)
            {
                xAxis = xAxis + rightMove;
                if (xAxis >= 31)
                {
                    xAxis -= 31;
                }
                var test = dayInput[i][xAxis];
                if (dayInput[i][xAxis] == '#')
                {
                    treeCount++;
                }
                continue;
            }

            return treeCount;
        }
    }
}
