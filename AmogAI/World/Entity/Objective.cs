namespace AmogAI.World.Entity;

using SteeringBehaviour;

public class Objective : IRenderable {
    private const float SIZE = 20.0f;
    public Vector Position { get; set; }
    public int Duration { get; set; } // in ms
    public bool IsInProgress { get; set; }
    public bool IsDone { get; set; }
    public Color BrushColor { get; set; }    
    public Objective(Vector position, Color brushColor) : this(position, 5000, brushColor) { }
    public Objective(Vector position, int duration, Color brushColor) {
        Position = position;
        Duration = duration;
        BrushColor = brushColor;
    }
    
    public void StartTask() {
        IsInProgress = true;
    }

    public void EndTask() {
        IsInProgress = false;
        IsDone = true;
    }

    public void Render(Graphics g) {
        g.DrawRectangle(new Pen(BrushColor, 5),
            Position.X - SIZE / 2,
            Position.Y - SIZE / 2,
            SIZE,
            SIZE);
    }

    public void RenderOverlay(Graphics g) {
        if (IsDone) {
            g.DrawLine(new Pen(BrushColor, 3),
                Position.X - SIZE / 2,
                Position.Y - SIZE / 2,
                Position.X + SIZE / 2,
                Position.Y + SIZE / 2
            );
            g.DrawLine(new Pen(BrushColor, 3),
                Position.X - SIZE / 2,
                Position.Y + SIZE / 2,
                Position.X + SIZE / 2,
                Position.Y - SIZE / 2
            );
        }
    }
}