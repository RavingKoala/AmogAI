namespace AmogAI.World.Entity;

using AmogAI.StateBehaviour.KillerStates;
using AmogAI.StateBehaviour.SurvivorStates;
using AmogAI.SteeringBehaviour;

public class Killer : MovingEntity {
    public KillerStateMachine StateMachine;
    public float DetectionRadius; //px
    public float DetectionConeAngle; //
    public float DetectionConeDistance; //0-360
	private float AttackPower; //hp
	private float AttackDistance; //px
	private float AttackCooldown; //px
	private bool OnAttackCooldown; //ms
    private float DeltaAttackCooldown;

	public Killer(Vector pos, World world) : base(pos, world) {
        Velocity = new Vector(0, 0);
        Scale = 10;
        DetectionRadius = 30;
        DetectionConeAngle = 40;
        DetectionConeDistance = 80;
        AttackDistance = 20;
        AttackCooldown = 400;
        OnAttackCooldown = true;
        AttackPower = 20;
        AttackCooldown = 1000;
        DeltaAttackCooldown = 1000;

        Target = null;
        StateMachine = new KillerStateMachine(this);
    }

    public override void Update(float timeDelta) {
        if (OnAttackCooldown && DeltaAttackCooldown < AttackCooldown) {
            DeltaAttackCooldown += timeDelta;
            return;
        }

        base.Update(timeDelta);
        StateMachine.Update(timeDelta);

        if (StateMachine.CurrentState?.GetType() != typeof(KillState)
            || Target == null
            || Target.GetType() != typeof(Survivor))
            return;

        Survivor target = Target as Survivor;

        if (Position.Distance(target.Position) < AttackDistance) {
            target.Health -= AttackPower;
            if (target.Health <= 0) {
                World.Survivors.Remove(target);
                Target = null;
            }
            OnAttackCooldown = true;
            DeltaAttackCooldown = 0;
        }
    }

    public override void Render(Graphics g) {
        float entityX = Position.X - Scale;
        float entityY = Position.Y - Scale;
        float size = Scale * 2;

        // Draw the entity
        Pen p = new Pen(Color.DarkRed, 1);
        g.DrawEllipse(p, entityX, entityY, size, size);
    }

    public override void RenderOverlay(Graphics g) {
        Pen p = new Pen(Color.DarkRed, 1);
        
        // Draw the velocity vector
        g.DrawLine(p,
            Position.X,
            Position.Y,
            Position.X + Velocity.X * 80,
            Position.Y + Velocity.Y * 80);

        if (SteeringBehaviour.On(BehaviourType.Wander)) {
            // Draw the wander circle and target
            Vector circleCenter = Heading.Clone().Normalize() * SteeringBehaviour.WanderDistance + Position;
            float circleX = circleCenter.X - SteeringBehaviour.WanderRadius;
            float circleY = circleCenter.Y - SteeringBehaviour.WanderRadius;
            float sizeDiameter = SteeringBehaviour.WanderRadius * 2;
            g.DrawEllipse(p, circleX, circleY, sizeDiameter, sizeDiameter);

            float targetX = Position.X + (SteeringBehaviour.WanderTarget.X - Scale);
            float targetY = Position.Y + (SteeringBehaviour.WanderTarget.Y - Scale);
            float sizeTarget = Scale * 2;
            Pen p2 = new Pen(Color.Red, 1);
            g.DrawEllipse(p2, targetX, targetY, sizeTarget, sizeTarget);
        }

        if (StateMachine.CurrentState?.GetType() == typeof(WanderState)) {
            Pen p2 = new Pen(Color.Yellow, 1);

            float circleX = Position.X - DetectionRadius;
            float circleY = Position.Y - DetectionRadius;
            float detectDiameter = DetectionRadius * 2;
            g.DrawEllipse(p2, circleX, circleY, detectDiameter, detectDiameter);
        }

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
