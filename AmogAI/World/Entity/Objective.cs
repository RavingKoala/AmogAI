namespace AmogAI.World.Entity;

using SteeringBehaviour;

public class Objective {
    private const float SIZE = 20.0f;
    public Vector Position { get; set; }
    public int Duration { get; set; } // in ms
    public bool IsInProgress { get; private set; }
    public bool IsDone { get; private set; }
    public Objective(Vector position) : this(position, 5000) { }
    public Objective(Vector position, int duration) {
        Position = position;
        Duration = duration;
        IsDone = false;
        //IsInProgress = false;
    }
    
    public void StartTask(Survivor survivor) {
        IsInProgress = true;
    }

    public void EndTask() {
        IsInProgress = false;
        IsDone = true;
        Console.WriteLine("Task is done");
    }

    public void Render(Graphics g) {
        g.DrawRectangle(new Pen(Brushes.Olive, 5),
            Position.X - SIZE / 2,
            Position.Y - SIZE / 2,
            SIZE,
            SIZE);
    }

    public void RenderOverlay(Graphics g) {
        if (IsDone) {
            g.DrawLine(new Pen(Brushes.Olive, 3),
                Position.X - SIZE / 2,
                Position.Y - SIZE / 2,
                Position.X + SIZE / 2,
                Position.Y + SIZE / 2
            );
            g.DrawLine(new Pen(Brushes.Olive, 3),
                Position.X - SIZE / 2,
                Position.Y + SIZE / 2,
                Position.X + SIZE / 2,
                Position.Y - SIZE / 2
            );
        }
    }
}