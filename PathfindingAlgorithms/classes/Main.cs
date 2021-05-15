using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PathfindingAlgorithms
{
    public class Main
    {
        public Enviroment Env;
        public PathfindingAlgorithm Alg;
        public WallGenerator Gen;
        public DispatcherTimer Timer;
        public RunningState State;

        public Canvas Canv;
        private MainViewModel MVM;

        public Main(Canvas canv, MainViewModel mvm)
        {
            // Canvas
            Canv = canv;
            MVM = mvm;

            // Timer
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(30);
            Timer.Tick += new EventHandler(Step);

            // Default state
            State = RunningState.NotRunning;

        }
        public void StartupInitialization()
        {
            // Default initialization
            Env = new SquareGridEnviroment(Canv, ( 16, 9 ));
            Env.StartNode = Env.Nodes.First();
            Env.EndNode = Env.Nodes.Last();
            Env.StartNode.SetType(NodeType.Start);
            Env.EndNode.SetType(NodeType.End);
        }
        public void Step(object sender, EventArgs e)
        {
            switch (State)
            {
                case RunningState.PathfindingAlgorithm:
                    if (!Alg.PathFound && Alg.PathExists != Exist.False) Alg.Step();
                    else StopTimer();
                    break;
                case RunningState.WallGenerator:
                    if (!Gen.IsFinished) Gen.Step();
                    else StopTimer();
                    break;
            }
        }
        public bool IsStart
        {
            get { return (Env.StartNode != null && Env.EndNode != null && State == RunningState.NotRunning); }
            set { }
        }
        public void Start(string algString)
        {
            if (IsStart)
            {
                Env.ClearState();
                Env.RemovePaths();
                Alg = Tools.AlgorithmFromString(algString, Env);
                Env.AddPath(Alg.Path);
                StartTimer(RunningState.PathfindingAlgorithm);
            }
        }
        /* Timer */
        public void StartTimer(RunningState state)
        {
            Timer.Start();
            MVM.IsRunning = true;
            State = state;
        }
        public void StopTimer()
        {
            Timer.Stop();
            MVM.IsRunning = false;
            State = RunningState.NotRunning;
        }
        public void PlayPauseTimer()
        {
            if (Timer.IsEnabled) Timer.Stop();
            else Timer.Start();
        }
        public void StopTimerAndClear()
        {
            Env.ClearState();
            Env.RemovePaths();
            StopTimer();
        }
        public void ChangeTimerInterval(int interval)
        {
            Timer.Interval = TimeSpan.FromMilliseconds(interval);
        }
        /* Wall generator */
        public void GenerateWall(string wallGenString)
        {
            // Choose wall generator from string
            WallGenerator _gen = Tools.WallGeneratorFromString(wallGenString, Env);
            if (_gen != null)
            {
                Gen = _gen;
                StopTimer();
                Env.RemovePaths();
                Env.ClearState();
                Env.ClearWalls();
                StartTimer(RunningState.WallGenerator);
            }
            else MessageBox.Show("Selected wall generator cannot run.");
        }
        /* Enviroment */
        public void NewEnviroment(EnviromentConstructor envCon)
        {
            Env.Clear(); // remove all canv children
            Env = envCon.Construct(Canv); // generate new enviroment
            Env.StartNode = Env.Nodes.First();
            Env.EndNode = Env.Nodes.Last();
            Env.StartNode.SetType(NodeType.Start);
            Env.EndNode.SetType(NodeType.End);
        }
        public void ResetEnviroment()
        {
            Env.RemovePaths();
            Env.ClearState();
            Env.ClearWalls();
        }

    }
}
