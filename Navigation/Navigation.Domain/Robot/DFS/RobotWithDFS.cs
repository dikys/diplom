using System;
using System.Collections.Generic;
using System.Linq;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.DFS
{
    public class RobotWithDFS : MobileRobot
    {
        public Node Start { get; }

        private Node _currentNode;
        public Node CurrentNode
        {
            get { return _currentNode; }
            set
            {
                _currentNode = value;

                Position = _currentNode.Position.Clone();
            }
        }
        public List<Node> WayToExit { get; }

        public List<List<Line>> ViewedContours { get; }

        public RobotWithDFS(Maze.Maze maze, Point position) : base(maze, position)
        {
            Start = new Node(position);
            CurrentNode = Start;
            WayToExit = new List<Node>();
            ViewedContours = new List<List<Line>>();
        }
        
        public override void Run()
        {
            var exitPoint = new Point();
            
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

                    Console.WriteLine("Идти назад надо");

                    continue;
                }

                var сontour = new List<Line>();

                if (Vision.LookAround(ref сontour, ref exitPoint))
                {
                    WayToExit.Add(new Node(exitPoint));

                    Console.WriteLine("Выход найден");

                    return;
                }

                var passages = GetPassageInСontour(сontour).Where(IsNewPassage).ToList();

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

                    Console.WriteLine("Идти назад надо");
                }
                
                ViewedContours.Add(сontour);
            }
        }

        private bool IsNewPassage(Line passage)
        {
            return ViewedContours.SelectMany(contour => contour).All(line => !line.HavePoint(passage.Start) && !line.HavePoint(passage.End));
        }
    }
}
