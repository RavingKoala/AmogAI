namespace AmogAI.World;

using AmogAI.SteeringBehaviour;
using AmogAI.World.Entity;

public class World {
	private List<MovingEntity> _staticEntities;
	private List<MovingEntity> _movingEntities;

	public World() {
		_staticEntities = new List<MovingEntity>();
		_movingEntities = new List<MovingEntity>();

		Populate();
	}

    private void Populate() {
		Person p1 = new Person(new Vector(100, 100));
		Person p2 = new Person(new Vector(150, 150));

		p1.Target = p2;
		p1.SteeringBehaviour.TurnOn(BehaviourType.Seek);

		_movingEntities.Add(p1);
		_movingEntities.Add(p2);

        //foreach (Person p in _movingEntities) {
        //    p.SteeringBehaviour.TurnOn(BehaviourType.Seek);
        //}
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
}
