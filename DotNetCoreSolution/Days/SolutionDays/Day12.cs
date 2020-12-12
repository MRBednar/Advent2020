using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day12 : BaseDay
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
                dayInput = await getInput.GetDayInput(12);
            }

            var result1 = ProcessDirections(dayInput);
            var result2 = ProcessDirections2(dayInput);

            return string.Format("Part1: {0}, Part2: {1}", result1, result2);
        }

        private int ProcessDirections (List<string> directionStrings)
        {
            var directionList = new List<KeyValuePair<char, int>>();
            foreach(var dirString in directionStrings)
            {
                var dir = dirString.First();
                int.TryParse(dirString.Substring(1), out var move);
                directionList.Add(new KeyValuePair<char, int>(dir, move));
            }

            var facing = 0;
            var northSouth = 0;
            var eastWest = 0;

            foreach(var direction in directionList)
            {
                switch(direction.Key)
                {
                    case 'N':
                        northSouth += direction.Value;
                        continue;
                    case 'S':
                        northSouth -= direction.Value;
                        continue;
                    case 'E':
                        eastWest += direction.Value;
                        continue;
                    case 'W':
                        eastWest -= direction.Value;
                        continue;
                    case 'F':
                        switch(facing)
                        {
                            case 1:
                                northSouth += direction.Value;
                                continue;
                            case 3:
                                northSouth -= direction.Value;
                                continue;
                            case 0:
                                eastWest += direction.Value;
                                continue;
                            case 2:
                                eastWest -= direction.Value;
                                continue;
                        }
                        continue;
                    case 'L':
                        var lTurn = direction.Value / 90;
                        switch (facing)
                        {
                            case 1:
                                switch (lTurn)
                                {
                                    case 1:
                                        facing = 2;
                                        continue;
                                    case 2:
                                        facing = 3;
                                        continue;
                                    case 3:
                                        facing = 0;
                                        continue;
                                    case 4:
                                        facing = 1;
                                        continue;
                                }
                                continue;
                            case 3:
                                switch (lTurn)
                                {
                                    case 1:
                                        facing = 0;
                                        continue;
                                    case 2:
                                        facing = 1;
                                        continue;
                                    case 3:
                                        facing = 2;
                                        continue;
                                    case 4:
                                        facing = 3;
                                        continue;
                                }
                                continue;
                            case 0:
                                switch (lTurn)
                                {
                                    case 1:
                                        facing = 1;
                                        continue;
                                    case 2:
                                        facing = 2;
                                        continue;
                                    case 3:
                                        facing = 3;
                                        continue;
                                    case 4:
                                        facing = 0;
                                        continue;
                                }
                                continue;
                            case 2:
                                switch (lTurn)
                                {
                                    case 1:
                                        facing = 3;
                                        continue;
                                    case 2:
                                        facing = 0;
                                        continue;
                                    case 3:
                                        facing = 1;
                                        continue;
                                    case 4:
                                        facing = 2;
                                        continue;
                                }
                                continue;
                        }
                        continue;
                    case 'R':
                        var rTurn = direction.Value / 90;
                        switch (facing)
                        {
                            case 1:
                                switch (rTurn)
                                {
                                    case 1:
                                        facing = 0;
                                        continue;
                                    case 2:
                                        facing = 3;
                                        continue;
                                    case 3:
                                        facing = 2;
                                        continue;
                                    case 4:
                                        facing = 1;
                                        continue;
                                }
                                continue;
                            case 3:
                                switch (rTurn)
                                {
                                    case 1:
                                        facing = 2;
                                        continue;
                                    case 2:
                                        facing = 1;
                                        continue;
                                    case 3:
                                        facing = 0;
                                        continue;
                                    case 4:
                                        facing = 3;
                                        continue;
                                }
                                continue;
                            case 0:
                                switch (rTurn)
                                {
                                    case 1:
                                        facing = 3;
                                        continue;
                                    case 2:
                                        facing = 2;
                                        continue;
                                    case 3:
                                        facing = 1;
                                        continue;
                                    case 4:
                                        facing = 0;
                                        continue;
                                }
                                continue;
                            case 2:
                                switch (rTurn)
                                {
                                    case 1:
                                        facing = 1;
                                        continue;
                                    case 2:
                                        facing = 0;
                                        continue;
                                    case 3:
                                        facing = 3;
                                        continue;
                                    case 4:
                                        facing = 2;
                                        continue;
                                }
                                continue;
                        }
                        continue;
                    default:
                        Console.WriteLine("Should Not Be Reached");
                        continue;
                }
            }

            var dist = Math.Abs(eastWest) + Math.Abs(northSouth);
            return dist;
        }

        private int ProcessDirections2(List<string> directionStrings)
        {
            var directionList = new List<KeyValuePair<char, int>>();
            foreach (var dirString in directionStrings)
            {
                var dir = dirString.First();
                int.TryParse(dirString.Substring(1), out var move);
                directionList.Add(new KeyValuePair<char, int>(dir, move));
            }

            var facing = 0;
            var offsetNS = 1;
            var offsetEW = 10;
            var northSouth = 0;
            var eastWest = 0;

            foreach (var direction in directionList)
            {
                switch (direction.Key)
                {
                    case 'N':
                        if(facing == 0 || facing == 1)
                        {
                            offsetNS += direction.Value;
                        } else
                        {
                            offsetNS -= direction.Value;
                        }
                        
                        continue;
                    case 'S':
                        if (facing == 0 || facing == 1)
                        {
                            offsetNS -= direction.Value;
                        }
                        else
                        {
                            offsetNS += direction.Value;
                        }
                        continue;
                    case 'E':
                        if (facing == 0 || facing == 3)
                        {
                            offsetEW += direction.Value;
                        }
                        else
                        {
                            offsetEW -= direction.Value;
                        }
                        continue;
                    case 'W':
                        if (facing == 0 || facing == 3)
                        {
                            offsetEW -= direction.Value;
                        }
                        else
                        {
                            offsetEW += direction.Value;
                        }
                        continue;
                    case 'F':
                        switch (facing)
                        {
                            case 1:
                                northSouth += (direction.Value * offsetNS);
                                eastWest -= (direction.Value * offsetEW);
                                continue;
                            case 3:
                                northSouth -= (direction.Value * offsetNS);
                                eastWest += (direction.Value * offsetEW);
                                continue;
                            case 0:
                                eastWest += (direction.Value * offsetEW);
                                northSouth += (direction.Value * offsetNS);
                                continue;
                            case 2:
                                eastWest -= (direction.Value * offsetEW);
                                northSouth -= (direction.Value * offsetNS);
                                continue;
                        }
                        continue;
                    case 'L':
                        var lTurn = direction.Value / 90;
                        var tempEW = offsetEW;
                        switch (facing)
                        {
                            case 1:
                                switch (lTurn)
                                {
                                    case 1:
                                        facing = 2;
                                        offsetEW = offsetNS;
                                        offsetNS = tempEW;
                                        continue;
                                    case 2:
                                        facing = 3;
                                        continue;
                                    case 3:
                                        facing = 0;
                                        offsetEW = offsetNS;
                                        offsetNS = tempEW;
                                        continue;
                                    case 4:
                                        facing = 1;
                                        continue;
                                }
                                continue;
                            case 3:
                                switch (lTurn)
                                {
                                    case 1:
                                        facing = 0;
                                        offsetEW = offsetNS;
                                        offsetNS = tempEW;
                                        continue;
                                    case 2:
                                        facing = 1;
                                        continue;
                                    case 3:
                                        facing = 2;
                                        offsetEW = offsetNS;
                                        offsetNS = tempEW;
                                        continue;
                                    case 4:
                                        facing = 3;
                                        continue;
                                }
                                continue;
                            case 0:
                                switch (lTurn)
                                {
                                    case 1:
                                        facing = 1;
                                        offsetEW = offsetNS;
                                        offsetNS = tempEW;
                                        continue;
                                    case 2:
                                        facing = 2;
                                        continue;
                                    case 3:
                                        facing = 3;
                                        offsetEW = offsetNS;
                                        offsetNS = tempEW;
                                        continue;
                                    case 4:
                                        facing = 0;
                                        continue;
                                }
                                continue;
                            case 2:
                                switch (lTurn)
                                {
                                    case 1:
                                        facing = 3;
                                        offsetEW = offsetNS;
                                        offsetNS = tempEW;
                                        continue;
                                    case 2:
                                        facing = 0;
                                        continue;
                                    case 3:
                                        facing = 1;
                                        offsetEW = offsetNS;
                                        offsetNS = tempEW;
                                        continue;
                                    case 4:
                                        facing = 2;
                                        continue;
                                }
                                continue;
                        }
                        continue;
                    case 'R':
                        var rTurn = direction.Value / 90;
                        var tempNS = offsetNS;
                        switch (facing)
                        {
                            case 1:
                                switch (rTurn)
                                {
                                    case 1:
                                        facing = 0;
                                        offsetNS = offsetEW;
                                        offsetEW = tempNS;
                                        continue;
                                    case 2:
                                        facing = 3;
                                        continue;
                                    case 3:
                                        facing = 2;
                                        offsetNS = offsetEW;
                                        offsetEW = tempNS;
                                        continue;
                                    case 4:
                                        facing = 1;
                                        continue;
                                }
                                continue;
                            case 3:
                                switch (rTurn)
                                {
                                    case 1:
                                        facing = 2;
                                        offsetNS = offsetEW;
                                        offsetEW = tempNS;
                                        continue;
                                    case 2:
                                        facing = 1;
                                        continue;
                                    case 3:
                                        facing = 0;
                                        offsetNS = offsetEW;
                                        offsetEW = tempNS;
                                        continue;
                                    case 4:
                                        facing = 3;
                                        continue;
                                }
                                continue;
                            case 0:
                                switch (rTurn)
                                {
                                    case 1:
                                        facing = 3;
                                        offsetNS = offsetEW;
                                        offsetEW = tempNS;
                                        continue;
                                    case 2:
                                        facing = 2;
                                        continue;
                                    case 3:
                                        facing = 1;
                                        offsetNS = offsetEW;
                                        offsetEW = tempNS;
                                        continue;
                                    case 4:
                                        facing = 0;
                                        continue;
                                }
                                continue;
                            case 2:
                                switch (rTurn)
                                {
                                    case 1:
                                        facing = 1;
                                        offsetNS = offsetEW;
                                        offsetEW = tempNS;
                                        continue;
                                    case 2:
                                        facing = 0;
                                        continue;
                                    case 3:
                                        facing = 3;
                                        offsetNS = offsetEW;
                                        offsetEW = tempNS;
                                        continue;
                                    case 4:
                                        facing = 2;
                                        continue;
                                }
                                continue;
                        }
                        continue;
                    default:
                        Console.WriteLine("Should Not Be Reached");
                        continue;
                }
            }

            var dist = Math.Abs(eastWest) + Math.Abs(northSouth);
            return dist;
        }
    }
}
