namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;
using System.Diagnostics.Eventing.Reader;

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
            Console.WriteLine("changing to seektaskstate");
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new SeekTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.CurrentObjective = null;
    }
}
