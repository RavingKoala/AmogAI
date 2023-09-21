namespace AmogAI.World.Entity;

using AmogAI.SteeringBehaviour;

public interface IEntity {
	public Vector Position { get; set; }
	public float MaxSpeed { get; set; }
	public Vector Velocity { get; set; }

	public void InitialDraw(Graphics g);
	public void Update(float timeDelta);
	public void Redraw(Graphics g);
}
