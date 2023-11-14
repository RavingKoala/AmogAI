namespace AmogAI.World.Grid;

using AmogAI.SteeringBehaviour;
using System;
using System.Drawing;

public class Node : IRenderable {
	public Vector Position {  get; set; }
	// public list<node> NeighboringNodes ?

	public Node(Vector pos) {
		Position = pos;
	}
	public Node(float x, float y) : this(new Vector(x, y)) { }
	public Node(int x, int y) : this(new Vector((float)x, (float)y)) { }

	public void Render(Graphics g) {
		throw new NotImplementedException();
	}

	public void RenderOverlay(Graphics g) {
		Pen p = new Pen(Color.Red, 2);

		g.DrawEllipse(p, Position.X - 2, Position.Y - 2, (float)4, (float)4);
        //g.FillEllipse(p, Position.X - 2, Position.Y - 2, (float)4, (float)4);
    }

    public override bool Equals(Object? obj) {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
            return false;
        } else {
            Node node = (Node)obj;
            return this.Position.Equals(node.Position);
        }
    }
}
