namespace AmogAI.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRenderable {
    public void Render(Graphics g);
    public void RenderOverlay(Graphics g);
}
