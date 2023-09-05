
using Microsoft.VisualBasic.Devices;

namespace AmogAI {
	public partial class MainFrame : Form {

		public World.World world;
		public System.Timers.Timer GameTimer;
		private DateTime lastSignalTime;
		public MainFrame() {
			InitializeComponent();

			lastSignalTime = DateTime.Now;

			GameTimer = new System.Timers.Timer();
            GameTimer.Elapsed += Timer_Elapsed;
            GameTimer.Interval = 1000/Properties.Settings.Default.fps;
            GameTimer.Enabled = true;

		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			float TimeDleta = (float)(e.SignalTime - lastSignalTime).TotalMilliseconds;
			lastSignalTime = e.SignalTime;

            //world.Update(e.SignalTime);
            //dbPanel1.Invalidate();
        }
	}
}