namespace AmogAI.StateBehaviour;
public abstract class CompositeGoal<T> : Goal<T> {
    public Stack<Goal<T>> Subgoals;

    public CompositeGoal(string name, Stack<Goal<T>> subgoals) {
        Name = name;
        Subgoals = subgoals;
    }

    public CompositeGoal(string name, Goal<T> subgoal) {
        Name = name;
        Subgoals = new Stack<Goal<T>>();
        AddSubgoal(subgoal);
    }

    public override bool HandleMessage(string message) {
        bool result = Subgoals.Peek().HandleMessage(message);
        if (!result) {
            Console.WriteLine(message);
        }
        return false;
    }

    public int ProcessSubGoals() {
        while (
            Subgoals.Count() > 0 &&
            (
                Subgoals.Peek().State == GoalStates.Completed ||
                Subgoals.Peek().State == GoalStates.Failed
            )) {
            Subgoals.Pop().Terminate();
        }

        if (Subgoals.Count > 0) {
            int StatusOfSubGoals = Subgoals.Peek().Process();

            if (StatusOfSubGoals == (int)GoalStates.Completed && Subgoals.Count > 1) {
                return (int)GoalStates.Active;
            }

            return StatusOfSubGoals;
        }

        return (int)GoalStates.Completed;
    }

    public void RemoveAllSubGoals() {
        foreach (Goal<T> g in Subgoals) {
            g.Terminate();
        }
        Subgoals.Clear();
    }

    public void AddSubgoal(Goal<T> goal) {
        Subgoals.Push(goal);
    }
}
