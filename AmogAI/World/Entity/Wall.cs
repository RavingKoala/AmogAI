﻿namespace AmogAI.World.Entity;

using AmogAI.SteeringBehaviour;
using System.Drawing;

public class Wall : IRenderable {
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
        Vector v = Normal.Normalize() * 20;

        g.DrawLine(new Pen(Brushes.Purple, 1),
            Center.X,
            Center.Y,
            Center.X + v.X,
            Center.Y + v.Y);
    }
}
