namespace AmogAI.SteeringBehaviour;

public enum Deceleration {
	slow = 3,
	normal = 2,
	fast = 1
}

public class ArriveBehaviour : ISteeringBehaviour {
	/// <summary>
	/// Method to gradually arrive to goal.
	/// </summary>
	/// <param name="self">IEntity</param>
	/// <param name="goal">IEntity</param>
	/// <returns>Vector</returns>
	public Vector Calculate(IEntity self, IEntity goal, Deceleration deceleration) {
		Vector ToTarget = goal.Position - self.Position;
		float distance = Calculator.GetVectorLength(ToTarget);

		if (distance > 0) {
			float DecelerationTweaker = 0.3f;
			float speed = distance / ((float)deceleration * DecelerationTweaker);

			if (speed > self.max_speed) {
				speed = self.max_speed;
			}

			Vector DesiredVelocity = ToTarget * speed / distance;

			return DesiredVelocity;
		}

		return new Vector();
	}

	public Vector Calculate(IEntity self, IEntity goal) {
		return Calculate(self, goal, Deceleration.normal);
	}
}
