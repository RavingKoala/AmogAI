namespace AmogAI.World;

using SteeringBehaviour;

public class Objective {
	public Vector Position { get; set; }
	public int Duration { get; set; } // in ms
	public bool isDone { get; set; }

	public Objective(Vector position) : this(position, 1000) {}
	public Objective(Vector position, int duration) {
		Position = position;
		Duration = duration;
		isDone = false;
	}

}