namespace AmogAI.StateBehaviour;

using AmogAI.World.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IState {
    void Enter(Survivor s);
    void Execute(Survivor s);
    void Exit(Survivor s);
}
