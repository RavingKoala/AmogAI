namespace AmogAI;

using AmogAI.SteeringBehaviour;

public partial class MainFrame : Form
{

    public World.World World;
    public System.Timers.Timer GameTimer;
    private DateTime LastSignalTime;
    private bool showOverlay;
    public static Vector WindowCenter = new Vector(0, 0);
    public MainFrame()
    {
        InitializeComponent();

        World = new World.World();
        showOverlay = false;

        MainFrame.WindowCenter = new Vector(this.Size.Width / 2, this.Size.Height / 2);
        LastSignalTime = DateTime.Now;

        GameTimer = new System.Timers.Timer();
        GameTimer.Elapsed += Timer_Elapsed;
        GameTimer.Interval = 1000 / Properties.Settings.Default.fps;
        GameTimer.Enabled = true;

    }

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        float timeDelta = (float)(e.SignalTime - LastSignalTime).TotalMilliseconds;
        LastSignalTime = e.SignalTime;

        World.Update(timeDelta);
        gamePanel.Invalidate();
        if (showOverlay)
            overlayPanel.Invalidate();
    }

    private void OnGamePanel_Paint(object sender, PaintEventArgs e)
    {
        World.Render(e.Graphics, RenderPanelType.Game);
    }

    private void OnOverlayPanel_Paint(object sender, PaintEventArgs e)
    {
        World.Render(e.Graphics, RenderPanelType.Overlay);
    }

    private void OnWindow_Resize(object sender, System.EventArgs e)
    {
        Control window = (Control)sender;
        gamePanel.Size = window.Size;
        overlayPanel.Size = window.Size;

        MainFrame.WindowCenter = new Vector(window.Size.Width / 2, window.Size.Height / 2);
    }
}