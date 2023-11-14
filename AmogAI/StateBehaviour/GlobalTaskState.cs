namespace AmogAI.StateBehaviour;

using AmogAI.World;
using AmogAI.World.Entity;

public class GlobalTaskState : IState<World> {
    private object _lock = new object();
    public void Enter(World world) {
        // move SurvivorStateMachine's statemachine to TaskStateMachine
        foreach (var entity in world.MovingEntities) {
            if (entity.GetType() == typeof(Survivor)) {
                Survivor survivor = (Survivor)entity;
                survivor.SurvivorStateMachine.ChangeStateMachine(new TaskStateMachine<Survivor>(survivor));
            }
        }
        //world.Stopwatch.Start();
    }

    public void Execute(World world) {
        // increment a timer for when an emergency should trigger, then move to GlobalEmergencyState
        //if (world.Stopwatch.Elapsed.TotalSeconds >= 10) { }
        //{
        //    world.Stopwatch.Reset();
        //    world.GlobalStateMachine.ChangeState(new GlobalEmergencyState());
        //}
        if (world.EmergencyHappening) {
            world.GlobalStateMachine.ChangeState(new GlobalEmergencyState());
        }

        foreach (var entity in world.MovingEntities) {
            if (entity.GetType() == typeof(Survivor)) {
                Survivor survivor = (Survivor)entity;
                if (survivor.SurvivorStateMachine.StateMachine.CurrentState != null)
                    if (survivor.SurvivorStateMachine.StateMachine.GetType() == typeof(TaskStateMachine<Survivor>))
                        Console.WriteLine("in taskstate");
            }
        }
    }

    public void Exit(World world) {
        // start an emergency
        Console.WriteLine("Starting emergency!");
    }
}
