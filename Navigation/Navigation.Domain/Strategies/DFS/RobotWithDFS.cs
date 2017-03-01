using System;
using System.Collections.Generic;
using System.Linq;
using Navigation.Domain.Robot;
using Navigation.Infrastructure;

namespace Navigation.Domain.Strategies.DFS
{
    [StrategyInfo(Name = "Мгновенный DFS")]
    public class RobotWithDFS : MobileRobot
    {
        private Node _currentNode;
        
        public RobotWithDFS(Maze.Maze maze, Point position) : base(maze, position)
        {
            Start = new Node(position);
            CurrentNode = Start;
            WayToExit = new List<Node>();
            ViewedContours = new List<List<Line>>();
        }

        public Node Start { get; }
        public List<Node> WayToExit { get; }
        public List<List<Line>> ViewedContours { get; }
        public Node CurrentNode
        {
            get { return _currentNode; }
            set
            {
                _currentNode = value;

                Position = _currentNode.Position;
            }
        }

        public override void Run()
        {
            while (true)
            {
                if (CurrentNode.HaveAdjacentNodes)
                {
                    if (CurrentNode.HaveNotDeadLockAdjacentNodes)
                    {
                        WayToExit.Add(CurrentNode);

                        CurrentNode = CurrentNode.NotDeadLockAdjacentNodes.First();

                        continue;
                    }

                    if (CurrentNode.Position == Start.Position)
                    {
                        Console.WriteLine("Выхода нет");

                        return;
                    }

                    CurrentNode.IsDeadLock = true;

                    CurrentNode = WayToExit.Last();

                    WayToExit.RemoveAt(WayToExit.Count - 1);

                    continue;
                }

                var visionResult = Vision.LookAround();

                if (visionResult.SawFinish)
                {
                    WayToExit.Add(new Node(visionResult.FinishPoint));

                    Console.WriteLine("Выход найден");

                    return;
                }

                var passages = visionResult.ObservedPassages.Where(IsNewPassage).ToList();

                if (passages.Any())
                {
                    passages.ForEach(passage => CurrentNode.AdjacentNodes.Add(new Node(passage.Center)));
                }
                else if (CurrentNode.Position == Start.Position)
                {
                    Console.WriteLine("Выхода нет");

                    return;
                }
                else
                {
                    CurrentNode.IsDeadLock = true;

                    CurrentNode = WayToExit.Last();

                    WayToExit.RemoveAt(WayToExit.Count - 1);
                }
                
                ViewedContours.Add(visionResult.ObservedContour.ToList());
            }
        }

        private bool IsNewPassage(Line passage)
        {
            return ViewedContours.SelectMany(contour => contour).All(line => !line.HavePoint(passage.Start) && !line.HavePoint(passage.End));
        }
    }
}
