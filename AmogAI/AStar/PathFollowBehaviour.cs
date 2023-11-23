namespace AmogAI.AStar;

using AmogAI.World.Entity;
using System;
using System.Collections.Generic;
using AmogAI.SteeringBehaviour;

public class PathFollowBehaviour {
    private readonly MovingEntity _entity;
    private readonly List<Node> _gridNodes;
    public Queue<Node> Path { get; private set; } // does not contain the next node on path
    public Node NextNodeOnPath { get; private set; }
    private Vector To;
    public bool Arrived { get; private set;}

    public PathFollowBehaviour(MovingEntity entity, List<Node> gridNodes, Vector destination) {
        _entity = entity;
        _gridNodes = gridNodes;
        To = destination;
        CalcAStarTo(destination);
        Arrived = false;

        if (entity.Position == destination)
            Arrived = true;
    }

    public static Node GetClosestNodeFromVector(Vector vector, List<Node> nodes) {
        if (nodes.Count <= 0)
            throw new Exception("calculating on empty Node list.");

        Node closestNode = nodes[0];
        foreach (Node node in nodes)
            if (vector.DistanceSq(node.Position) < vector.DistanceSq(closestNode.Position))
                closestNode = node;

        return closestNode;
    }

    public Vector update() {
        if (To == _entity.Position) {
            Arrived = true;
            return new Vector(0, 0);
        }

        if (Path.Count <= 0)
            return To - _entity.Position;

        if (_entity.Position == Path.Peek().Position) {
            Node tempNode = Path.Dequeue();
            return tempNode.Position - _entity.Position;

        }

        return Path.Peek().Position - _entity.Position;

    }

    public void CalcAStarTo(Objective objective) {
        CalcAStarTo(objective.Position);
    }

    public void CalcAStarTo(Vector vector) {
        To = vector;
        Node toNode = PathFollowBehaviour.GetClosestNodeFromVector(vector, _gridNodes);
        Node fromNode = PathFollowBehaviour.GetClosestNodeFromVector(_entity.Position, _gridNodes);
        Path = AStar.FindPath(fromNode, toNode, _gridNodes);
    }
}