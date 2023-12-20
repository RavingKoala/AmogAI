namespace AmogAI.StateBehaviour.WordStates;

using AmogAI.StateBehaviour.SurvivorStates;
using AmogAI.World;
using AmogAI.World.Entity;

public class GlobalEmergencyState : IState<World> {
    public void Enter(World world) {
        foreach (Survivor survivor in world.Survivors) {
            if (survivor.CurrentObjective != null)
                survivor.CurrentObjective.IsInProgress = false;

            survivor.ResetObjective();
            survivor.ResetSeekingForObjectiveTime();
            survivor.PathFollowBehaviour.ClearPath();

            survivor.SurvivorStateMachine.ChangeStateMachine(new EmergencyStateMachine(survivor));
        }
    }

    public void Execute(World world, float timeDelta) {
        if (!world.IsEmergencyHappening) {
            world.GlobalStateMachine.ChangeState(new GlobalTaskState());
        }
    }

    public void Exit(World world) {
    }
}
