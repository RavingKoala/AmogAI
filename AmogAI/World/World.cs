namespace AmogAI.World;

using AmogAI.StateBehaviour;
using AmogAI.AStar;
using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;
using System.Diagnostics;

public class World : IRenderable {
    public bool EmergencyHappening { get; set; }
    public Stopwatch Stopwatch { get; set; }
    public List<MovingEntity> MovingEntities { get; set; }
    public List<Objective> Objectives { get; set; }
    public List<Wall> Walls { get; set; }
    public GlobalStateMachine GlobalStateMachine { get; set; }
    public List<Edge> GridEdges { get; private set; }
    public List<Node> GridNodes { get; private set; }

    public World() {
        Walls = new List<Wall>();
        Objectives = new List<Objective>();
        MovingEntities = new List<MovingEntity>();
        Stopwatch = new Stopwatch();
        GlobalStateMachine = new GlobalStateMachine(this);

        DrawWalls();
        DrawGrid();
        MakeObjectives();
        Populate();
    }

    private void DrawGrid() {
        (GridNodes, GridEdges) = Graph.Generate(this);
    }

    private void MakeObjectives() {
        //var gridDistance = 200;
        //for (int x = 1; x < 5; x++) {
        //    for (int y = 1; y < 4; y++) {
        //        Objectives.Add(new Objective(new Vector(x * gridDistance, y * gridDistance)));
        //    }
        //}
        Objective o1 = new Objective(new Vector(200, 225));

        Objectives.Add(o1);
    }

    private void Populate() {
        Survivor p1 = new Survivor(new Vector(50, 50), this);
        Survivor p2 = new Survivor(new Vector(50, 50), this);
        Survivor p3 = new Survivor(new Vector(50, 50), this);
        Survivor p4 = new Survivor(new Vector(50, 50), this);

        MovingEntities.Add(p1);
        MovingEntities.Add(p2);
        MovingEntities.Add(p3);
        MovingEntities.Add(p4);
    }

    private void DrawWalls() {
        // Outer walls
        Wall leftWall = new Wall(new Vector(0, -1), new Vector(0, 801), false);
        Wall topWall = new Wall(new Vector(-1, 0), new Vector(1351, 0), true);
        Wall bottomWall = new Wall(new Vector(-1, 800), new Vector(1351, 800), false);
        Wall rightWall = new Wall(new Vector(1350, -1), new Vector(1350, 801), true);

        Walls.Add(leftWall);
        Walls.Add(topWall);
        Walls.Add(rightWall);
        Walls.Add(bottomWall);

        // Upper left structure
        Wall ULwall1 = new Wall(new Vector(99, 101), new Vector(499, 101), false);
        Wall ULwall2 = new Wall(new Vector(99, 101), new Vector(99, 351), true);
        Wall ULwall3 = new Wall(new Vector(99, 351), new Vector(499, 351), true);
        Wall ULwall4 = new Wall(new Vector(499, 101), new Vector(499, 151), false);
        Wall ULwall5 = new Wall(new Vector(499, 351), new Vector(499, 301), true);
        Wall ULwall6 = new Wall(new Vector(149, 151), new Vector(499, 151), true);
        Wall ULwall7 = new Wall(new Vector(149, 301), new Vector(499, 301), false);
        Wall ULwall8 = new Wall(new Vector(149, 151), new Vector(149, 301), false);

        Walls.Add(ULwall1);
        Walls.Add(ULwall2);
        Walls.Add(ULwall3);
        Walls.Add(ULwall4);
        Walls.Add(ULwall5);
        Walls.Add(ULwall6);
        Walls.Add(ULwall7);
        Walls.Add(ULwall8);

        // Upper middle structure
        Wall UMwall1 = new Wall(new Vector(599, 101), new Vector(599, 351), true);
        Wall UMwall2 = new Wall(new Vector(599, 101), new Vector(699, 101), false);
        Wall UMwall3 = new Wall(new Vector(699, 101), new Vector(699, 351), false);
        Wall UMwall4 = new Wall(new Vector(599, 351), new Vector(699, 351), true);

        Walls.Add(UMwall1);
        Walls.Add(UMwall2);
        Walls.Add(UMwall3);
        Walls.Add(UMwall4);

        // Upper right structure
        Wall URwall1 = new Wall(new Vector(799, 101), new Vector(799, 351), true);
        Wall URwall2 = new Wall(new Vector(799, 101), new Vector(899, 101), false);
        Wall URwall3 = new Wall(new Vector(899, 101), new Vector(899, 251), false);
        Wall URwall4 = new Wall(new Vector(799, 351), new Vector(1199, 351), true);
        Wall URwall5 = new Wall(new Vector(899, 251), new Vector(1199, 251), false);
        Wall URwall6 = new Wall(new Vector(1199, 251), new Vector(1199, 351), false);

        Walls.Add(URwall1);
        Walls.Add(URwall2);
        Walls.Add(URwall3);
        Walls.Add(URwall4);
        Walls.Add(URwall5); 
        Walls.Add(URwall6);
    }

    public void Update(float timeDelta) {
        GlobalStateMachine.Update(timeDelta);

        foreach (var entity in MovingEntities) {
            entity.Update(timeDelta);
        }
    }

    public void Render(Graphics g) {
        foreach (Wall wall in Walls)
            wall.Render(g);
        foreach (Objective objective in Objectives)
            objective.Render(g);
        foreach (MovingEntity entity in MovingEntities)
            entity.Render(g);
    }

    public void RenderOverlay(Graphics g) {
        foreach (Edge edge in GridEdges)
            edge.RenderOverlay(g);
        foreach (Node node in GridNodes)
            node.RenderOverlay(g);
        foreach (Wall wall in Walls)
            wall.RenderOverlay(g);
        foreach (Objective objective in Objectives)
            objective.RenderOverlay(g);
        foreach (MovingEntity entity in MovingEntities)
            if (entity.GetType() == typeof(Survivor)) {
                Survivor survivor = (Survivor)entity;
                entity.RenderOverlay(g);
            }
    }

}