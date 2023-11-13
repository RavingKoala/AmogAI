namespace AmogAI.World.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Edge : IRenderable {

	public Node Node1;
	public Node Node2;
	public const bool Bidirectional = true;

	public void Render(Graphics g) {
		throw new NotImplementedException();
	}

	public void RenderOverlay(Graphics g) {
		Pen p = new Pen(Color.Yellow, 2);

		g.DrawLine(p, Node1.Position.X, Node1.Position.Y, Node2.Position.X, Node2.Position.Y);
	}
}
