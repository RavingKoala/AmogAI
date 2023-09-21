namespace AmogAI;

using AmogAI.SteeringBehaviour;

public enum RenderPanelType {
    Game,
    Overlay
}
public interface Renderable {
    void RenderGame(Graphics g, Vector windowOffset);
    void RenderOverlay(Graphics g, Vector windowOffset);
}
