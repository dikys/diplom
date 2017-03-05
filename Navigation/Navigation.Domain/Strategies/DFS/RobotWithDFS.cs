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

            Console.WriteLine(1);
        }

        public Node Start { get; }
        public List<Node> WayToExit { get; }
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

                var passages = visionResult.ObservedPassages;

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
            }
        }

        private void MoveBack()
        {
            CurrentNode.IsDeadLock = true;

            CurrentNode = WayToExit.Last();

            WayToExit.RemoveAt(WayToExit.Count - 1);
        }
    }
}
