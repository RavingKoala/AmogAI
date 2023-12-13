﻿namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class WalkTowardsTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        if (survivor.CurrentObjective != null)
            survivor.PathFollowBehaviour.SetDestination(survivor.CurrentObjective);
    }

    public void Execute(Survivor survivor, float timeDelta) {
        if (survivor.PathFollowBehaviour.Arrived) {
            Console.WriteLine("changing to dotaskstate");
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new DoTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.PathFollowBehaviour.ClearPath();
    }
}
