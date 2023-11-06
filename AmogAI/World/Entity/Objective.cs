namespace AmogAI.World.Entity;

using SteeringBehaviour;

public class Objective {
    public const float SIZE = 20.0f;
    public Vector Position { get; set; }
    public int Duration { get; set; } // in ms
    public bool isDone { get; set; }

    public Objective(Vector position) : this(position, 1000) { }
    public Objective(Vector position, int duration) {
        Position = position;
        Duration = duration;
        isDone = false;
    }

    public void Render(Graphics g) {
        g.DrawRectangle(new Pen(Brushes.Olive, 5),
            Position.X - SIZE / 2,
            Position.Y - SIZE / 2,
            SIZE,
            SIZE);
    }

    public void RenderOverlay(Graphics g) {
        if (isDone) {
            g.DrawLine(new Pen(Brushes.Olive, 5),
                Position.X - SIZE / 2,
                Position.Y - SIZE / 2,
                Position.X + SIZE / 2,
                Position.Y + SIZE / 2
            );
            g.DrawLine(new Pen(Brushes.Olive, 5),
                Position.X - SIZE / 2,
                Position.Y + SIZE / 2,
                Position.X + SIZE / 2,
                Position.Y - SIZE / 2
            );
        }
    }
}