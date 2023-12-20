namespace AmogAI.World.Entity;

using AmogAI.AStar;
using AmogAI.FuzzyLogic;
using AmogAI.StateBehaviour.SurvivorStates;
using AmogAI.SteeringBehaviour;
using System.Drawing;

public class Survivor : MovingEntity {
    public SurvivorTaskGoal SurvivorTaskGoal { get; set; }
    public SurvivorStateMachine SurvivorStateMachine { get; set; }
    public float Health { get; set; }
    public Objective? CurrentObjective { get; set; }
    public float ObjectiveProgress { get; set; }
    public float SeekTimer { get; private set; }
    public float SeekingForObjectiveTime { get; set; }
    public bool IsDoingTask { get; set; }
    public const float decisionInterval = 1000f;
    public float decisionIntervalDelta = 0;

    public Survivor(Vector pos, World world) : base(pos, world) {
        Velocity = new Vector(0, 0);
        Scale = 10;
        Health = 100;
        SeekTimer = 4000;
        
        SurvivorTaskGoal = new SurvivorTaskGoal(this);
        SurvivorStateMachine = new SurvivorStateMachine(this);
    }

    public Survivor(Vector pos, MovingEntity target, World world) : this(pos, world) {
        Target = target;
    }

    public void SetObjective(Objective objective) {
        CurrentObjective = objective;
    }

    public void StartCurrentTask() {
        if (CurrentObjective != null) {
            IsDoingTask = true;
            CurrentObjective.StartTask();
        }
    }

    public override void Update(float timeDelta) {
        if (Health <= 0)
            return;

        if (IsDoingTask && CurrentObjective != null) {
            ObjectiveProgress += timeDelta;
            if (ObjectiveProgress >= CurrentObjective.Duration) {
                CurrentObjective.EndTask();
                IsDoingTask = false;
            }

            return;
        }

        base.Update(timeDelta);
        SurvivorStateMachine.Update(timeDelta);
    }

    public Objective CalculateNearestObjective() {
        return World.Objectives
            .OrderBy(objective => (Position - objective.Position).Length())
            .Where(objective => !objective.IsDone)
            .First();
    }

    public float CalculateDistanceBetweenNearestKillerAndPotentialObjective(Objective objective) {
        return World.Killers
            .OrderBy(killer => (Position - killer.Position).Length())
            .First()
            .Position
            .Distance(objective.Position);
    }

    public void ResetObjective() {
        CurrentObjective = null;
        ObjectiveProgress = 0f;
        IsDoingTask = false;
    }

    public void ResetSeekingForObjectiveTime() {
        SeekingForObjectiveTime = 0f;
    }   

    public override void Render(Graphics g) {
        float entityX = Position.X - Scale;
        float entityY = Position.Y - Scale;
        float size = Scale * 2;

        // Draw the entity
        Pen p = new Pen(Color.Blue, 1);
        g.DrawEllipse(p, entityX, entityY, size, size);
    }

    public override void RenderOverlay(Graphics g) {
        Pen p = new Pen(Color.Blue, 1);
        
        // Draw the velocity vector
        g.DrawLine(p,
            Position.X,
            Position.Y,
            Position.X + Velocity.X * 80,
            Position.Y + Velocity.Y * 80);

        if (PathFollowBehaviour.Destination == null) { // TODO: No pathfollow means
            // Draw the wander circle and target
            Vector circleCenter = Heading.Clone().Normalize() * SteeringBehaviour.WanderDistance + Position;
            float circleX = circleCenter.X - SteeringBehaviour.WanderRadius;
            float circleY = circleCenter.Y - SteeringBehaviour.WanderRadius;
            float sizeRadius = SteeringBehaviour.WanderRadius * 2;
            g.DrawEllipse(p, circleX, circleY, sizeRadius, sizeRadius);

            float targetX = Position.X + (SteeringBehaviour.WanderTarget.X - Scale);
            float targetY = Position.Y + (SteeringBehaviour.WanderTarget.Y - Scale);
            float sizeTarget = Scale * 2;
            Pen p2 = new Pen(Color.Red, 1);
            g.DrawEllipse(p2, targetX, targetY, sizeTarget, sizeTarget);

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
}
