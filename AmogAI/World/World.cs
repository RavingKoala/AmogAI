using AmogAI.World.Entity;

namespace AmogAI.World;

public class World {
	private List<IEntity> entities;

	public World() {
		entities = new List<IEntity>();
		entities.Add(new Person());
	}

	public void Update(float timeDelta) {
		foreach (var entity in entities) {
			entity.Update(timeDelta);
		}
	}

	public void Render(Graphics g, RenderPanelType renderType) {
		if (renderType == RenderPanelType.Game)
			foreach (var entity in entities)
				entity.Redraw(g);
	}
}
