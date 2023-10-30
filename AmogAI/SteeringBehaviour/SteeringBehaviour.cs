namespace AmogAI.SteeringBehaviour;

using AmogAI.World.Entity;
using System;

public enum BehaviourType {
    Seek = 0,
    Pursuit = 1,
    Wander = 2,
    WallAvoidance = 3
}

public class SteeringBehaviour {
    public MovingEntity Entity { get; set; }
    public Vector SteeringForce { get; set; }
    public Vector WanderTarget { get; set; }
    public Vector[] Feelers { get; set; }
    public float WanderRadius { get; set; }
    public float WanderDistance { get; set; }
    public float WanderJitter { get; set; }
    public float LastJitterAngle { get; set; }
    public float WeightSeek { get; set; } // 0
    public float WeightPursuit { get; set; } // 1
    public float WeightWander { get; set; } // 2
    public float WeightWallAvoidance { get; set; } // 3
    private bool[] _behaviours;

    public SteeringBehaviour(MovingEntity entity) {
        SteeringForce = new Vector();
        Entity = entity;
        _behaviours = new bool[4];
        Feelers = new Vector[3];

        WanderRadius = 15f;
        WanderDistance = 40f;
        WanderJitter = 1f;
        LastJitterAngle = 0f;

        WeightSeek = 1f;
        WeightPursuit = 1f;
        WeightWander = 1f;
        WeightWallAvoidance = 1f;
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
            SteeringForce += SteeringForce + Seek(Entity.Target.Position) * WeightSeek;

        if (On(BehaviourType.Pursuit))
            SteeringForce += Pursuit(Entity.Target) * WeightPursuit;

        if (On(BehaviourType.Wander))
            SteeringForce += Wander() * WeightWander;

        if (On(BehaviourType.WallAvoidance))
            SteeringForce += WallAvoidance(Entity.World.Walls) * WeightWallAvoidance;

        return SteeringForce.Truncate(Entity.MaxForce);
    }

    private void CreateFeelers() {

    }

    public Vector Seek(Vector targetPos) {
        Vector desiredVelocity = (targetPos - Entity.Position).Normalize() * Entity.MaxSpeed;
        return desiredVelocity - Entity.Velocity;
    }

    public Vector Pursuit(MovingEntity target) {
        Vector toEvader = target.Position - Entity.Position;
        float RelativeHeading = Entity.Position.Dot(target.Heading);

        if (toEvader.Dot(Entity.Heading) > 0 && RelativeHeading < -0.95)
            return Seek(target.Position);

        float lookAheadTime = toEvader.Length() / (Entity.MaxSpeed + target.Velocity.Length());
        return Seek(target.Position + target.Velocity * lookAheadTime);
    }

    public Vector Wander() {
        float jitterThisTimeSlice = WanderJitter * Entity.TimeElapsed;

        Random r = new Random();

        var jitterAngleOffset = ((float)r.NextDouble() < 0.5 ? -1 : 1) * jitterThisTimeSlice;
        LastJitterAngle += jitterAngleOffset;

        float theta = LastJitterAngle / (2 * (float)Math.PI);
        WanderTarget = new Vector(WanderRadius * (float)Math.Cos(theta), WanderRadius * (float)Math.Sin(theta));

        if (Entity.Heading.LengthSquared() == 0)
            Entity.Heading = new Vector(0.000001f, 0);

        WanderTarget += Entity.Heading.Clone().Normalize() * WanderDistance;

        return WanderTarget;
    }

    public Vector WallAvoidance(List<Wall> walls) {
        CreateFeelers();

        // write the lineintersection method

        return new Vector();
    }
}
