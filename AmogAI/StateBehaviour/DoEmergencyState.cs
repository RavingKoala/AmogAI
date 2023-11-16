namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class DoEmergencyState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        // start doing emergency
    }

    public void Execute(Survivor survivor) {
        // wait at task location until emergency is done (its timer is up)

        // survivor.World.EmergencyHappening = false;
    }

    public void Exit(Survivor survivor) {
        Console.WriteLine("Emergency is over, back to doing my task!");
    }
}
