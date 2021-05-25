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
        protected Environment Env;
        public WallGenerator(Environment env)
        {
            Env = env;
            IsFinished = false;
        }
        public abstract void Step();
    }
}
