namespace AmogAI.AStar;

using AmogAI.SteeringBehaviour;
using AmogAI.World;
using AmogAI.World.Entity;
using System;
using System.Collections.Generic;

public class PathFollowBehaviour {
    private readonly MovingEntity _entity;
    private readonly List<Node> _gridNodes;
    private readonly List<Edge> _gridEdges;
    public Queue<Node> Path { get; private set; } // does not contain the next node on path
    public Node NextNodeOnPath { get; private set; }
    public Vector Destination { get; private set; }
    public bool Arrived { get; private set;}

    public PathFollowBehaviour(MovingEntity entity, List<Node> gridNodes, List<Edge> gridEdges) {
        _entity = entity;
        _gridNodes = gridNodes;
        _gridEdges = gridEdges;
        Arrived = false;
    }

    public void SetDestination(Objective destination) {
        Destination = destination.Position;
        CalcAStar();
    }

    public static Node GetClosestNodeFromVector(Vector vector, List<Node> nodes) {
        if (nodes.Count <= 0)
            throw new Exception("Calculating on empty node list.");

        Node closestNode = nodes[0];
        foreach (Node node in nodes)
            if (vector.DistanceSq(node.Position) < vector.DistanceSq(closestNode.Position))
                closestNode = node;

        return closestNode;
    }

    public Vector Update() {
        if (Destination == _entity.Position) {
            Arrived = true;
            return new Vector(0, 0);
        }

        if (Path.Count <= 0)
            return Destination - _entity.Position;

        if (_entity.Position == Path.Peek().Position) {
            Node tempNode = Path.Dequeue();
            return tempNode.Position - _entity.Position;
        }

        return Path.Peek().Position - _entity.Position;
    }

    public void CalcAStar() {
        Node toNode = GetClosestNodeFromVector(Destination, _gridNodes);
        Node fromNode = GetClosestNodeFromVector(_entity.Position, _gridNodes);
        Path = AStar.FindPath(fromNode, toNode, _gridEdges);
    }

    public void ClearPath() {
        Path.Clear();
        Destination = null;
        Arrived = false;
    }
}