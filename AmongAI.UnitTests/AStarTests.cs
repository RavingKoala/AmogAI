namespace AmogAI.UnitTests;

using System;
using AmogAI.World;
using AmogAI.AStar;
using NUnit.Framework;

[TestFixture]
public class AStarTests {
    //private World world;

    private List<Node> _nodes;
    private List<Edge> _edges;
    private const int nodeSpacing = 10;
    private const int gridSize = 5;

    [SetUp]
    public void Setup() {
        //world = new World();

        _nodes = new List<Node>();
        _edges = new List<Edge>();

        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                _nodes.Add(new Node(x * nodeSpacing, y * nodeSpacing));
                if (x > 0)
                    _edges.Add(new Edge(_nodes[(y*gridSize)+x-1], _nodes[(y*gridSize)+x]));
                if (y > 0)
                    _edges.Add(new Edge(_nodes[((y-1)*gridSize)+x], _nodes[(y*gridSize)+x]));
            }
        }
    }

    [Test]
    public void AStar_FindPath0x0To3x4() {
        Node node0x0 = _nodes[0];
        Node node4x3 = _nodes[(4*gridSize)+3];

        Queue<Node>? result = AStar.FindPath(node0x0, node4x3, _edges);
        Queue<Node>? expectedResult = new Queue<Node>( new Node[] { node0x0, _nodes[(1*gridSize)+0], _nodes[(1*gridSize)+1], _nodes[(2*gridSize)+1], _nodes[(3*gridSize)+1], _nodes[(3*gridSize)+2], _nodes[(4*gridSize)+2], node4x3 });

        Assert.That(result, Is.EqualTo(expectedResult));
    }
}