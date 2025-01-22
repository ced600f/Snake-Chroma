using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Scene
{
    public virtual void Load() { }
    public abstract void Update();
    public abstract void Draw();
    public virtual void Unload() { }
}
