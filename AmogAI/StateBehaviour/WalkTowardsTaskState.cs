﻿namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;

public class WalkTowardsTaskState : IState<Survivor> {
    public void Enter(Survivor survivor) {
        survivor.SteeringBehaviour.WeightWallAvoidance = 0.01f; 
        survivor.PathFollowBehaviour.SetDestination(survivor.CurrentObjective);
    }

    public void Execute(Survivor survivor) {
        if (survivor.PathFollowBehaviour.Arrived) {
            Console.WriteLine("changing to dotaskstate");
            survivor.SurvivorStateMachine.StateMachine.ChangeState(new DoTaskState());
        }
    }

    public void Exit(Survivor survivor) {
        survivor.PathFollowBehaviour.ClearPath();
    }
}
