namespace AmogAI.SteeringBehaviour;

using AmogAI.World.Entity;
using System;

public enum BehaviourType {
    Seek = 0,
    Pursuit = 1,
    Wander = 2,
}

public class SteeringBehaviour {
    public MovingEntity Entity { get; set; }
    public Vector SteeringForce { get; set; }
    public Vector WanderTarget { get; set; }
    public float WanderRadius { get; set; }
    public float WanderDistance { get; set; }
    public float WanderJitter { get; set; }
    public float WeightSeek { get; set; } // 0
    public float WeightPursuit { get; set; } // 1
    public float WeightWander { get; set; } // 2
    private bool[] _behaviours;

    public SteeringBehaviour(MovingEntity entity) {
        SteeringForce = new Vector();
        Entity = entity;
        _behaviours = new bool[3];

        float theta = ((float)new Random().NextDouble()) * (2 * (float)Math.PI);
        WanderTarget = new Vector(WanderRadius * (float)Math.Cos(theta), WanderRadius * (float)Math.Sin(theta));

        WanderRadius = 3f;
        WanderDistance = 5f;
        WanderJitter = 1f;

        WeightSeek = 1;
        WeightPursuit = 1;
        WeightWander = 1;
    }

    public void TurnOn(BehaviourType behaviour) {
        _behaviours[(int)behaviour] = true;
    }

    public void TurnOff(BehaviourType behaviour) {
        _behaviours[(int)behaviour] = false;
    }

    public bool On(BehaviourType behaviour) {
        return _behaviours[(int)behaviour];
    }

    public Vector Calculate() {
        SteeringForce.Reset();

        if (On(BehaviourType.Seek))
            SteeringForce += Seek(Entity.Target.Position) * WeightSeek;

        if (On(BehaviourType.Pursuit))
            SteeringForce += Pursuit(Entity.Target) * WeightPursuit;

        if (On(BehaviourType.Wander))
            SteeringForce += Wander() * WeightWander;

        return SteeringForce.Truncate(Entity.MaxForce);
    }

    public Vector Seek(Vector targetPos) {
        Vector target = targetPos.Clone();

        Vector desiredVelocity = (target - Entity.Position).Normalize() * Entity.MaxSpeed;
        return desiredVelocity - Entity.Velocity;
    }

    public Vector Pursuit(MovingEntity target) {
        Vector toEvader = target.Position.Clone() - Entity.Position;
        float RelativeHeading = Entity.Position.Dot(target.Heading);

        if (toEvader.Dot(Entity.Heading) > 0 && RelativeHeading < -0.95)
            return Seek(target.Position);

        float lookAheadTime = toEvader.Length() / (Entity.MaxSpeed + target.Velocity.Length());
        return Seek(target.Position.Clone() + target.Velocity.Clone() * lookAheadTime);
    }

    public Vector Wander() {
        float JitterThisTimeSlice = WanderJitter * Entity.TimeElapsed;

        WanderTarget += new Vector((float)(new Random().NextDouble() * 2 - 1) * JitterThisTimeSlice,
                                   (float)(new Random().NextDouble() * 2 - 1) * JitterThisTimeSlice);

        WanderTarget.Normalize();

        WanderTarget *= WanderRadius;

        Vector targetLocal = WanderTarget.Clone() + new Vector(WanderDistance, 0);

        //Vector targetWorld = new Vector(
        //               targetLocal.Clone().X * Entity.Heading.X - targetLocal.Clone().Y * Entity.Heading.Y,
        //               targetLocal.Clone().X * Entity.Heading.Y + targetLocal.Clone().Y * Entity.Heading.X);

        //Vector targetWorld = new Vector(
        //               targetLocal.Clone().X * Entity.Heading.X - targetLocal.Clone().Y * Entity.Heading.Y,
        //               targetLocal.Clone().X * Entity.Side.X + targetLocal.Clone().Y * Entity.Side.Y);

        Vector targetWorld = Vector.PointToWorldSpace(targetLocal, Entity.Heading, Entity.Side, Entity.Position);

        return targetWorld - Entity.Position;
    }
}
