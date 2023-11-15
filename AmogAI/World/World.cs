namespace AmogAI.World;

using AmogAI.AStar;
using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class World : IRenderable {
    private List<MovingEntity> _movingEntities;
    public List<Objective> Objectives;
    public List<Wall> Walls { get; set; }
    public List<Edge> GridEdges {  get; private set; }
    public List<Node> GridNodes { get; private set; }


    public World() {
        _movingEntities = new List<MovingEntity>();
        Walls = new List<Wall>();
        Objectives = new List<Objective>();

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
        Person p1 = new Person(new Vector(50, 50), this);
        Person p2 = new Person(new Vector(350, 350), this);

        p1.SteeringBehaviour.TurnOn(BehaviourType.Pursuit);
        p1.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);
        p1.Target = p2;

        p2.SteeringBehaviour.TurnOn(BehaviourType.Wander);
        p2.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);

        _movingEntities.Add(p1);
        _movingEntities.Add(p2);
    }

    private void DrawWalls() {
        Wall leftWall = new Wall(new Vector(-1, 0), new Vector(-1, 800), false);
        Wall topWall = new Wall(new Vector(-1, 0), new Vector(1351, 0), true);
        Wall rightWall = new Wall(new Vector(1351, 0), new Vector(1351, 800), true);
        Wall bottomWall = new Wall(new Vector(-1, 800), new Vector(1351, 800), false);

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
        foreach (var entity in _movingEntities) {
            entity.Update(timeDelta);
        }
    }

    public void Render(Graphics g) {
        foreach (var wall in Walls)
            wall.Render(g);
        foreach (var objective in Objectives)
            objective.Render(g);
        foreach (var entity in _movingEntities)
            entity.Render(g);
    }

    public void RenderOverlay(Graphics g) {
        foreach (var Node in GridNodes)
            Node.RenderOverlay(g);
        foreach (var Edge in GridEdges)
            Edge.RenderOverlay(g);
        foreach (var wall in Walls)
            wall.RenderOverlay(g);
        foreach (var objective in Objectives)
            objective.RenderOverlay(g);
        foreach (var entity in _movingEntities)
            if (entity.GetType() == typeof(Person)) {
                Person person = (Person)entity;
                entity.RenderOverlay(g);
            }
    }

}