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
        
        public RobotWithDFS(IRobotVision robotVision, Point position) : base(robotVision, position)
        {
            Start = new Node(position);
            CurrentNode = Start;

            WayToExit = new List<Node>();
        }

        public Node Start { get; }
        public List<Node> WayToExit { get; }
        public bool FinishFound => WayToExit.Count != 0;
        private Node CurrentNode
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
                        //Console.WriteLine("Выхода нет");

                        return;
                    }

                    MoveBack();

                    continue;
                }

                var visionResult = RobotVision.LookAround(Position);

                if (visionResult.SawFinish)
                {
                    WayToExit.Add(CurrentNode);
                    WayToExit.Add(new Node(visionResult.FinishPoint));

                    //Console.WriteLine("Выход найден");

                    return;
                }
                
                if (visionResult.ObservedPassages.Any())
                {
                    CurrentNode.AdjacentNodes.AddRange(visionResult.ObservedPassages.Select(passage => new Node(passage.Center)));
                }
                else if (CurrentNode.Position == Start.Position)
                {
                    //Console.WriteLine("Выхода нет");

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
