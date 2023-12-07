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
    public List<Edge> GridEdges {  get; private set; }
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
        var gridDistance = 200;
        for (int x = 1; x < 5; x++) {
            for (int y = 1; y < 4; y++) {
                Objectives.Add(new Objective(new Vector(x * gridDistance, y * gridDistance)));
            }
        }
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
        Wall leftWall = new Wall(new Vector(0, -1), new Vector(0, 801), false);
        Wall topWall = new Wall(new Vector(-1, 0), new Vector(1351, 0), true);
        Wall bottomWall = new Wall(new Vector(-1, 800), new Vector(1351, 800), false);
        Wall rightWall = new Wall(new Vector(1350, -1), new Vector(1350, 801), true);

        Walls.Add(leftWall);
        Walls.Add(topWall);
        Walls.Add(rightWall);
        Walls.Add(bottomWall);
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