namespace AmogAI.World;

using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class World {
    private List<MovingEntity> _movingEntities;
    public List<Objective> Objectives;
    public List<Wall> Walls { get; set; }

    public World() {
        _movingEntities = new List<MovingEntity>();
        Walls = new List<Wall>();
        Objectives = new List<Objective>();

        MakeObjectives();
        Populate();
        DrawWalls();

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

        for (int i = 0; i < 20; i++) {
            Person p = new Person(new Vector(350, 350), this);
            p.SteeringBehaviour.TurnOn(BehaviourType.Wander);
            p.SteeringBehaviour.TurnOn(BehaviourType.WallAvoidance);

            _movingEntities.Add(p);
        }

        _movingEntities.Add(p1);
        _movingEntities.Add(p2);
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

        Wall w1 = new Wall(new Vector(250, 100), new Vector(250, 600), false); // left
        Wall w2 = new Wall(new Vector(450, 100), new Vector(450, 600), true); // right
        Wall w3 = new Wall(new Vector(250, 100), new Vector(450, 100), true); // top
        Wall w4 = new Wall(new Vector(250, 600), new Vector(450, 600), false); // bottom

        //Walls.Add(w1);
        //Walls.Add(w2);
        //Walls.Add(w3);
        //Walls.Add(w4);
    }

    public void Update(float timeDelta) {
        foreach (var entity in _movingEntities) {
            entity.Update(timeDelta);
        }
    }

    public void Render(Graphics g) {
        foreach (var entity in _movingEntities)
            entity.Render(g);
        foreach (var wall in Walls)
            wall.Render(g);
        foreach (var objective in Objectives)
            objective.Render(g);
    }

    public void RenderOverlay(Graphics g) {
        foreach (var objective in Objectives)
            objective.RenderOverlay(g);
    }

}