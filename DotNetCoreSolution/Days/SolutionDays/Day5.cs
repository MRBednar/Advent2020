using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day5 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {
            var resultP1 = Part1().Result;
            return string.Format(resultP1);
        }

        private async Task<string> Part1 ()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(5);
            }
            var seatIdList = new List<int>();
            foreach(var boardingPass in dayInput)
            {
                var maxRow = 127;
                var minRow = 0;
                var maxColumn = 7;
                var minColumn = 0;

                foreach(var passKey in boardingPass)
                {
                    switch (passKey)
                    {
                        case 'F':
                            maxRow = maxRow - RowCodeConvert(minRow, maxRow);
                            continue;
                        case 'B':
                            minRow = minRow + RowCodeConvert(minRow, maxRow);
                            continue;
                        case 'L':
                            maxColumn = maxColumn - RowCodeConvert(minColumn, maxColumn);
                            continue;
                        case 'R':
                            minColumn = minColumn + RowCodeConvert(minColumn, maxColumn);
                            continue;
                        default:
                            Console.WriteLine("This is an invalid character.");
                            continue;
                    }
                }
                var seatId = (maxRow * 8) + maxColumn;
                seatIdList.Add(seatId);
            }

            var minSeatId = seatIdList.Min();
            var maxSeatId = seatIdList.Max();
            var missingSeat = 0;

            for(var i = minSeatId; i < maxSeatId; i++)
            {
                if(!seatIdList.Contains(i))
                {
                    missingSeat = i;
                    break;
                }
            }

            var fullResult = string.Format("Part1: {0}, Part2: {1}", maxSeatId, missingSeat);
            return fullResult;
        }

        private int RowCodeConvert(int minNum, int maxNum)
        {
            return ((maxNum - minNum) / 2) + ((maxNum - minNum) % 2);
        }
    }
}
