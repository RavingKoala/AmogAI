namespace AmogAI.World;

using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class World {
	private List<MovingEntity> _staticEntities;
	private List<MovingEntity> _movingEntities;
	public List<Wall> Walls { get; set; }

	public World() {
		_staticEntities = new List<MovingEntity>();
		_movingEntities = new List<MovingEntity>();
		Walls = new List<Wall>();

		Populate();
		DrawWalls();
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
		Wall leftWall = new Wall(new Vector(0, 0), new Vector(0, 800));	
		Wall topWall = new Wall(new Vector(0, 0), new Vector(1350, 0));	
		Wall rightWall = new Wall(new Vector(1350, 0), new Vector(1350, 800));	
		Wall bottomWall = new Wall(new Vector(0, 800), new Vector(1350, 800));	

		Wall w1 = new Wall(new Vector(250, 100), new Vector(250, 600));
		Wall w2 = new Wall(new Vector(450, 100), new Vector(450, 600));

		Walls.Add(leftWall);
		Walls.Add(topWall);
		Walls.Add(rightWall);
		Walls.Add(bottomWall);

		Walls.Add(w1);
		Walls.Add(w2);
	}

    public void Update(float timeDelta) {
		foreach (var entity in _movingEntities) {
			entity.Update(timeDelta);
		}
	}

	public void Render(Graphics g, RenderPanelType renderType) {
		if (renderType == RenderPanelType.Game)
			foreach (var entity in _movingEntities)
				entity.Render(g);
	}

	public void InitialRender(Graphics g, RenderPanelType renderType) {
        if (renderType == RenderPanelType.Game)
            foreach (var wall in Walls)
                wall.Render(g);
    }
}
