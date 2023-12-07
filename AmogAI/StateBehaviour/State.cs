namespace AmogAI.StateBehaviour;

public interface IState<T> {
    void Enter(T owner);
    void Execute(T owner, float timeDelta);
    void Exit(T owner);
}
