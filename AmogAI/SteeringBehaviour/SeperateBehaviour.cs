namespace AmogAI.SteeringBehaviour;

public class SeperateBehaviour : ISteeringBehaviour {
	/// <summary>
	/// A method to seperate from goal
	/// </summary>
	/// <param name="self">IEntity</param>
	/// <param name="goal">IEntity</param>
	/// <returns>Vector</returns>
	public Vector Calculate(IEntity self, List<IEntity> neighbours) {
		Vector force = new Vector();
		for (int i = 0; i < neighbours.Count; i++) {
			if (neighbours[i] != self) // "neighbours[i].IsTagged()"???
			{
				Vector ToAgent = self.Position - neighbours[i].Position;
				force += Calculator.NormalizeVector(ToAgent) / Calculator.GetVectorLength(ToAgent);
			}
		}
		return force;
	}

	public Vector Calculate(IEntity self, IEntity goal) {
		List<IEntity> neighbours = new List<IEntity>();
		neighbours.Add(goal);
		return Calculate(self, neighbours);
	}
}
