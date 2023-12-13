namespace AmogAI.StateBehaviour.SurvivorStates;

using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class SeekTaskState : IState<Survivor> {
    private bool _taskAssigned = false;
    private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

    public void Enter(Survivor survivor) {
        survivor.SteeringBehaviour.TurnOn(BehaviourType.Wander);
        survivor.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);

        _timer.Interval = 2000;
        _timer.Tick += (sender, e) => {
            _taskAssigned = true;
            _timer.Stop();
        };
        _timer.Start();
    }

    public void Execute(Survivor survivor, float timeDelta) {
        // wander for a while until desirability of a task is high enough to assign
        if (_taskAssigned) {
            _timer.Dispose();
            survivor.SetObjective(1);
            Console.WriteLine("changing to walktowardstaskstate");
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new WalkTowardsTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.SteeringBehaviour.TurnOff(BehaviourType.Wander);
    }
}
