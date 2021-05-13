using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    public abstract class WallGenerator
    {
        public bool IsFinished;
        protected Enviroment Env;
        public WallGenerator(Enviroment env)
        {
            Env = env;
            IsFinished = false;
        }
        public abstract void Step();
    }
}
