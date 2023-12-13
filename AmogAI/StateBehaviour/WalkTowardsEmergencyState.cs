namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class WalkTowardsEmergencyState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.CurrentObjective = survivor.World.EmergencyObjective;
        survivor.PathFollowBehaviour.SetDestination(survivor.CurrentObjective);
    }

    public void Execute(Survivor survivor, float timeDelta) {
        if (survivor.PathFollowBehaviour.Arrived)
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new DoEmergencyState());
    }

    public void Exit(Survivor survivor) {
        survivor.PathFollowBehaviour.ClearPath();
        Console.WriteLine("Gonna start working on the emergency!");
    }
}
