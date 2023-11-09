namespace AmogAI.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRenderable {
    public abstract void Render(Graphics g);
    public abstract void RenderOverlay(Graphics g);
}
