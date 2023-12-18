namespace AmogAI.World.Entity;

using AmogAI.StateBehaviour.KillerStates;
using AmogAI.StateBehaviour.SurvivorStates;
using AmogAI.SteeringBehaviour;

public class Killer : MovingEntity {
    public Survivor? target;
    public KillerStateMachine stateMachine;

    public Killer(Vector pos, World world) : base(pos, world) {
        Velocity = new Vector(0, 0);
        Scale = 10;

        target = null;
        stateMachine = new KillerStateMachine(this);
    }

    public override void Update(float timeDelta) {
        base.Update(timeDelta);
        stateMachine.Update(timeDelta);
    }

    public override void Render(Graphics g) {
        double entityX = Position.X - Scale;
        double entityY = Position.Y - Scale;
        double size = Scale * 2;

        // Draw the entity
        Pen p = new Pen(Color.DarkRed, 1);
        g.DrawEllipse(p, new Rectangle((int)entityX, (int)entityY, (int)size, (int)size));
    }

    public override void RenderOverlay(Graphics g) {
        Pen p = new Pen(Color.DarkRed, 1);
        
        // Draw the velocity vector
        g.DrawLine(p,
            Position.X,
            Position.Y,
            Position.X + Velocity.X * 80,
            Position.Y + Velocity.Y * 80);

        // Draw the wander circle and target
        Vector circleCenter = Heading.Clone().Normalize() * SteeringBehaviour.WanderDistance + Position;
        double circleX = circleCenter.X - SteeringBehaviour.WanderRadius;
        double circleY = circleCenter.Y - SteeringBehaviour.WanderRadius;
        double sizeRadius = SteeringBehaviour.WanderRadius * 2;
        g.DrawEllipse(p, new Rectangle((int)circleX, (int)circleY, (int)sizeRadius, (int)sizeRadius));

        double targetX = Position.X + (SteeringBehaviour.WanderTarget.X - Scale);
        double targetY = Position.Y + (SteeringBehaviour.WanderTarget.Y - Scale);
        double sizeTarget = Scale * 2;
        Pen p2 = new Pen(Color.Red, 1);
        g.DrawEllipse(p2, new Rectangle((int)targetX, (int)targetY, (int)sizeTarget, (int)sizeTarget));

        // Draw the feelers
        Pen p3 = new Pen(Color.Green, 1);
        foreach (var feeler in SteeringBehaviour.Feelers) {
            g.DrawLine(p3,
                Position.X,
                Position.Y,
                feeler.X,
                feeler.Y);
        }
    }
}
