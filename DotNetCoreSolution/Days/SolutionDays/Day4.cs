using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day4 : BaseDay
    {
        public static List<string> dayInput;

        public override string Run()
        {
            var p1Result = Part1().Result;
            var p2Result = Part2(p1Result);
            return string.Format("Part1: {0}, Part2:{1}", p1Result.Count, p2Result);
        }

        private async Task<List<string>> Part1()
        {
            using (AmazonS3Client s3Client = new AmazonS3Client())
            {
                var getInput = new GetInputFromS3(s3Client);
                dayInput = await getInput.GetDayInput(4);
            }

            var validPassports = new List<string>();
            var individualPassports = ProcessPassports(dayInput);

            foreach (var passport in individualPassports)
            {
                if (passport.Contains("byr:") && passport.Contains("iyr:") && passport.Contains("eyr:"))
                {
                    if (passport.Contains("hgt:") && passport.Contains("hcl:") && passport.Contains("ecl:") && passport.Contains("pid"))
                    {
                        validPassports.Add(passport);
                    }
                }
            }

            return validPassports;
        }

        private int Part2(List<string> startingPassports)
        {
            var validPassports = 0;
            var validPassDict = new List<Dictionary<string, string>>();

            foreach(var passport in startingPassports)
            {
                var passportArray = passport.Split(" ");
                var test = passportArray.Select(x => x.Split(':'))
                    .ToDictionary(x => x[0], x => x[1]);

                Regex hairColorRegex = new Regex(@"#([0-9]|[a-f]){6}");
                Regex passportIdRegex = new Regex(@"\d{9}");
                var eyeColors = new string[]
                {
                    "amb",
                    "blu",
                    "brn",
                    "gry",
                    "grn",
                    "hzl",
                    "oth"
                };

                int.TryParse(test["byr"], out var birthYear);
                if(birthYear < 1920 || birthYear > 2002)
                {
                    continue;
                }

                int.TryParse(test["iyr"], out var issueYear);
                if (issueYear < 2010 || issueYear > 2020)
                {
                    continue;
                }

                int.TryParse(test["eyr"], out var experationYear);
                if (experationYear < 2020 || experationYear > 2030)
                {
                    continue;
                }

                if(test["hgt"].Contains("cm"))
                {
                    var heightString = test["hgt"].Split("cm");
                    int.TryParse(heightString[0], out var heightCM);
                    if (heightCM < 150 || heightCM > 193)
                    {
                        continue;
                    }
                } else if(test["hgt"].Contains("in"))
                {
                    var heightString = test["hgt"].Split("in");
                    int.TryParse(heightString[0], out var heightIn);
                    if (heightIn < 59 || heightIn > 76)
                    {
                        continue;
                    }
                } else
                {
                    continue;
                }

                if(!hairColorRegex.IsMatch(test["hcl"]))
                {
                    continue;
                }

                if (!(passportIdRegex.IsMatch(test["pid"]) && test["pid"].Length == 9))
                {
                    continue;
                }

                if (!eyeColors.Any(test["ecl"].Contains)) {
                    continue;
                }

                validPassports++;
            }

            return validPassports;
        }

        private static bool FindBlank(string toCheck)
        {
            return string.IsNullOrEmpty(toCheck);
        }

        private List<string> ProcessPassports(List<string> batchFile)
        {
            var returnPassports = new List<string>();
            Predicate<string> findBlank = FindBlank;

            while (batchFile.FindIndex(findBlank) > 0)
            {
                var blankLineIndex = batchFile.FindIndex(findBlank);
                var extractedPassport = batchFile.GetRange(0, blankLineIndex);
                var passportLine = string.Join(" ", extractedPassport);
                returnPassports.Add(passportLine);
                batchFile.RemoveRange(0, blankLineIndex + 1);
            }

            var lastLine = string.Join(" ", batchFile);
            returnPassports.Add(lastLine);

            return returnPassports;
        }
    }
}
