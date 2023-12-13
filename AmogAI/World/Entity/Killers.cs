namespace AmogAI.World.Entity;

using AmogAI.SteeringBehaviour;

public class Killers : MovingEntity {
    public Survivor? target;

    Killers(Vector pos, World world) : base(pos, world) {
        target = null;
    }


}
