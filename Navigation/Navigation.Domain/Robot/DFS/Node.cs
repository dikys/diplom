using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Infrastructure;

namespace Navigation.Domain.Robot.DFS
{
    public class Node
    {
        public Point Position { get; }
        public bool IsDeadLock { get; set; }

        public List<Node> AdjacentNodes { get; }
        public bool HaveAdjacentNodes => AdjacentNodes.Any();

        public IReadOnlyList<Node> NotDeadLockAdjacentNodes => AdjacentNodes.Where(node => node.IsDeadLock == false).ToList().AsReadOnly();
        public bool HaveNotDeadLockAdjacentNodes => NotDeadLockAdjacentNodes.Any();
        
        public Node(Point position)
        {
            Position = position;

            AdjacentNodes = new List<Node>();

            IsDeadLock = false;
        }
    }
}
