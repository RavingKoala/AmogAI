using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AmogAI {
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
			this.SuspendLayout();

			this.components = new System.ComponentModel.Container();
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1350, 800);
			this.Text = "AmogAI Game";

			if (Properties.Settings.Default.isFullscreen)
				WindowState = FormWindowState.Maximized;
			else 
				WindowState = FormWindowState.Normal;


			this.Resize += new System.EventHandler(this.OnWindow_Resize);


			gamePanel = new GamePanel();
			this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnGamePanel_Paint);
			gamePanel.Size = this.Size;

			overlayPanel = new OverlayPanel();
			this.overlayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnOverlayPanel_Paint);
			overlayPanel.Size = this.Size;

			gamePanel.Invalidate();
			overlayPanel.Invalidate();
			 
			this.Controls.Add(gamePanel);
			this.Controls.Add(overlayPanel);

			this.ResumeLayout(false);
		}

		#endregion

		private GamePanel gamePanel;
		private OverlayPanel overlayPanel;
	}
}