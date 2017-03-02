using System.Collections.Generic;
using System.Linq;

namespace Navigation.Infrastructure
{
    public class Node
    {
        public Point Position { get; }
        public bool IsDeadLock { set; get; }

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
