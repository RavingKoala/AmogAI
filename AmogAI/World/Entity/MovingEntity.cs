namespace AmogAI.World.Entity;

using AmogAI.AStar;
using AmogAI.SteeringBehaviour;

public abstract class MovingEntity : IRenderable {
    public World World { get; set; }
    public Vector Position { get; set; }
    public MovingEntity? Target { get; set; }
    public Vector Velocity { get; set; }
    public Vector Heading { get; set; }
    public Vector Side { get; set; }
    public SteeringBehaviour SteeringBehaviour { get; set; }
    public PathFollowBehaviour PathFollowBehaviour { get; set; }
    public float Mass { get; set; }
    public float MaxSpeed { get; set; }
    public float MaxForce { get; set; }
    public float MaxTurnRate { get; set; }
    public float Scale { get; set; }
    public float TimeElapsed { get; set; }

    public MovingEntity(Vector pos, World world) {
        Mass = 300f;
        MaxSpeed = 0.2f;
        MaxForce = 1f;

        Position = pos;
        World = world;
        Velocity = new Vector();
        Heading = new Vector();
        Side = new Vector();
        SteeringBehaviour = new SteeringBehaviour(this);
        PathFollowBehaviour = new PathFollowBehaviour(this, world.GridNodes);
    }

    public virtual void Render(Graphics g) {
        g.DrawEllipse(new Pen(Brushes.Black, 10), (int)MainFrame.WindowCenter.X - 55, (int)MainFrame.WindowCenter.Y - 55, 100, 100);
    }

    public virtual void Update(float timeDelta) {
        TimeElapsed = timeDelta;

        // If a path is set, update method will move on that path
        if (PathFollowBehaviour.Destination != null) {
            Vector force = PathFollowBehaviour.Update();
            force = force.Truncate(MaxSpeed * timeDelta);
            Position += force;
            if (!PathFollowBehaviour.Arrived)
                return;
        }

        // If there is no path, update method will use steering behaviours instead
        // Calculate the steering force 
        Vector steeringForce = SteeringBehaviour.Calculate();
        Vector acceleration = steeringForce / Mass;

        // Convert the steering force into an acceleration 
        Velocity += acceleration * timeDelta;

        // Update the vehicle's velocity
        Velocity.Truncate(MaxSpeed);
        Position += Velocity * timeDelta;

        // Update the vehicle's position using the new velocity
        if (Velocity.LengthSquared() > 0.00000001) {
            Heading = Velocity.Normalize();

            Side = Heading.Perp();
        }

        //Console.WriteLine(ToString());
    }

    public override string ToString() {
        return string.Format("{0}", Velocity);
    }

    public virtual void RenderOverlay(Graphics g) {
        throw new NotImplementedException();
    }
}
