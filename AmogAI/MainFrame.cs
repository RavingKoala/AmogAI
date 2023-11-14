namespace AmogAI;

using AmogAI.SteeringBehaviour;

public partial class MainFrame : Form {

    public World.World World;
    public System.Timers.Timer GameTimer;
    public static Vector WindowCenter = new Vector(0, 0);
    private bool _showOverlay;
    private float _timeDelta = 1000 / Properties.Settings.Default.fps;
    private readonly object _lock = new();

    public MainFrame() {
        InitializeComponent();

        World = new World.World();
        _showOverlay = false;

        MainFrame.WindowCenter = new Vector(this.Size.Width / 2, this.Size.Height / 2);

        if (Properties.Settings.Default.isFullscreen)
            WindowState = FormWindowState.Maximized;

        GameTimer = new System.Timers.Timer();
        GameTimer.Elapsed += Timer_Elapsed;
        GameTimer.Interval = 1000 / Properties.Settings.Default.fps;
        GameTimer.Enabled = true;
    }

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e) {
        lock (_lock) {
            World.Update(_timeDelta);

            gamePanel.Invalidate();
        }
    }

    private void OnGamePanel_Paint(object sender, PaintEventArgs e) {
        World.Render(e.Graphics);
        if (_showOverlay)
            World.RenderOverlay(e.Graphics);
    }

    private void MainFrame_KeyDown(object sender, KeyEventArgs e) {
        if (e.KeyCode == Keys.Tab)
            _showOverlay = !_showOverlay;
        if (e.KeyCode == Keys.E)
            World.EmergencyHappening = !World.EmergencyHappening;
    }
}