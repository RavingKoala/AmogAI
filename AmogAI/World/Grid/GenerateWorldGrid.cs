namespace AmogAI.World.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GenerateWorldGrid {
	public const float NodeSpacing = 40;

	public static Tuple<List<Node>, List<Edge>> Generate(World world) {
		// start node
		// for each node in queue {
		// get neighboring nodes
		// check neightboring nodes for duplicates in existing list
		// if new create edge between them
		// }
	}

	private static List<Node> NodeNeighbours(Node node) {
		List<Node> retList = new List<Node>();
		foreach (var xDir in new int[] { -1, 1 })
			foreach (var yDir in new int[] { -1, 1 })
				retList.Add(new Node(node.Position.X + xDir * NodeSpacing, node.Position.Y + yDir * NodeSpacing));

		return retList;
	}
}
