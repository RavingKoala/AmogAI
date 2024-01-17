namespace AmogAI.AStar;

using AmogAI.SteeringBehaviour;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

public class AStarNode {
    public Node Node;
    public AStarNode? LastNode;
    public float GScore;
    public float FScore;

    //only the first node
    public AStarNode(Node node) {
        Node = node;
        GScore = 0;
        FScore = float.MaxValue;
    }

    public static float calculateHeuristic(Node from, Node to) {
        return Math.Abs(to.Position.X - from.Position.X) + Math.Abs(to.Position.X - from.Position.Y);
    }
}

public class AStar {
    public static Queue<Node>? FindPath(Node fromNode, Node toNode, List<Edge> edges) {
        PriorityQueue<AStarNode, float> nodesQueue = new PriorityQueue<AStarNode, float>();
        List<AStarNode> visitedNodes = new List<AStarNode>();

        AStarNode startNode = new AStarNode(fromNode);
        startNode.FScore = AStarNode.calculateHeuristic(fromNode, toNode);
        nodesQueue.Enqueue(startNode, startNode.FScore);

        //while openSet is not empty:
        while (nodesQueue.Count > 0) {
            AStarNode currentNode = nodesQueue.Dequeue();
            visitedNodes.Add(currentNode);

            if (currentNode.Node == toNode)
                return reconstructPath(currentNode);

            List<Edge> NeigborEdges = edges.FindAll(e => e.Node1 == currentNode.Node || e.Node2 == currentNode.Node);
            foreach (Edge edge in NeigborEdges) {
                Node connectedNode = edge.Node1 == currentNode.Node ? edge.Node2 : edge.Node1;
                AStarNode? neighbor = visitedNodes.Find(n => n.Node == connectedNode);

                if (neighbor != null) {
                    float neighbor_gScore = currentNode.GScore + edge.cost;
                    if (neighbor_gScore < neighbor.GScore) {
                        neighbor.LastNode = currentNode;
                        neighbor.GScore = neighbor_gScore;
                        neighbor.FScore = neighbor.GScore + AStarNode.calculateHeuristic(neighbor.Node, toNode);
                    }
                } else {
                    neighbor = new AStarNode(connectedNode);
                    neighbor.LastNode = currentNode;
                    neighbor.GScore = currentNode.GScore + edge.cost;
                    neighbor.FScore = neighbor.GScore + AStarNode.calculateHeuristic(connectedNode, toNode);
                    nodesQueue.Enqueue(neighbor, neighbor.FScore);
                }
            }
        }

        return null; // no valid path found
    }

    private static Queue<Node> reconstructPath(AStarNode destinationNode) {
        AStarNode? currentNode = destinationNode;
        Queue<Node> path = new Queue<Node>();
        while (currentNode != null) {
            path.Enqueue(currentNode.Node);
            currentNode = currentNode.LastNode;
        }
        return new Queue<Node>(path.Reverse());
    }
}
