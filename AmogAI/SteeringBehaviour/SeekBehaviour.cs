namespace AmogAI.SteeringBehaviour;

using AmogAI.World.Entity;

public class SeekBehaviour {
	/// <summary>
	/// Method to move towards goal
	/// </summary>
	/// <param name="self">IEntity</param>
	/// <param name="goal">IEntity</param>
	/// <returns>Vector</returns>
	public static Vector Calculate(IEntity self, IEntity goal) {
		return ((goal.Position - self.Position).Normalize() * self.MaxSpeed) - self.Velocity;
	}
}
