using AmogAI.SteeringBehaviour;

namespace AmogAI.StateBehaviour;

public class Goal_Flee : Goal<Mario> {
	public Character Enemy { get; set; }
	public Goal_Flee(Character enemy) {
		if (enemy is Mario) {
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
		ISteeringBehaviour behaviour = new FleeBehaviour();

		//behaviour.Calculate(Enemy);

		return (int)State;
	}

	public override void Terminate() {
		State = GoalStates.Completed;
	}
}
