using Amazon.S3;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day1 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run ()
        {
            var p1Result = Part1().Result;
            var p2Result = Part2();
            return string.Format("Part 1: {0}, Part 2: {1}", p1Result, p2Result);
        }

        private async Task<int> Part1 ()
        {
            var test1 = 0;
            var test2 = 0;
            var multiplyResult = 0;
            var is2020 = false;
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(1);
            }

            while (!is2020)
            {
                foreach (string dayLine in dayInput)
                {
                    if(int.TryParse(dayLine, out test1))
                    {
                        var remander = (2020 - test1).ToString();
                        is2020 = dayInput.Contains(remander);
                        if(is2020)
                        {
                            test2 = int.Parse(remander);
                            break;
                        }
                    }
                }
            }

            multiplyResult = test1 * test2;
            return multiplyResult;
        }

        private int Part2()
        {
            var test1 = 0;
            var test2 = 0;
            var test3 = 0;
            var is2020 = false;

            while(!is2020)
            {
                for (int i = 0; i < dayInput.Count; i++)
                {
                    var dayLine = dayInput[i];
                    if (int.TryParse(dayLine, out test1))
                    {
                        var remander1 = (2020 - test1);
                        for (int x = 1; x + i < dayInput.Count; x++)
                        {
                            var checkLine = dayInput[x + i];
                            if (int.TryParse(checkLine, out test2) && test2 < remander1)
                            {
                                var remander2 = (remander1 - test2).ToString();
                                is2020 = dayInput.Contains(remander2);
                                if(is2020)
                                {
                                    test3 = int.Parse(remander2);
                                    break;
                                }
                            }
                        }
                    }
                    if(is2020)
                    {
                        break;
                    }
                }
            }

            var multiplyResult = test1 * test2 * test3;
            return multiplyResult;
        }
    }
}
