using System;
using System.Reflection;
using System.Threading;

namespace ConsoleStateMachine
{
    class SplashScreenState : State
    {
        public SplashScreenState() :
            base(TypeState.Start)
        {
        }

        public override void Entry()
        {
            ConsoleDisplayN18.Clear();
            ConsoleDisplayN18.WriteLine("Chronomètre");
            Thread.Sleep(1000);
            Program.Context.CurrentState = new MenuState();
        }

        public override void Exit()
        {
            //Do nothing
        }

        public override void Do()
        {
            //Do nothing
        }
    }
}
