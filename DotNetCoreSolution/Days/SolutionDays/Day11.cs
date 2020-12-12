using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.DotNetCoreSolution.Days.SolutionDays
{
    public class Day11 : BaseDay
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
                dayInput = await getInput.GetDayInput(11);
            }

            var seatMultiArray = new List<List<char>>();

            for(var i = 0; i < dayInput.Count(); i++)
            {
                seatMultiArray.Add(dayInput[i].Select(x => x).ToList());
            }

            var settledSeats = ProcessSeatingChart(seatMultiArray);
            var part1OccupiedCount = OccupiedSeatCount(settledSeats);

            var part2Seats = ProcessSeatingChart2(seatMultiArray);
            var part2OccupiedSeats = OccupiedSeatCount(part2Seats);

            return string.Format("Part1: {0}, Part2: {1}", part1OccupiedCount, part2OccupiedSeats);
        }

        private int OccupiedSeatCount(List<List<char>> seatingChart)
        {
            var occupiedSeats = 0;

            for (var x = 0; x < seatingChart.Count; x++)
            {
                for (var y = 0; y < seatingChart[x].Count; y++)
                {
                    if (seatingChart[x][y] == '#')
                    {
                        occupiedSeats++;
                    }
                }
            }

            return occupiedSeats;
        }

        private List<List<char>> ProcessSeatingChart (List<List<char>> seatingChart)
        {
            var noSeatsChanged = false;

            while (!noSeatsChanged)
            {
                var changedOccupied = 0;
                var tempChart = seatingChart.ConvertAll(x => new List<char>(x));
               
                for (var y = 0; y < seatingChart.Count; y++)
                {
                    for (var x = 0; x < seatingChart[y].Count; x++)
                    {
                        if (seatingChart[y][x] == '#' || seatingChart[y][x] == 'L')
                        {
                            var occupiedAdjacentSeats = SeatsAdjacent(y, x, seatingChart);

                            if (occupiedAdjacentSeats == 0 && seatingChart[y][x] == 'L')
                            {
                                tempChart[y][x] = '#';
                                changedOccupied++;
                            }

                            if (occupiedAdjacentSeats >= 4 && seatingChart[y][x] == '#')
                            {
                                tempChart[y][x] = 'L';
                                changedOccupied++;
                            }
                        }
                    }
                }

                if(changedOccupied == 0)
                {
                    noSeatsChanged = true;
                }

                seatingChart = tempChart;
            }            

            return seatingChart;
        }

        private int SeatsAdjacent(int yPos, int xPos, List<List<char>> seatingChart)
        {
            var occupiedAdjacent = 0;

            for(var y = -1; y < 2; y++)
            {
                for(var x = -1; x < 2; x++)
                {
                    var xCheck = xPos + x;
                    var yCheck = yPos + y;
                    if (xCheck >= 0 && yCheck >= 0 && xCheck < seatingChart[yPos].Count && yCheck < seatingChart.Count)
                    {
                        if(xCheck == xPos && yCheck == yPos)
                        {
                            continue;
                        }
                        if(seatingChart[yCheck][xCheck] == '#')
                        {
                            occupiedAdjacent++;
                        }
                    }
                }
            }

            return occupiedAdjacent;
        }

        private List<List<char>> ProcessSeatingChart2(List<List<char>> seatingChart)
        {
            var noSeatsChanged = false;

            while (!noSeatsChanged)
            {
                var changedOccupied = 0;
                var tempChart = seatingChart.ConvertAll(x => new List<char>(x));

                for (var y = 0; y < seatingChart.Count; y++)
                {
                    for (var x = 0; x < seatingChart[y].Count; x++)
                    {
                        if (seatingChart[y][x] == '#' || seatingChart[y][x] == 'L')
                        {
                            var occupiedAdjacentSeats = SeatsAdjacentP2(y, x, seatingChart);

                            if (occupiedAdjacentSeats == 0 && seatingChart[y][x] == 'L')
                            {
                                tempChart[y][x] = '#';
                                changedOccupied++;
                            }

                            if (occupiedAdjacentSeats >= 5 && seatingChart[y][x] == '#')
                            {
                                tempChart[y][x] = 'L';
                                changedOccupied++;
                            }
                        }
                    }
                }

                if (changedOccupied == 0)
                {
                    noSeatsChanged = true;
                }

                seatingChart = tempChart;
            }

            return seatingChart;
        }

        private int SeatsAdjacentP2(int yPos, int xPos, List<List<char>> seatingChart)
        {
            var occupiedAdjacent = 0;

            for(var d =1; d < 9; d++)
            {
                switch(d)
                {
                    case 1:
                        //Check down
                        if (NextSeatOccupied(yPos, xPos, 1, 0, seatingChart))
                        {
                            occupiedAdjacent++;
                        }
                        continue;
                    case 2:
                        //Check down-left
                        if (NextSeatOccupied(yPos, xPos, 1, 1, seatingChart))
                        {
                            occupiedAdjacent++;
                        }
                        continue;
                    case 3:
                        //Check left
                        if (NextSeatOccupied(yPos, xPos, 0, 1, seatingChart))
                        {
                            occupiedAdjacent++;
                        }
                        continue;
                    case 4:
                        //Check up-left
                        if (NextSeatOccupied(yPos, xPos, -1, 1, seatingChart))
                        {
                            occupiedAdjacent++;
                        }
                        continue;
                    case 5:
                        //Check up
                        if (NextSeatOccupied(yPos, xPos, -1, 0, seatingChart))
                        {
                            occupiedAdjacent++;
                        }
                        continue;
                    case 6:
                        //Check up-right
                        if (NextSeatOccupied(yPos, xPos, -1, -1, seatingChart))
                        {
                            occupiedAdjacent++;
                        }
                        continue;
                    case 7:
                        //Check right
                        if (NextSeatOccupied(yPos, xPos, 0, -1, seatingChart))
                        {
                            occupiedAdjacent++;
                        }
                        continue;
                    case 8:
                        //Check down-right
                        if (NextSeatOccupied(yPos, xPos, 1, -1, seatingChart))
                        {
                            occupiedAdjacent++;
                        }
                        continue;
                }


            }

            return occupiedAdjacent;
        }

        private bool NextSeatOccupied (int yPos, int xPos, int yDir, int xDir, List<List<char>> seatingChart)
        {
            var seatFound = false;
            var seatStatus = ' ';
            var move = 1;
            while(seatFound == false)
            {
                var xCheck = xPos + (xDir * move);
                var yCheck = yPos + (yDir * move);

                if (xCheck < 0 || yCheck < 0 || yCheck >= seatingChart.Count || xCheck >= seatingChart[yPos].Count)
                {
                    return false;
                }

                if (xCheck >= 0 && yCheck >= 0 && yCheck < seatingChart.Count && xCheck < seatingChart[yPos].Count)
                {
                    if (seatingChart[yCheck][xCheck] == '#' || seatingChart[yCheck][xCheck] == 'L')
                    {
                        seatStatus = seatingChart[yCheck][xCheck];
                        seatFound = true;
                    }
                }
                move++;
            }

            return (seatStatus == '#');
        }
    }
}
