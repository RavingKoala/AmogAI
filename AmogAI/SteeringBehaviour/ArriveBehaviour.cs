namespace AmogAI.SteeringBehaviour;

using AmogAI.World.Entity;

public enum Deceleration {
	slow = 3,
	normal = 2,
	fast = 1
}

public class ArriveBehaviour {
	/// <summary>
	/// Method to gradually arrive to goal.
	/// </summary>
	/// <param name="self">IEntity</param>
	/// <param name="goal">IEntity</param>
	/// <returns>Vector</returns>
	public static Vector Calculate(IEntity self, IEntity goal, Deceleration deceleration) {
		Vector ToTarget = goal.Position - self.Position;
		float distance = ToTarget.Length();

		if (distance > 0) {
			float DecelerationTweaker = 0.3f;
			float speed = distance / ((float)deceleration * DecelerationTweaker);

			if (speed > self.MaxSpeed) {
				speed = self.MaxSpeed;
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
