namespace AmogAI.World.Entity;

using AmogAI.StateBehaviour.KillerStates;
using AmogAI.StateBehaviour.SurvivorStates;
using AmogAI.SteeringBehaviour;

public class Killer : MovingEntity {
    public Survivor? target;
    public KillerStateMachine stateMachine;

    public Killer(Vector pos, World world) : base(pos, world) {
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
    
    }
}
