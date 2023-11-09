namespace AmogAI.World.Entity;

using AmogAI.SteeringBehaviour;
using System.Drawing;

public class Wall {
    public Vector VecFrom { get; set; }
    public Vector VecTo { get; set; }
    public Vector Normal { get; set; }
    public Vector Center { get; init; }

    public Wall(Vector vecFrom, Vector vecTo, bool normalShouldFaceBottomLeft) {
        VecFrom = vecFrom;
        VecTo = vecTo;
        Center = (vecFrom + vecTo) / 2;
        CalculateNormal(normalShouldFaceBottomLeft);
    }

    private void CalculateNormal(bool normalShouldFaceBottomLeft) {
        Vector temp = VecTo - VecFrom;
        temp.Normalize();

        if (normalShouldFaceBottomLeft)
            Normal = temp.Perp();
        else
            Normal = temp.PerpNeg();
    }

    public void Render(Graphics g) {
        g.DrawLine(new Pen(Brushes.Purple, 5),
            VecFrom.X,
            VecFrom.Y,
            VecTo.X,
            VecTo.Y);
    }

    public void RenderOverlay(Graphics g) {
        g.DrawLine(new Pen(Brushes.Purple, 5),
            Center.X,
            Center.Y,
            Normal.X + 50,
            Normal.Y + 50);
    }
}
