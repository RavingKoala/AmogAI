namespace AmogAI.AStar;

using AmogAI.SteeringBehaviour;
using AmogAI.World;
using System;
using System.Drawing;

public class Node : IRenderable {
    public Vector Position { get; set; }
    public List<Edge> ConnectedEdges { get; set; }

    public Node(Vector pos) {
        Position = pos;
        ConnectedEdges = new List<Edge>();
    }
    public Node(float x, float y) : this(new Vector(x, y)) { }
    public Node(int x, int y) : this(new Vector(x, y)) { }

    public void Render(Graphics g) {
        throw new NotImplementedException();
    }

    public void RenderOverlay(Graphics g) {
        Pen p = new Pen(Color.Red, 2);

        g.DrawEllipse(p, Position.X - 2, Position.Y - 2, 4, 4);
        //g.FillEllipse(p, Position.X - 2, Position.Y - 2, (float)4, (float)4);
        foreach (Edge edge in ConnectedEdges) {
            Vector direction = (edge.Node2.Position - Position) / 4;
            g.DrawLine(new Pen(Color.Black, 2), Position.X, Position.Y, Position.X + direction.X, Position.Y + direction.Y);
        }
    }

    public static bool operator ==(Node n1, Node n2) {
        if ((object)n1 == null)
            return (object)n2 == null;

        return n1.Equals(n2);
    }

    public static bool operator !=(Node n1, Node n2) {
        return !(n1 == n2);
    }

    public override bool Equals(object? obj) {
        //Check for null and compare run-time types.
        if (obj == null || !GetType().Equals(obj.GetType())) {
            return false;
        } else {
            Node node = (Node)obj;
            return Position == node.Position;
        }
    }
}
