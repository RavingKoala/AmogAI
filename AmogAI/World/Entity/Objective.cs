namespace AmogAI.World.Entity;

using SteeringBehaviour;

public class Objective {
    private const float SIZE = 20.0f;
    public Vector Position { get; set; }
    public Survivor Survivor { get; private set; }
    public int Duration { get; set; } // in ms
    public System.Windows.Forms.Timer Timer { get; set; }
    public bool IsDone { get; private set; }
    public Objective(Vector position) : this(position, 5000) { }
    public Objective(Vector position, int duration) {
        Position = position;
        Duration = duration;
        Timer = new System.Windows.Forms.Timer();
        Timer.Interval = Duration;
        Timer.Tick += EndTask;
        IsDone = false;
    }
    
    public void StartTask(Survivor survivor) {
        Survivor = survivor;
        Survivor.IsDoingTask = true;
        Timer.Start();
    }

    private void EndTask(object? sender, EventArgs e) {
        IsDone = true;
        Timer.Stop();
        Survivor.IsDoingTask = false;
        Survivor = null;
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