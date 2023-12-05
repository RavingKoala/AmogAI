namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class WalkTowardsTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.SteeringBehaviour.WeightWallAvoidance = 0.01f; 

        // calculate shortest path to task and assign it to survivor
        survivor.PathFollowBehaviour.SetDestination(survivor.CurrentObjective);
    }

    public void Execute(Survivor survivor) {
        // path towards task
        // if reached task -> transition to DoTaskState
        if (survivor.PathFollowBehaviour.Arrived) {
            Console.WriteLine("changing to dotaskstate");
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new DoTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.PathFollowBehaviour.ClearPath();
    }
}
