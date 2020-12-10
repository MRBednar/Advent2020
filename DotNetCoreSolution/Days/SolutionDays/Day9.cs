using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day9 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {

            var p1Return = Part1().Result;
            return string.Format(p1Return);
        }

        private async Task<string> Part1()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(9);
            }

            var doubleInput = dayInput.Select(x => double.Parse(x));

            var errorBit = ProcessXmasEncodeForError(doubleInput.ToList());

            var encryptionWeakness = Part2(doubleInput, errorBit);

            return string.Format("Part1: {0}, Part2 {1}", errorBit, encryptionWeakness);
        }

        private double Part2(IEnumerable<double> doubleInput, double errorBit)
        {
            double encryptWeakness = 0;

            var i = 0;
            while(encryptWeakness == 0 && i < doubleInput.Count())
            {
                var toSum = new List<double>();

                var c = i;
                while(toSum.Sum() < errorBit && encryptWeakness == 0)
                {
                    toSum.Add(doubleInput.ElementAt(c));
                    if (toSum.Sum() == errorBit)
                    {
                        encryptWeakness = toSum.Max() + toSum.Min();
                    }
                    c++;
                }
                i++;
            }


            return encryptWeakness;
        }

        private double ProcessXmasEncodeForError(IEnumerable<double> encodedInput)
        {
            var queueSize = 25;
            var checkQueue = new Queue<double>(queueSize);
            double errorNumber = 0;

            for(var i = 0; i < queueSize; i++)
            {
                checkQueue.Enqueue(encodedInput.ElementAt(i));
            }

            for(var c = queueSize; c < encodedInput.Count(); c++)
            {
                if(!CheckInt(checkQueue, encodedInput.ElementAt(c)))
                {
                    errorNumber = encodedInput.ElementAt(c);
                    break;
                }
                checkQueue.Dequeue();
                checkQueue.Enqueue(encodedInput.ElementAt(c));
            }


            return errorNumber;
        }

        private bool CheckInt (Queue<double> checkQueue, double numberTocheck)
        {
            var checkArray = checkQueue.ToArray();
            var canAdd = false;
            var iCount = 0;
            while(!canAdd && iCount < checkQueue.Count())
            {
                var checker = checkArray.ElementAt(iCount);
                var containsCheck = numberTocheck - checker;
                if (!(containsCheck == checker))
                {
                    canAdd = checkQueue.Contains(containsCheck);
                }
                iCount++;
            }

            return canAdd;
        }
    }
}
