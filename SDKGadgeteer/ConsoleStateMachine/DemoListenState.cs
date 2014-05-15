using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleStateMachine 
{
    class DemoListenState : State
    {
        private string _message;

        public DemoListenState()
            : base(TypeState.Normal)
        {
        }

        public override void Entry()
        {
            _message = "";
             Do();
            StartListen();
        }

        public override void Exit()
        {
            StopListen();
        }

        public override void Do()
        {
            ConsoleDisplayN18.Clear();
            ConsoleDisplayN18.WriteLine("Demo Listen State\n");
            _message += @"        8       " + "\n";
            _message += @"       /\       " + "\n";
            _message += @"       ||       " + "\n";
            _message += @"4 <===  5 ===> 6" + "\n";
            _message += @"       ||       " + "\n";
            _message += @"       \/       " + "\n";
            _message += @"        2       " + "\n";
            _message += @"Home:1    Back:3" + "\n";
            ConsoleDisplayN18.WriteLine(_message);
        }

       public override void ButtonLeftPressed()
        {
            _message = "ButtonLeftPressed\n(key1+Enter)\n";
            Do();
        }

        public override void ButtonRightPressed()
        {
            _message = "ButtonRightPressed\n(key3+Enter)\n";
            Do();
        }

        public override void JoystickPressed()
        {
            _message = "ButtonJoystickPressed\n(key5+Enter)\n";
            Do();
        }

        public override void JoystickPosition(double X, double Y)
        {
            _message = "X=" + X + " Y=" + Y + "\n";
            Do();
        }
    }
}
