namespace AmogAI.SteeringBehaviour;

using AmogAI.World.Entity;

public class FleeBehaviour {
	/// <summary>
	/// Method to move away from goal
	/// </summary>
	/// <param name="self">IEntity</param>
	/// <param name="goal">IEntity</param>
	/// <returns>Vector</returns>
	public Vector Calculate(IEntity self, IEntity goal) {
		return ((self.Position - goal.Position).Normalize() * self.MaxSpeed) - self.Velocity;
	}
}
