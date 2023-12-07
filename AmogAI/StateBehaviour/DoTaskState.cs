namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class DoTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.StartCurrentTask();
    }

    public void Execute(Survivor survivor) {
        //if (survivor.CurrentObjective.Timer.Enabled)
        //    Console.WriteLine("timer is working");

        if (survivor.CurrentObjective.IsDone) {
            Console.WriteLine("changing to seektaskstate");
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new SeekTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.CurrentObjective = null;
    }
}
