using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Navigation.Domain.Robot;
using Navigation.Infrastructure;
using Navigation.Domain.Robot.Visions;

namespace Navigation.Domain.Strategies.DFS
{
    [StrategyInfo(Name = "Мгновенный DFS")]
    public class RobotWithDFS : MobileRobot
    {
        private Node _currentNode;
        
        public RobotWithDFS(Lazy<IRobotVision> robotVision, Point position) : base(robotVision, position)
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

                    MoveBack();

                    continue;
                }

                var visionResult = RobotVision.Value.LookAround();

                if (visionResult.SawFinish)
                {
                    WayToExit.Add(new Node(visionResult.FinishPoint));

                    Console.WriteLine("Выход найден");

                    return;
                }

                var passages = visionResult.ObservedPassages.Where(IsNewPassage).ToList();

                if (passages.Any())
                {
                    CurrentNode.AdjacentNodes.AddRange(passages.Select(passage => new Node(passage.Center)));
                }
                else if (CurrentNode.Position == Start.Position)
                {
                    Console.WriteLine("Выхода нет");

                    return;
                }
                else
                {
                    MoveBack();
                }

                ViewedContours.Add(visionResult.ObservedContour.ToList());
            }
        }

        private void MoveBack()
        {
            CurrentNode.IsDeadLock = true;

            CurrentNode = WayToExit.Last();

            WayToExit.RemoveAt(WayToExit.Count - 1);
        }

        private bool IsNewPassage(Line passage)
        {
            return ViewedContours.SelectMany(contour => contour).All(line => !line.HavePoint(passage.Start) && !line.HavePoint(passage.End));
        }
    }
}
