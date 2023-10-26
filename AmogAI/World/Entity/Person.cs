namespace AmogAI.World.Entity;

using AmogAI.SteeringBehaviour;
using System.Drawing;

public class Person : MovingEntity {
    public Person(Vector pos) : base(pos) {
        Velocity = new Vector(0, 0);
        Scale = 10;
    }

    public Person(Vector pos, MovingEntity target) : this(pos) {
        Target = target;
    }

    public override void Render(Graphics g) {
        double leftCorner = Position.X - Scale;
        double rightCorner = Position.Y - Scale;
        double size = Scale * 2;      
        
        
        double leftCornerRadius = SteeringBehaviour.WanderRadius * leftCorner;
        double rightCornerRadius = SteeringBehaviour.WanderRadius * rightCorner;
        double sizeRadius = Scale * 2;
            
        Pen p = new Pen(Color.Blue, 1);
        g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        //g.DrawEllipse(p, new Rectangle((int) (int)rightCorner, (int)size, (int)size));
        g.DrawLine(p,
            Position.X,
            Position.Y,
            Position.X + Velocity.X * 2,
            Position.Y + Velocity.Y * 2);
    }
}
