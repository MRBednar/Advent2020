using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day13 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {
            var p1Return = Part1().Result;
            return string.Format(p1Return);
        }

        private async Task<string> Part1 ()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(13);
            }

            var earleistDepature = int.Parse(dayInput[0]);
            var busStringList = dayInput[1].Split(',');
            var validBusStringList = busStringList.Where(busString => busString != "x");
            var validBusList = validBusStringList.Select(busString => int.Parse(busString)).ToList();
            var busWaitTime = new List<KeyValuePair<int, int>>();
            foreach(var validBus in validBusList)
            {
                var remander = earleistDepature % validBus;
                var departTime = earleistDepature + (validBus - remander);
                busWaitTime.Add(new KeyValuePair<int,int>(departTime, validBus));
            }

            var earliestBusList = busWaitTime.OrderBy(x => x.Key).ToList();

            var earliestBus = earliestBusList.FirstOrDefault();

            var returnCode = (earliestBus.Key - earleistDepature) * earliestBus.Value;
            return string.Format("Earliest Depart Time: {0}, Earliest Bus Number: {1}, Code: {2}", earliestBus.Key, earliestBus.Value, returnCode);
        }

        private async Task Part2()
        {

        }
    }
}
