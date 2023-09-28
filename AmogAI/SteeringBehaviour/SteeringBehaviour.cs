namespace AmogAI.SteeringBehaviour;

using AmogAI.World.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum BehaviourType {
    Seek = 0,
}

public class SteeringBehaviour {
    public MovingEntity Entity { get; set; }
    public Vector SteeringForce { get; set; }
    public float WeightSeek { get; set; } // 0
    private bool[] _behaviours;

    public SteeringBehaviour(MovingEntity entity) {
        SteeringForce = new Vector();
        Entity = entity;
        _behaviours = new bool[6];

        WeightSeek = 1;
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

        return SteeringForce.Truncate(Entity.MaxForce);
    }

    public Vector Seek(Vector TargetPos) {
        Vector target = TargetPos.Clone();

        Vector desiredVelocity = (target - Entity.Position).Normalize() * Entity.MaxSpeed;
        return desiredVelocity - Entity.Velocity;
    }

    //public Vector2D Pursuit(MovingEntity Target)
    //{
    //    Vector2D toEvader = Target.Pos.Clone() - Entity.Pos;
    //    double RelativeHeading = Entity.Pos.Clone().Dot(Target.Heading);

    //    if (toEvader.Dot(Entity.Heading) > 0 && RelativeHeading < -0.95)
    //        return Seek(Target.Pos);

    //    // target.velocity.length() is same as target's speed
    //    double lookAheadTime = toEvader.Length() / Entity.MaxSpeed + Target.Velocity.Length();
    //    return Seek(Target.Pos + Target.Velocity * lookAheadTime);
    //}
}
