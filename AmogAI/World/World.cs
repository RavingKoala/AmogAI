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
    public Dictionary<int, Objective> Objectives { get; set; }
    public List<Wall> Walls { get; set; }
    public GlobalStateMachine GlobalStateMachine { get; set; }
    public List<Edge> GridEdges {  get; private set; }
    public List<Node> GridNodes { get; private set; }

    public World() {
        MovingEntities = new List<MovingEntity>();
        Walls = new List<Wall>();
        Objectives = new Dictionary<int, Objective>();
        Stopwatch = new Stopwatch();
        GlobalStateMachine = new GlobalStateMachine(this);

        DrawWalls();
        DrawGrid();
        MakeObjectives();
        Populate();
        
        Survivor survivor = (Survivor)MovingEntities[0];
        survivor.SetObjective(1);
        survivor.CurrentObjective.StartTask();
    }

    private void DrawGrid() {
        (GridNodes, GridEdges) = Graph.Generate(this);
    }

    private void MakeObjectives() {
        var gridDistance = 200;
        int objectiveId = 1;
        for (int x = 1; x < 5; x++) {
            for (int y = 1; y < 4; y++) {
                Objectives.Add(objectiveId, new Objective(objectiveId, new Vector(x * gridDistance, y * gridDistance)));
                objectiveId++;
            }
        }
    }
        
    private void Populate() {
        Survivor p1 = new Survivor(new Vector(50, 50), this);
        Survivor p2 = new Survivor(new Vector(50, 50), this);
        Survivor p3 = new Survivor(new Vector(50, 50), this);
        Survivor p4 = new Survivor(new Vector(50, 50), this);

        p1.SteeringBehaviour.TurnOn(BehaviourType.Pursuit);
        p1.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);
        p2.SteeringBehaviour.TurnOn(BehaviourType.Wander);
        p2.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);
        p3.SteeringBehaviour.TurnOn(BehaviourType.Wander);
        p3.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);
        p4.SteeringBehaviour.TurnOn(BehaviourType.Wander);
        p4.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);

        MovingEntities.Add(p1);
        MovingEntities.Add(p2);
        MovingEntities.Add(p3);
        MovingEntities.Add(p4);
    }

    private void DrawWalls() {
        Wall leftWall = new Wall(new Vector(0, 0), new Vector(0, 800), false);
        Wall topWall = new Wall(new Vector(0, 0), new Vector(1350, 0), true);
        Wall rightWall = new Wall(new Vector(1350, 0), new Vector(1350, 800), true);
        Wall bottomWall = new Wall(new Vector(0, 800), new Vector(1350, 800), false);

        Walls.Add(leftWall);
        Walls.Add(topWall);
        Walls.Add(rightWall);
        Walls.Add(bottomWall);

        Wall w1 = new Wall(new Vector(240, 120), new Vector(240, 600), true); // left
        Wall w2 = new Wall(new Vector(480, 120), new Vector(480, 600), false); // right
        Wall w3 = new Wall(new Vector(240, 120), new Vector(480, 120), false); // top
        Wall w4 = new Wall(new Vector(240, 600), new Vector(480, 600), true); // bottom

        Walls.Add(w1);
        Walls.Add(w2);
        Walls.Add(w3);
        Walls.Add(w4);
    }

    public void Update(float timeDelta) {
        GlobalStateMachine.Update(timeDelta);

        foreach (var entity in MovingEntities) {
            entity.Update(timeDelta);
        }
    }

    public void Render(Graphics g) {
        foreach (var wall in Walls)
            wall.Render(g);
        foreach (KeyValuePair<int, Objective> objective in Objectives)
            objective.Value.Render(g);
        foreach (var entity in MovingEntities)
            entity.Render(g);
    }

    public void RenderOverlay(Graphics g) {
        foreach (var Node in GridNodes)
            Node.RenderOverlay(g);
        foreach (var Edge in GridEdges)
            Edge.RenderOverlay(g);
        foreach (var wall in Walls)
            wall.RenderOverlay(g);
        foreach (KeyValuePair<int, Objective> objective in Objectives)
            objective.Value.RenderOverlay(g);
        foreach (var entity in MovingEntities)
            if (entity.GetType() == typeof(Survivor)) {
                Survivor survivor = (Survivor)entity;
                entity.RenderOverlay(g);
            }
    }

}