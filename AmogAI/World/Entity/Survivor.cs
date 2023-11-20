﻿namespace AmogAI.World.Entity;

using AmogAI.FuzzyLogic;
using AmogAI.StateBehaviour;
using AmogAI.SteeringBehaviour;
using System.Drawing;

public class Survivor : MovingEntity {
    public SurvivorTaskGoal SurvivorTaskGoal { get; set; }
    public SurvivorStateMachine SurvivorStateMachine { get; set; }
    public const float decisionInterval = 1000; // ms
    public float decisionIntervalDelta = 0;
    public float Health { get; set; }
    public Objective CurrentObjective { get; set; }

    public Survivor(Vector pos, World world) : base(pos, world) {
        Velocity = new Vector(0, 0);
        Scale = 10;
        Health = 100;
        
        SurvivorTaskGoal = new SurvivorTaskGoal(this);
        SurvivorStateMachine = new SurvivorStateMachine(this);
    }

    public Survivor(Vector pos, MovingEntity target, World world) : this(pos, world) {
        Target = target;
    }

    public void SetObjective(int objectiveId) {
        World.Objectives.TryGetValue(objectiveId, out Objective objective);
        CurrentObjective = objective;
    }

    public override void Update(float timeDelta) {
        base.Update(timeDelta);
        SurvivorStateMachine.Update(timeDelta);

        decisionIntervalDelta += timeDelta;
        if (decisionIntervalDelta > decisionInterval) {
            decisionIntervalDelta = 0;

            float result = SurvivorTaskGoal.Process(this.World.Objectives);
            Console.WriteLine(result);
        }
    }

    public override void Render(Graphics g) {
        double entityX = Position.X - Scale;
        double entityY = Position.Y - Scale;
        double size = Scale * 2;

        // Draw the entity
        Pen p = new Pen(Color.Blue, 1);
        g.DrawEllipse(p, new Rectangle((int)entityX, (int)entityY, (int)size, (int)size));
    }

    public override void RenderOverlay(Graphics g) {
        Pen p = new Pen(Color.Blue, 1);
        
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

        if (SteeringBehaviour.WanderTarget != null) {
            g.DrawEllipse(p, new Rectangle((int)circleX, (int)circleY, (int)sizeRadius, (int)sizeRadius));

            double targetX = Position.X + (SteeringBehaviour.WanderTarget.X - Scale);
            double targetY = Position.Y + (SteeringBehaviour.WanderTarget.Y - Scale);
            double sizeTarget = Scale * 2;
            Pen p2 = new Pen(Color.Red, 1);
            g.DrawEllipse(p2, new Rectangle((int)targetX, (int)targetY, (int)sizeTarget, (int)sizeTarget));
        }

        // Draw the feelers
        Pen p3 = new Pen(Color.Green, 1);
        if (SteeringBehaviour.Feelers != null) {
            foreach (var feeler in SteeringBehaviour.Feelers) {
                g.DrawLine(p3,
                    Position.X,
                    Position.Y,
                    feeler.X,
                    feeler.Y);
            }
        }
    }
}