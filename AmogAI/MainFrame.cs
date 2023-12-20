namespace AmogAI;

using AmogAI.SteeringBehaviour;

public partial class MainFrame : Form {

    public World.World World;
    public System.Timers.Timer GameTimer;
    private bool _showOverlay;
    private bool _paused = false;
    private float _timeDelta = 1000 / Properties.Settings.Default.fps;
    private readonly object _lock = new();

    public MainFrame() {
        InitializeComponent();

        World = new World.World();
        _showOverlay = false;

        if (Properties.Settings.Default.isFullscreen)
            WindowState = FormWindowState.Maximized;

        GameTimer = new System.Timers.Timer();
        GameTimer.Elapsed += Timer_Elapsed;
        GameTimer.Interval = _timeDelta;
        GameTimer.Enabled = true;
    }

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e) {
        lock (_lock) {
            if (_paused)
                return;

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
        if (e.KeyCode == Keys.E) {
            World.IsEmergencyHappening = true;
            World.EmergencyObjective.IsDone = false;    
        }
        if (e.KeyCode == Keys.P)
            _paused = !_paused;
    }
}