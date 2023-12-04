namespace AmogAI.AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmogAI.World;

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

    public static bool operator ==(Edge e1, Edge e2) {
        if ((object)e1 == null)
            return (object)e2 == null;

        return e1.Equals(e2);
    }

    public static bool operator !=(Edge e1, Edge e2) {
        return !(e1 == e2);
    }

    public override bool Equals(object? obj) {
        //Check for null and compare run-time types.
        if (obj == null || !GetType().Equals(obj.GetType())) {
            return false;
        } else {
            Edge edge = (Edge)obj;
            if (Bidirectional)
                return Node1.Equals(edge.Node1) && Node2.Equals(edge.Node2) || Node1.Equals(edge.Node2) && Node2.Equals(edge.Node1);

            return this.Node1.Equals(edge.Node1) && this.Node2.Equals(edge.Node2);
        }
    }
}
