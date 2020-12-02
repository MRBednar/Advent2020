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

            return string.Format("Not Yet Implemented");
        }

        private async Task Part1 ()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(1);
            }
        }

        private async Task Part2()
        {

        }
    }
}
