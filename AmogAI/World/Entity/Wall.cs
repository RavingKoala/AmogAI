namespace AmogAI.World.Entity;

using AmogAI.SteeringBehaviour;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Wall{
    public Vector VecFrom { get; set; }
    public Vector VecTo{ get; set; }
    public Vector Normal { get; set; }
    public Vector Center { get; init; }

    public Wall(Vector vecFrom, Vector vecTo) {
        VecFrom = vecFrom;
        VecTo = vecTo;
        Center = (vecFrom + vecTo) / 2;
        CalculateNormal();
    }

    private void CalculateNormal() {
        Vector temp = VecTo - VecFrom;
        temp.Normalize();

        Normal = temp.Perp();
    }

    public void Render(Graphics g) {
        g.DrawLine(new Pen(Brushes.Purple, 5), new Point((int)VecFrom.X, (int)VecFrom.Y), new Point((int)VecTo.X, (int)VecTo.Y));
    }
}
