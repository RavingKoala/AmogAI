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
		Pen p = new Pen(Color.Yellow);

		g.DrawEllipse(p, Position.X - 2, Position.Y - 2, 4, 4);
	}
}
