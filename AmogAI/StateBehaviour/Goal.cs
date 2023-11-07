namespace AmogAI.StateBehaviour;

public abstract class Goal<T> {
    public string Name { get; protected set; }
    public GoalStates State { get; protected set; }
    public abstract void Activate();
    public abstract int Process();
    public abstract void Terminate();
    public abstract bool HandleMessage(string message);
}
