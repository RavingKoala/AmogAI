namespace AmogAI.StateBehaviour;

using AmogAI.World;
using AmogAI.World.Entity;

public class GlobalEmergencyState : IState<World> {
    public void Enter(World world) {
        foreach (var entity in world.MovingEntities) {
            if (entity.GetType() == typeof(Survivor)) {
                Survivor survivor = (Survivor)entity;
                survivor.SurvivorStateMachine.ChangeStateMachine(new EmergencyStateMachine(survivor));
            }
        }
    }

    public void Execute(World world, float timeDelta) {
        if (!world.EmergencyHappening) {
            world.GlobalStateMachine.ChangeState(new GlobalTaskState());
        }

        //foreach (var entity in world.MovingEntities) {
        //    if (entity.GetType() == typeof(Survivor)) {
        //        Survivor survivor = (Survivor)entity;
        //        if (survivor.SurvivorStateMachine.StateMachine.CurrentState != null)
        //            if (survivor.SurvivorStateMachine.StateMachine.GetType() == typeof(EmergencyStateMachine))
        //                Console.WriteLine("in emergencystate");
        //    }
        //}
    }

    public void Exit(World world) {
        Console.WriteLine("Emergency is over!");
    }
}
