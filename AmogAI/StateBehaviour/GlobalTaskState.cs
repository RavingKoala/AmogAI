namespace AmogAI.StateBehaviour;

using AmogAI.World;
using AmogAI.World.Entity;

public class GlobalTaskState : IState<World> {
    public void Enter(World world) {
        foreach (var entity in world.MovingEntities) {
            if (entity.GetType() == typeof(Survivor)) {
                Survivor survivor = (Survivor)entity;
                survivor.SurvivorStateMachine.ChangeStateMachine(new TaskStateMachine(survivor));
            }
        }
    }

    public void Execute(World world, float timeDelta) {
        if (world.EmergencyHappening) {
            world.GlobalStateMachine.ChangeState(new GlobalEmergencyState());
            // start an emergency, this should call a method inside World that starts a random emergency
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
