using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AmogAI;

partial class MainFrame {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.gamePanel = new GamePanel();
        this.SuspendLayout();
        // 
        // gamePanel
        // 
        this.gamePanel.Dock = DockStyle.Fill;
        this.gamePanel.Location = new Point(0, 0);
        this.gamePanel.Name = "gamePanel";
        this.gamePanel.Size = new Size(1350, 800);
        this.gamePanel.TabIndex = 0;
        this.gamePanel.Paint += this.OnGamePanel_Paint;
        // 
        // MainFrame
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1350, 800);
        Controls.Add(this.gamePanel);
        ForeColor = SystemColors.ControlText;
        Name = "MainFrame";
        Text = "AmogAI Game";
        KeyDown += this.MainFrame_KeyDown;
        this.ResumeLayout(false);
    }

    #endregion

    private GamePanel gamePanel;
}