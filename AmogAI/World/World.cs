namespace AmogAI.World;

using AmogAI.AStar;
using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;
using System.Diagnostics;
using AmogAI.StateBehaviour.WordStates;

public class World : IRenderable {
    public bool EmergencyHappening { get; set; }
    public Objective EmergencyObjective { get; set; }
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
        Objective upperLeft = new Objective(new Vector(200, 225));
        Objective upperRight = new Objective(new Vector(950, 200));
        Objective lowerLeft = new Objective(new Vector(200, 600));
        Objective lowerRight = new Objective(new Vector(1000, 675));

        Objectives.Add(upperLeft);
        Objectives.Add(upperRight);
        Objectives.Add(lowerLeft);
        Objectives.Add(lowerRight);

        EmergencyObjective = new Objective(new Vector(1000, 450));
        Objectives.Add(EmergencyObjective);
    }

    private void Populate() {
        Survivor p1 = new Survivor(new Vector(500, 400), this);
        Survivor p2 = new Survivor(new Vector(500, 400), this);
        Survivor p3 = new Survivor(new Vector(500, 400), this);
        Survivor p4 = new Survivor(new Vector(500, 400), this);

        MovingEntities.Add(p1);
        //MovingEntities.Add(p2);
        //MovingEntities.Add(p3);
        //MovingEntities.Add(p4);
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

        // Lower left structure
        Wall LLwall1 = new Wall(new Vector(99, 451), new Vector(99, 701), true);
        Wall LLwall2 = new Wall(new Vector(99, 451), new Vector(149, 451), false);
        Wall LLwall3 = new Wall(new Vector(99, 701), new Vector(499, 701), true);
        Wall LLwall4 = new Wall(new Vector(499, 701), new Vector(499, 651), true);
        Wall LLwall5 = new Wall(new Vector(149, 651), new Vector(499, 651), false);
        Wall LLwall6 = new Wall(new Vector(149, 451), new Vector(149, 651), false);

        Walls.Add(LLwall1);
        Walls.Add(LLwall2);
        Walls.Add(LLwall3);
        Walls.Add(LLwall4);
        Walls.Add(LLwall5);
        Walls.Add(LLwall6);

        // Lower middle structure
        Wall LMwall1 = new Wall(new Vector(249, 451), new Vector(699, 451), false);
        Wall LMwall2 = new Wall(new Vector(699, 451), new Vector(699, 701), false);
        Wall LMwall3 = new Wall(new Vector(649, 701), new Vector(699, 701), true);
        Wall LMwall4 = new Wall(new Vector(649, 501), new Vector(649, 701), true);
        Wall LMwall5 = new Wall(new Vector(249, 451), new Vector(249, 501), true);
        Wall LMwall6 = new Wall(new Vector(249, 501), new Vector(649, 501), true);

        Walls.Add(LMwall1);
        Walls.Add(LMwall2);
        Walls.Add(LMwall3);
        Walls.Add(LMwall4);
        Walls.Add(LMwall5);
        Walls.Add(LMwall6);

        // Lower right structure
        Wall LRwall1 = new Wall(new Vector(799, 551), new Vector(799, 701), true);
        Wall LRwall2 = new Wall(new Vector(799, 551), new Vector(1199, 551), false);
        Wall LRwall3 = new Wall(new Vector(1199, 551), new Vector(1199, 701), false);
        Wall LRwall4 = new Wall(new Vector(799, 701), new Vector(924, 701), true);
        Wall LRwall5 = new Wall(new Vector(1074, 701), new Vector(1199, 701), true);
        Wall LRwall6 = new Wall(new Vector(924, 701), new Vector(924, 626), true);
        Wall LRwall7 = new Wall(new Vector(1074, 701), new Vector(1074, 626), false);
        Wall LRwall8 = new Wall(new Vector(924, 626), new Vector(1074, 626), true);

        Walls.Add(LRwall1); 
        Walls.Add(LRwall2);
        Walls.Add(LRwall3);
        Walls.Add(LRwall4);
        Walls.Add(LRwall5);
        Walls.Add(LRwall6);
        Walls.Add(LRwall7);
        Walls.Add(LRwall8);

        // Emergency structure
        Wall EWall1 = new Wall(new Vector(874, 401), new Vector(924, 401), false);  
        Wall EWall2 = new Wall(new Vector(874, 401), new Vector(874, 501), true);
        Wall EWall3 = new Wall(new Vector(924, 401), new Vector(924, 501), false);
        Wall EWall4 = new Wall(new Vector(874, 501), new Vector(924, 501), true);
        Wall EWall5 = new Wall(new Vector(1074, 401), new Vector(1124, 401), false);
        Wall EWall6 = new Wall(new Vector(1074, 401), new Vector(1074, 501), true);
        Wall EWall7 = new Wall(new Vector(1124, 401), new Vector(1124, 501), false);
        Wall EWall8 = new Wall(new Vector(1074, 501), new Vector(1124, 501), true);

        Walls.Add(EWall1);
        Walls.Add(EWall2);
        Walls.Add(EWall3);
        Walls.Add(EWall4);
        Walls.Add(EWall5);
        Walls.Add(EWall6);
        Walls.Add(EWall7);
        Walls.Add(EWall8);
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
                entity.RenderOverlay(g);
            }
    }
}