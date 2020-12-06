using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day6 : BaseDay
    {
        public static List<string> dayInput;
        public static List<string> dayInput2;

        public Predicate<string> findBlank = FindBlank;

        public override string Run()
        {

            var resultP1 = Part1().Result;
            var resultP2 = Part2();
            return string.Format(resultP1 + resultP2);
        }

        private async Task<string> Part1 ()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(6);
                dayInput2 = await getInput.GetDayInput(6);
            }

            var answerCollection = ProcessAnswers(dayInput);
            var answerSum = 0;
            foreach(var groupAnswer in answerCollection)
            {
                answerSum += groupAnswer.Length;
            }

            return String.Format("Part1: {0}", answerSum);
        }

        private string Part2()
        {
            var allYes = AllYes(dayInput2);

            return String.Format(", Part2: {0}", allYes);

        }

        private List<string> ProcessAnswers(List<string> batchFile)
        {
            var returnAnswers = new List<string>();

            while (batchFile.FindIndex(findBlank) > 0)
            {
                var blankLineIndex = batchFile.FindIndex(findBlank);
                var extractedAnswers = batchFile.GetRange(0, blankLineIndex);
                var answerLine = string.Join("", extractedAnswers);
                var distinctAnswers = answerLine.Trim().ToCharArray().Distinct().OrderBy(x => x);
                returnAnswers.Add(string.Join("", distinctAnswers));
                batchFile.RemoveRange(0, blankLineIndex + 1);
            }

            var lastLine = string.Join("", batchFile);
            var distinctLine = lastLine.Trim().ToCharArray().Distinct().OrderBy(x => x);
            returnAnswers.Add(string.Join("", distinctLine));

            return returnAnswers;
        }

        private int AllYes(List<string> batchFile)
        {
            var allRowsYes = 0;

            while (batchFile.FindIndex(findBlank) > 0)
            {
                var blankLineIndex = batchFile.FindIndex(findBlank);
                var extractedAnswers = batchFile.GetRange(0, blankLineIndex);

                if(extractedAnswers.Count() <= 1)
                {
                    allRowsYes += extractedAnswers[0].Length;
                    batchFile.RemoveRange(0, blankLineIndex + 1);
                    continue;
                }

                foreach(var answerCheck in extractedAnswers[0])
                {
                    var extractedString = string.Join("", extractedAnswers);
                    var charCount = extractedString.Count(x => x == answerCheck);
                    if(charCount == extractedAnswers.Count())
                    {
                        allRowsYes++;
                    }
                }
                batchFile.RemoveRange(0, blankLineIndex + 1);
            }

            if (batchFile.Count() <= 1)
            {
                allRowsYes += batchFile[0].Length;
            }

            foreach (var answerCheck in batchFile[0])
            {
                var extractedString = string.Join("", batchFile);
                var charCount = extractedString.Count(x => x == answerCheck);
                if (charCount == batchFile.Count())
                {
                    allRowsYes++;
                }
            }

            return allRowsYes;
        }

        private static bool FindBlank(string toCheck)
        {
            return string.IsNullOrEmpty(toCheck);
        }
    }
}
