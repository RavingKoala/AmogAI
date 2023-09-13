using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmogAI {
	internal class OverlayPanel: Panel {
		public OverlayPanel() {
			this.DoubleBuffered = true;

			this.BackColor = Color.Transparent;
			this.ForeColor = Color.Black;

		}
	}
}
