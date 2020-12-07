using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day7 : BaseDay
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
                dayInput = await getInput.GetDayInput(7);
            }

            var colorDict = new Dictionary<string, string>();

            foreach(var inputLine in dayInput)
            {
                var lineSplit = inputLine.Split(" contain ");
                var containSplit = lineSplit[0].Split("bags ");
                colorDict.Add(containSplit[0], lineSplit[1]);
            }

            var colorHold = new HashSet<string>();

            foreach(var bagKV in colorDict)
            {
                if (bagKV.Value.Contains("shiny gold"))
                {
                    colorHold.Add(bagKV.Key.Remove(bagKV.Key.Length - 1));
                }

            }

            for(var i = 0; i < colorHold.Count; i++)
            {
                var colorReturn = ProcessColors(colorDict, colorHold.ElementAt(i));
                foreach(var colorR in colorReturn)
                {
                    colorHold.Add(colorR);
                }
            }

            return string.Format("Part1: {0}, Part2: {1}", colorHold.Count, Part2(colorDict));
        }

        private int Part2(Dictionary<string, string> colorDictonary)
        {
            var goldBags = 0;

            colorDictonary.TryGetValue("shiny gold bags", out var goldCointains);

            var goldArray = goldCointains.Split(',');

            var goldBagContains = new Dictionary<string, int>();

            foreach(var containsBags in goldArray)
            {
                var bagColorAr = containsBags.Trim().Split(" bag");
                var bagColorNum = bagColorAr[0];
                var bagColor = bagColorNum.Substring(2);
                var countString = bagColorNum.Remove(1);
                var bagCount = int.Parse(countString);

                goldBagContains.Add(bagColor, bagCount);
            }

            for(var i = 0; i < goldBagContains.Count; i++)
            {
                goldBags += ProcessInternalBags(goldBagContains.ElementAt(i), colorDictonary);
            }

            return goldBags;
        }

        private HashSet<string> ProcessColors(Dictionary<string, string> bagDict, string colorToCheck)
        {
            var colorsToReturn = new HashSet<string>();
            foreach(var bagKV in bagDict)
            {
                if (bagKV.Value.Contains(colorToCheck))
                {
                    colorsToReturn.Add(bagKV.Key.Remove(bagKV.Key.Length - 1));
                }
            }
            return colorsToReturn;
        }

        private int ProcessInternalBags (KeyValuePair<string, int> colorCount, Dictionary<string, string> bagDict)
        {
            var bagContains = new Dictionary<string, int>();

            var internalBagCount = 0;

            bagDict.TryGetValue(colorCount.Key + " bags", out var internalBags);

            var internalArray = internalBags.Split(',');

            foreach (var containsBags in internalArray)
            {
                var bagColorAr = containsBags.Trim().Split(" bag");
                var bagColorNum = bagColorAr[0];
                var bagColor = bagColorNum.Substring(2);
                var countString = bagColorNum.Remove(1);
                int.TryParse(countString, out var bagCount);

                if(bagCount > 0)
                {
                    bagContains.Add(bagColor, bagCount * colorCount.Value);
                }
            }

            foreach(var netedBags in bagContains)
            {
                internalBagCount += ProcessInternalBags(netedBags, bagDict);
            }

            internalBagCount += colorCount.Value;

            return internalBagCount;
        }
    }
}
