namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class DoEmergencyState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        Console.WriteLine("Entering DoEmergencyState");
        survivor.StartCurrentTask();
    }

    public void Execute(Survivor survivor, float timeDelta) {
        if (survivor.CurrentObjective == null) {
            survivor.World.EmergencyHappening = false;
            return;
        }

        if (survivor.CurrentObjective.IsDone)
            survivor.World.EmergencyHappening = false;
    }

    public void Exit(Survivor survivor) {
        survivor.ResetObjective();
        Console.WriteLine("Emergency is over, back to searching for a task!");
    }
}
