namespace AmogAI.StateBehaviour.WordStates;

using AmogAI.StateBehaviour.SurvivorStates;
using AmogAI.World;
using AmogAI.World.Entity;

public class GlobalTaskState : IState<World> {
    public void Enter(World world) {
        foreach (Survivor survivor in world.Survivors) {
            if (survivor.CurrentObjective != null) 
                survivor.CurrentObjective.IsInProgress = false;

            survivor.ResetObjective();
            survivor.ResetSeekingForObjectiveTime();
            survivor.PathFollowBehaviour.ClearPath();

            survivor.SurvivorStateMachine.ChangeStateMachine(new TaskStateMachine(survivor));
        }
    }

    public void Execute(World world, float timeDelta) {
        if (world.EmergencyHappening) {
            world.GlobalStateMachine.ChangeState(new GlobalEmergencyState());
        }

        //foreach (var entity in world.MovingEntities) {
        //    if (entity.GetType() == typeof(Survivor)) {
        //        Survivor survivor = (Survivor)entity;
        //        if (survivor.SurvivorStateMachine.StateMachine.CurrentState != null)
        //            if (survivor.SurvivorStateMachine.StateMachine.GetType() == typeof(TaskStateMachine))
        //                Console.WriteLine("in taskstate");
        //    }
        //}
    }

    public void Exit(World world) {
        Console.WriteLine("There's an emergency!");
    }
}
