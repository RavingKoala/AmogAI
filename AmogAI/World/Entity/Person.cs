namespace AmogAI.World.Entity;

using AmogAI.SteeringBehaviour;
using System.Drawing;

public class Person : MovingEntity {
    //public void InitialRender(Graphics g) {
    //}

    //public void Update(float delta) {
    //	//var steeringForce = Vector2.Zero;
    //	//steeringForce += Seek.Calculate(steeringForce, GlobalPosition, _player.GlobalPosition, MaxSpeed);
    //	//steeringForce += ObstacleAvoidance.Calculate(steeringForce, _rayCastPivot, MaxSpeed);

    //	//GetNode<RayCast2D>("Velocity").CastTo = steeringForce;

    //	//Velocity += steeringForce;
    //	//Velocity = Velocity.Truncate(MaxSpeed);
    //	//Velocity = MoveAndSlide(Velocity);
    //}

    public Person(Vector pos, MovingEntity target) : base(pos, target) {
        Velocity = new Vector(0, 0);
        Scale = 10;
        MaxForce = 20;
    }
    
    public Person(Vector pos) : base(pos) {
        Velocity = new Vector(0, 0);
        Scale = 10;
        MaxForce = 20;
    }

    public override void Render(Graphics g) {
        double leftCorner = Position.X - Scale;
        double rightCorner = Position.Y - Scale;
        double size = Scale * 2;

        Pen p = new Pen(Color.Brown, 1);
        g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        g.DrawLine(p,
            Position.X,
            Position.Y,
            Position.X + Velocity.X * 2,
            Position.Y + Velocity.Y * 2);
    }
}
