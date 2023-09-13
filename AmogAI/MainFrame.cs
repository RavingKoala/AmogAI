
using Microsoft.VisualBasic.Devices;

namespace AmogAI {
	public partial class MainFrame : Form {

		public World.World World;
		public System.Timers.Timer GameTimer;
		private DateTime LastSignalTime;
		private Math.Vector WindowCenter;
		public MainFrame() {
			InitializeComponent();

			World = new World.World();

			LastSignalTime = DateTime.Now;

			GameTimer = new System.Timers.Timer();
            GameTimer.Elapsed += Timer_Elapsed;
            GameTimer.Interval = 1000/Properties.Settings.Default.fps;
            GameTimer.Enabled = true;

		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			float timeDelta = (float)(e.SignalTime - LastSignalTime).TotalMilliseconds;
			LastSignalTime = e.SignalTime;

            World.Update(timeDelta);
			gamePanel.Invalidate();
			overlayPanel.Invalidate();
		}

		private void OnGamePanel_Paint(object sender, PaintEventArgs e) {
			World.Render(e.Graphics, WindowCenter, RenderPanelType.Game);
		}

		private void OnOverlayPanel_Paint(object sender, PaintEventArgs e) {
			World.Render(e.Graphics, WindowCenter, RenderPanelType.Overlay);
		}

		private void OnWindow_Resize(object sender, System.EventArgs e) {
			Control window = (Control)sender;

			WindowCenter = new Math.Vector(window.Size.Width/2, window.Size.Height/2);
		}
	}
}