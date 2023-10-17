namespace AmogAI.SteeringBehaviour;

using AmogAI.World.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum BehaviourType {
    Seek = 0,
    Pursuit = 1,
}

public class SteeringBehaviour {
    public MovingEntity Entity { get; set; }
    public Vector SteeringForce { get; set; }
    public float WeightSeek { get; set; } // 0
    public float WeightPursuit { get; set; } // 1
    private bool[] _behaviours;

    public SteeringBehaviour(MovingEntity entity) {
        SteeringForce = new Vector();
        Entity = entity;
        _behaviours = new bool[6];

        WeightSeek = 1;
        WeightPursuit = 1;
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
}
