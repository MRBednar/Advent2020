using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day8 : BaseDay
    {
        public static List<string> dayInput;

        public HashSet<int> potentialRows;

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
                dayInput = await getInput.GetDayInput(8);
            }

            potentialRows = new HashSet<int>();

            var accumulation = RetriveAccumulation(dayInput);

            var part2Result = Part2(potentialRows);

            return string.Format("Part1: {0}, {1}", accumulation.Key, part2Result);
        }

        private string Part2(HashSet<int> possibleRows)
        {
            var reverseList = possibleRows.Reverse();
            var finishTest = false;
            var itteration = 0;
            var finalAcc = 0;
            while(finishTest == false)
            {
                var rowToTest = reverseList.ElementAt(itteration);
                var tempRow = dayInput[rowToTest];
                var tempSplit = tempRow.Split(" ");
                if (tempSplit[0] == "jmp")
                {
                    dayInput[rowToTest] = "nop " + tempSplit[1];
                }
                else if (tempSplit[0] == "nop")
                {
                    dayInput[rowToTest] = "jmp " + tempSplit[1];
                }

                var testRun = RetriveAccumulation(dayInput);

                if (testRun.Value == true)
                {
                    finalAcc = testRun.Key;
                    finishTest = testRun.Value;
                }

                dayInput[rowToTest] = tempRow;
                itteration++;
            }
            return string.Format("Part2: {0}", finalAcc);
        }

        private KeyValuePair<int, bool> RetriveAccumulation(List<string> commandList)
        {
            var accumulation = 0;
            var rowCheck = new HashSet<int>();
            var finishRun = true;
            for (var i = 0; i < commandList.Count; i++)
            {
                if (!rowCheck.Add(i))
                {
                    finishRun = false;
                    break;
                }
                var commandSplit = commandList[i].Split(" ");
                switch (commandSplit[0])
                {
                    case "jmp":
                        potentialRows.Add(i);
                        var intImput = commandSplit[1].Substring(1);
                        if (commandSplit[1].First() == '+')
                        {
                            i += int.Parse(intImput) - 1;
                        }
                        else
                        {
                            i -= int.Parse(intImput) + 1;
                        }
                        continue;
                    case "acc":
                        var accImput = commandSplit[1].Substring(1);
                        if (commandSplit[1].First() == '+')
                        {
                            accumulation += int.Parse(accImput);
                        }
                        else
                        {
                            accumulation -= int.Parse(accImput);
                        }
                        continue;
                    default:
                        potentialRows.Add(i);
                        continue;
                }
            }

            var result = new KeyValuePair<int, bool>(accumulation, finishRun);
            return result;
        }
    }
}