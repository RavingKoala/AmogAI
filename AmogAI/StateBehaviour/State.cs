namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public interface IState<T> {
    void Enter(T s);
    void Execute(T s);
    void Exit(T s);
}
