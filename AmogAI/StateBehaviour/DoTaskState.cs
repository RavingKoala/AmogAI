namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class DoTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.CurrentObjective.StartTask();
    }

    public void Execute(Survivor survivor) {
        // wait at task location until task is done (its timer is up)

        if (survivor.CurrentObjective.IsDone) {
            Console.WriteLine("changing to seektaskstate");
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new SeekTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.CurrentObjective = null;
    }
}
