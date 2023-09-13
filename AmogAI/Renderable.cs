namespace AmogAI {
	public enum RenderPanelType {
		Game,
		Overlay
	}
	public interface Renderable {
		void RenderGame(Graphics g, Math.Vector windowOffset);
		void RenderOverlay(Graphics g, Math.Vector windowOffset);
	}
}
