namespace AmogAI.SteeringBehaviour;
public class QueueBehaviour : ISteeringBehaviour {
	public Vector Calculate(IEntity self, IEntity goal) {
		return Calculator.NormalizeVector(((goal.Position - (Calculator.NormalizeVector(goal.velocity) * goal.max_speed)) - self.Position) * self.max_speed) - self.velocity;
	}
}
