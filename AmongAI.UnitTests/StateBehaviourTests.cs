using System;
using AmogAI.StateBehaviour;
using AmogAI.StateBehaviour.SurvivorStates;
using AmogAI.StateBehaviour.WordStates;
using AmogAI.SteeringBehaviour;
using AmogAI.World;
using AmogAI.World.Entity;
using NUnit.Framework;

namespace AmogAI.UnitTests;

public class StateOwner 
{
    public OwnerStateMachine StateMachine;

    public StateOwner()
    {
         StateMachine = new OwnerStateMachine(this);
    }
}

public class OwnerStateMachine : StateMachine<StateOwner>
{
    public OwnerStateMachine(StateOwner owner) : base(owner)
    {
        CurrentState = new State1();
        CurrentState.Enter(owner);
    }
}

public class State1 : IState<StateOwner>
{
    public void Enter(StateOwner owner) { }
    public void Execute(StateOwner owner, float timeDelta) { }
    public void Exit(StateOwner owner) { }
}

public class State2 : IState<StateOwner>
{
    public void Enter(StateOwner owner) { }
    public void Execute(StateOwner owner, float timeDelta) { }
    public void Exit(StateOwner owner) { }
}

[TestFixture]
public class StateBehaviourTests {
    [Test]
    public void EnterState()
    {
        StateOwner stateOwner = new StateOwner();

        stateOwner.StateMachine.ChangeState(new State2());

        Assert.That(stateOwner.StateMachine.CurrentState?.GetType(), Is.EqualTo(typeof(State2)));
    }
}
