namespace AmogAI.SteeringBehaviour;

public interface ISteeringBehaviour {
	Vector Calculate(IEntity self, IEntity goal);
}
