namespace AmogAI.SteeringBehaviour;

public class FleeBehaviour : ISteeringBehaviour {
	/// <summary>
	/// Method to move away from goal
	/// </summary>
	/// <param name="self">IEntity</param>
	/// <param name="goal">IEntity</param>
	/// <returns>Vector</returns>
	public Vector Calculate(IEntity self, IEntity goal) {
		return (Calculator.NormalizeVector(self.Position - goal.Position) * self.max_speed) - self.velocity;
	}
}
