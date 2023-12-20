namespace AmogAI.StateBehaviour.SurvivorStates;

using AmogAI.World.Entity;

public class DoTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.StartCurrentTask();
    }

    public void Execute(Survivor survivor, float timeDelta) {
        if (survivor.CurrentObjective == null) {
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new SeekTaskState());
            return;
        }

        if (survivor.CurrentObjective.IsDone) {
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new SeekTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.ResetObjective();
    }
}
