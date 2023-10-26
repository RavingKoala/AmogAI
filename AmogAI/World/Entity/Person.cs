﻿namespace AmogAI.World.Entity;

using AmogAI.FuzzyLogic;
using AmogAI.SteeringBehaviour;

public class Person : IEntity {
	public Vector Position { get; set; }
	public float MaxSpeed { get; set; }
	public Vector Velocity { get; set; }
	public float Health { get; set; }
	public int TasksLeft { get; set; }
	public SurvivorTaskGoal SurvivorTaskGoal { get; set; }

	public void InitialDraw(Graphics g) {
		SurvivorTaskGoal = new SurvivorTaskGoal(this);
	}

	public void Redraw(Graphics g) {
		g.DrawEllipse(new Pen(Brushes.Black, 10), (int)MainFrame.WindowCenter.X - 55, (int)MainFrame.WindowCenter.Y - 55, 100, 100);
	}

	public void Update(float delta) {
		//var steeringForce = Vector2.Zero;
		//steeringForce += Seek.Calculate(steeringForce, GlobalPosition, _player.GlobalPosition, MaxSpeed);
		//steeringForce += ObstacleAvoidance.Calculate(steeringForce, _rayCastPivot, MaxSpeed);

		//GetNode<RayCast2D>("Velocity").CastTo = steeringForce;

		//Velocity += steeringForce;
		//Velocity = Velocity.Truncate(MaxSpeed);
		//Velocity = MoveAndSlide(Velocity);
	}
}
