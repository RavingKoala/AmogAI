namespace AmogAI.World.Entity;

using AmogAI.StateBehaviour.KillerStates;
using AmogAI.StateBehaviour.SurvivorStates;
using AmogAI.SteeringBehaviour;

public class Killer : MovingEntity {
    public Survivor? target;
    public KillerStateMachine stateMachine;

    Killer(Vector pos, World world) : base(pos, world) {
        target = null;
        stateMachine = new KillerStateMachine(this);
    }

    public override void Update(float timeDelta) {
        base.Update(timeDelta);
        stateMachine.Update(timeDelta);
    }
}
