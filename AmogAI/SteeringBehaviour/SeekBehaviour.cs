namespace AmogAI.SteeringBehaviour;
public class SeekBehaviour : ISteeringBehaviour {
	/// <summary>
	/// Method to move towards goal
	/// </summary>
	/// <param name="self">IEntity</param>
	/// <param name="goal">IEntity</param>
	/// <returns>Vector</returns>
	public Vector Calculate(IEntity self, IEntity goal) {
		return (Calculator.NormalizeVector(goal.Position - self.Position) * self.max_speed) - self.velocity;
	}
}
