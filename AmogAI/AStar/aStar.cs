namespace AmogAI.AStar;

using System.Collections.Generic;

public class AStar {
    public static Queue<Node> FindPath(Node fromNode, Node toNode, List<Node> gridNodes) {
        var retQueue = new Queue<Node>();
        
        retQueue.Enqueue(fromNode);
        retQueue.Enqueue(toNode);

        return retQueue;
    }
}
