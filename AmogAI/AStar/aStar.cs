namespace AmogAI.AStar;

using System.Collections.Generic;

using System.IO;
using AmogAI.World;

public class AStar {
    // <(float, Queue<Node>), float>
    // First float is cost of Queue<Node> so far
    // Last float is total travel distance
    // eg:
    //    { (20, (A -> B)), (20, (A -> C)), (40, (A -> B -> D), ... }
    private static PriorityQueue<(float, Queue<Node>), float> _paths = new PriorityQueue<(float, Queue<Node>), float>();


    public static Queue<Node>? FindPath(Node fromNode, Node toNode, List<Edge> edges) {
        return DoAStar(fromNode, toNode, edges);

        //Queue<Node> retQueue = new Queue<Node>();
        
        //retQueue.Enqueue(fromNode);
        //retQueue.Enqueue(toNode);

        //return retQueue;
    }

    private static Queue<Node>? DoAStar(Node fromNode, Node toNode, List<Edge> edges) {
        _paths.Clear();

        Queue<Node> firstPath = new Queue<Node>();
        firstPath.Enqueue(fromNode);
        float firstDistanceTraveled = 0;
        _paths.Enqueue((firstDistanceTraveled, firstPath), fromNode.Position.Distance(toNode.Position));

        var i = 0;
        while (_paths.Count > 0) {
            (float distanceTraveled, Queue<Node> path) = _paths.Dequeue();
            Node lastNode = path.Last();

            if (lastNode == toNode)
                return path;

            List<Edge> connectedEdges = edges.FindAll(e => e.Node1 == lastNode || e.Node2 == lastNode);

            foreach (Edge edge in connectedEdges) {
                Node connectedNode = edge.Node1 == lastNode ? edge.Node2 : edge.Node1;

                if (path.Any(n => n == connectedNode))
                    continue; // prevent loops

                Queue<Node> newPath = new Queue<Node>(path);
                newPath.Enqueue(connectedNode);
                float newDistance = distanceTraveled + edge.cost;
                float priority = newDistance + connectedNode.Position.Distance(toNode.Position);
                _paths.Enqueue(
                    (newDistance, newPath),
                    priority
                );
            }
        }

        return null; // no valid path found
    }
}
