namespace AmogAI.AStar;

using AmogAI.World.Entity;
using System;
using System.Collections.Generic;
using AmogAI.SteeringBehaviour;
using AmogAI.World;

public class PathFollowBehaviour {
    private readonly MovingEntity _entity;
    private World _world;
    public Queue<Node>? Path { get; private set; } // does not contain the next node on path
    private Vector To;
    public bool HasArrived { get; private set;}
    public bool IsImpossible { get; private set;}

    public PathFollowBehaviour(MovingEntity entity, World world, Vector destination) {
        _entity = entity;
        _world = world;
        To = destination;
        CalcAStarTo(destination);
        HasArrived = false;
        
        if (entity.Position == destination)
            HasArrived = true;
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
            HasArrived = true;
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
        Node toNode = PathFollowBehaviour.GetClosestNodeFromVector(vector, _world.GridNodes);
        Node fromNode = PathFollowBehaviour.GetClosestNodeFromVector(_entity.Position, _world.GridNodes);
        Queue<Node>? path = AStar.FindPath(fromNode, toNode, _world.GridEdges);
        if (path != null) {
            Path = path;
            IsImpossible = false;
        } else {
            Path = new Queue<Node>();
            IsImpossible = true;
        }
    }
}