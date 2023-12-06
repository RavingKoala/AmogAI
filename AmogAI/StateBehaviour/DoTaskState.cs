namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class DoTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.StartCurrentTask();
    }

    public void Execute(Survivor survivor) {
        // wait at task location until task is done (its timer is up)

        //Console.WriteLine("checking if task is done");
        if (survivor.CurrentObjective.IsDone) {
            Console.WriteLine("changing to seektaskstate");
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new SeekTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.CurrentObjective = null;
    }
}
