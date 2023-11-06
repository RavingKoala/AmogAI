using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

namespace AmogAI.StateBehaviour;

public class Goal_Flee : Goal<Person> {
	public MovingEntity Enemy { get; set; }
	public Goal_Flee(MovingEntity enemy) {
		if (enemy is Person) {
			throw new Exception("You can't flee from yourself!");
		} else {
			Enemy = enemy;
		}
	}

	public override void Activate() {
		State = GoalStates.Active;
	}

	public override bool HandleMessage(string message) {
		throw new NotImplementedException();
	}

	public override int Process() {
		// implement goal logic

		return (int)State;
	}

	public override void Terminate() {
		State = GoalStates.Completed;
	}
}
