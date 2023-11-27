namespace AmogAI.AStar;

using AmogAI.SteeringBehaviour;
using AmogAI.World;

public class Graph {
    public static readonly float NODE_SPACING = 40;
    public static readonly Vector NODE_START = new Vector(10, 10);

    public static (List<Node>, List<Edge>) Generate(World world) {
        var nodes = new List<Node>();
        var edges = new List<Edge>();
        Queue<Node> nodeQueue = new Queue<Node>();
        // start node
        var node1 = new Node(NODE_START);
        nodeQueue.Enqueue(node1);

        while (nodeQueue.Count > 0) {
            Node node = nodeQueue.Dequeue();
            if (nodeQueue.Count > 500) // failsafe if it goes through walls // TODO make sure this doesnt need to be here
                break;
            if (nodes.Any(n => n.Equals(node)))
                continue;

            nodes.Add(node);
            List<Node> neighbourNodes = getNodeNeighbours(node);
            foreach (Node neighbourNode in neighbourNodes) {
                // check if new node is on the other side of a wall
                float distToThisIP = 0f;
                Vector point = new Vector();
                var isIntersectingWall = false;
                for (int wall = 0; wall < world.Walls.Count; wall++) {
                    if (Vector.LineIntersection(node.Position,
                                            neighbourNode.Position,
                                            world.Walls[wall].VecFrom,
                                            world.Walls[wall].VecTo,
                                            ref distToThisIP,
                                            ref point)) {
                        isIntersectingWall = true;
                        break;
                    }
                }
                if (isIntersectingWall)
                    continue;

                // not through wall -> add node and edge to lists
                
                nodeQueue.Enqueue(neighbourNode);

                var edge = new Edge(node, neighbourNode);

                if (!edges.Any(e => e.Equals(edge)))
                    edges.Add(edge);

                node.ConnectedEdges.Add(edge);
            }
        }
        return (nodes, edges);
    }

    private static List<Node> getNodeNeighbours(Node node) {
        List<Node> retList = new List<Node>();
        foreach (var xDir in new int[] { -1, 0, 1 })
            foreach (var yDir in new int[] { -1, 0, 1 })
                if (xDir != 0 || yDir != 0)
                    retList.Add(new Node(node.Position.X + xDir * NODE_SPACING, node.Position.Y + yDir * NODE_SPACING));

        return retList;
    }
}
