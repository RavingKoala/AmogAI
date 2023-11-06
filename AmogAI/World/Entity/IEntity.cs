namespace AmogAI.World.Entity;

using AmogAI.SteeringBehaviour;

public interface IEntity {
	public Vector Position { get; set; }

	public void InitialRender(Graphics g);
}
