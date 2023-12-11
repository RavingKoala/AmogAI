namespace AmogAI.AStar;
using AmogAI.World;
using System;

public class Edge : IRenderable {

    public Node Node1;
    public Node Node2;
    public float cost;
    public bool Bidirectional;

    public Edge(Node node1, Node node2) {
        Node1 = node1;
        Node2 = node2;
        cost = node1.Position.Distance(node2.Position);
        Bidirectional = true;
    }

    public void Render(Graphics g) {
        throw new NotImplementedException();
    }

    public void RenderOverlay(Graphics g) {
        Pen p = new Pen(Color.Red, 1);

        g.DrawLine(p, Node1.Position.X, Node1.Position.Y, Node2.Position.X, Node2.Position.Y);
    }

    public static bool operator ==(Edge? e1, Edge? e2) {
        if (e1 is null)
            return e2 is null;

        return e1.Equals(e2);
    }

    public static bool operator !=(Edge? e1, Edge? e2) {
        if (e1 is null)
            return e2 is not null;
        
        return !e1.Equals(e2);
    }

    public override bool Equals(object? obj) {
        //Check for null and compare run-time types.
        if (obj == null || !GetType().Equals(obj.GetType())) {
            return false;
        } else {
            Edge edge = (Edge) obj;
            if (Bidirectional)
                return Node1.Equals(edge.Node1) && Node2.Equals(edge.Node2) || Node1.Equals(edge.Node2) && Node2.Equals(edge.Node1);

            return Node1.Equals(edge.Node1) && Node2.Equals(edge.Node2);
        }
    }

    public override int GetHashCode() {
        return Node1.GetHashCode() ^ Node2.GetHashCode() ^ (int) cost ^ (Bidirectional ? -1 : 0);
    }
}
