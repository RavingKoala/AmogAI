namespace AmogAI.StateBehaviour.SurvivorStates;

using AmogAI.World.Entity;

public class DoEmergencyState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.StartCurrentTask();
    }

    public void Execute(Survivor survivor, float timeDelta) {
        if (survivor.CurrentObjective == null) {
            survivor.World.IsEmergencyHappening = false;
            return;
        }

        if (survivor.CurrentObjective.IsDone)
            survivor.World.IsEmergencyHappening = false;
    }

    public void Exit(Survivor survivor) {
        survivor.ResetObjective();
    }
}
