namespace AmogAI.UnitTests;

using System;
using AmogAI.SteeringBehaviour;
using NUnit.Framework;

[TestFixture]
public class SteeringBehaviourTests {
    private bool temp;

    [SetUp]
    public void Setup() {
        temp = true;
    }

    [Test]
    public void VectorAdd() {
        Vector vec1 = new Vector(7, 7);
        Vector vec2 = new Vector(3, 4);

        Vector result = vec1 + vec2;
        Vector expectedResult = new Vector(10, 11);

        Assert.That(result.X, Is.EqualTo(expectedResult.X));
        Assert.That(result.Y, Is.EqualTo(expectedResult.Y));
    }

    [Test]
    public void VectorSubtract() {
        Vector vec1 = new Vector(7, 7);
        Vector vec2 = new Vector(3, 4);

        Vector result = vec1 - vec2;
        Vector expectedResult = new Vector(4, 3);

        Assert.That(result.X, Is.EqualTo(expectedResult.X));
        Assert.That(result.Y, Is.EqualTo(expectedResult.Y));
    }

    [Test]
    public void VectorDivide() {
        Vector vec1 = new Vector(8, 12);
        float f = 2;

        Vector result = vec1 / f;
        Vector expectedResult = new Vector(4, 6);

        Assert.That(result.X, Is.EqualTo(expectedResult.X));
        Assert.That(result.Y, Is.EqualTo(expectedResult.Y));
    }

    [Test]
    public void VectorMultiply() {
        Vector vec1 = new Vector(8, 12);
        float f = 2;

        Vector result = vec1 * f;
        Vector expectedResult = new Vector(16, 24);

        Assert.That(result.X, Is.EqualTo(expectedResult.X));
        Assert.That(result.Y, Is.EqualTo(expectedResult.Y));
    }
}
