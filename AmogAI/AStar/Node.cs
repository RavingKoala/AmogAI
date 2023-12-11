namespace AmogAI.AStar;

using AmogAI.SteeringBehaviour;
using AmogAI.World;
using System;
using System.Drawing;

public class Node : IRenderable {
    public Vector Position { get; set; }

    public Node(Vector pos) {
        Position = pos;
    }
    public Node(float x, float y) : this(new Vector(x, y)) { }
    public Node(int x, int y) : this(new Vector(x, y)) { }

    public void Render(Graphics g) {
        throw new NotImplementedException();
    }

    public void RenderOverlay(Graphics g) {
        Brush b = new SolidBrush(Color.Red);

        g.FillEllipse(b, Position.X - 2, Position.Y - 2, 4, 4);
    }

    public static bool operator ==(Node? n1, Node? n2) {
        if (n1 is null)
            return n2 is null;

        return n1.Equals(n2);
    }

    public static bool operator !=(Node? n1, Node? n2) {
        if (n1 is null)
            return n2 is not null;

        return !n1.Equals(n2);
    }

    public override bool Equals(object? obj) {
        //Check for null and compare run-time types.
        if (obj == null || !GetType().Equals(obj.GetType())) {
            return false;
        } else {
            Node node = (Node) obj;
            return Position == node.Position;
        }
    }

    public override int GetHashCode() {
        return Position.GetHashCode();
    }
}
