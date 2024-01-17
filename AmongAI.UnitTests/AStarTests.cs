namespace AmogAI.UnitTests;

using System;
using AmogAI.World;
using AmogAI.SteeringBehaviour;
using NUnit.Framework;

[TestFixture]
public class AStarTests {
    private World world;

    [SetUp]
    public void Setup() {
        world = new World();

    }
}