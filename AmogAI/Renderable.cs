namespace AmogAI;

using AmogAI.SteeringBehaviour;

public interface Renderable {
    void RenderGame(Graphics g, Vector windowOffset);
    void RenderOverlay(Graphics g, Vector windowOffset);
}
