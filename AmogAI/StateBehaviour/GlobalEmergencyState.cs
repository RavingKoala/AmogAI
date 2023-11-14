namespace AmogAI.StateBehaviour;

using AmogAI.World;
using AmogAI.World.Entity;

public class GlobalEmergencyState : IState<World> {
    public void Enter(World world) {
        // move SurvivorStateMachine's statemachine to EmergencyStateMachine
        foreach (var entity in world.MovingEntities) {
            if (entity.GetType() == typeof(Survivor)) {
                Survivor survivor = (Survivor)entity;
                survivor.SurvivorStateMachine.ChangeStateMachine(new EmergencyStateMachine<Survivor>(survivor));
            }
        }
        //world.Stopwatch.Start();
    }

    public void Execute(World world) {
        // start timer for the emergency and how long before it fails
        //if (world.Stopwatch.Elapsed.TotalSeconds >= 10) { }
        //{
        //    world.Stopwatch.Reset();
        //    world.GlobalStateMachine.ChangeState(new GlobalEmergencyState());
        //}
        if (!world.EmergencyHappening) {
            world.GlobalStateMachine.ChangeState(new GlobalTaskState());
        }

        foreach (var entity in world.MovingEntities) {
            if (entity.GetType() == typeof(Survivor)) {
                Survivor survivor = (Survivor)entity;
                if (survivor.SurvivorStateMachine.StateMachine.CurrentState != null)
                    if (survivor.SurvivorStateMachine.StateMachine.GetType() == typeof(EmergencyStateMachine<Survivor>))
                        Console.WriteLine("in emergencystate");
            }
        }
    }

    public void Exit(World world) {
        // if emergency is completed -> move to GlobalTaskState and RevertToPreviousState
        // if emergency is failed -> game is over
        Console.WriteLine("Emergency is over!");
    }
}
