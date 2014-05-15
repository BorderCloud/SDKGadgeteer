using System;
using System.Threading;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT;
using GT = Gadgeteer;

namespace SDKGadgeteer
{
    public sealed class MainState : State
    {
        private Menu _menu;
        private State[] _arrayState;

        public MainState(Program handle) : base(handle,TypeState.Normal)
        {
            _arrayState = new State[9];
            string disconnect = "(disconnect)";
            _menu = new Menu(MainHandle.Display_N18);
            _menu.Title = "SDK Gadgeteer";
            _menu.Lines[0] = "On/off Button led";
            _menu.Lines[1] = "Demo Joystick";
            _menu.Lines[2] = "Demo Timer";            
            _menu.Lines[3] = "Demo SDCard";
            _menu.Lines[4] = "Demo Tunes";
            _menu.Lines[5] = "Demo Led Strip";
            _menu.Lines[6] = "Demo Screen";
            _menu.Lines[7] = "Item7";
            _menu.Lines[8] = "Infos,versions,...";

            if (MainHandle.Tunes == null)
                _menu.Lines[4] += disconnect;

            if (MainHandle.LED_Strip == null)
                _menu.Lines[5] += disconnect;
        }

        private void SelectState(int menuItem)
        {
             State state = null;
             if (_arrayState[menuItem] == null)
             {
                 switch (menuItem)
                 {
                     case 0:
                         state = new LedButtonState(MainHandle);
                         break;
                     case 1:
                         state = new JoystickDemoState(MainHandle);
                         break;
                     case 2:
                         state = new ExampleState(MainHandle);
                         break;
                     case 3:
                         state = new SDCardState(MainHandle);
                         break;
                     case 4:
                         state = new TunesDemoState(MainHandle);
                         break;
                     case 5:
                         state = new LedStripDemoState(MainHandle);
                         break;
                     case 6:
                         state = new ScreenDemoState(MainHandle);
                         break;
                     case 8:
                         state = new InfoState(MainHandle);
                         break;
                 }
                 _arrayState[menuItem] = state;
             }
             else
             {
                 state = _arrayState[menuItem];
             }

             MainHandle.Context.CurrentState = state;
        }
        
        public override void Entry()
        {
            _menu.CursorLine = 0;
            _menu.Draw();
            StartListen();
        }

        public override void Exit()
        {
            StopListen();
        }

        public override void Do()
        {
            //Do nothing            
        }

        public override void JoystickReleased(Joystick sender, Joystick.JoystickState state)
        {
            //ConsoleDisplayN18.Write("Selected " + _menu.CursorLine);
            //Thread.Sleep(1000);
            //_menu.Draw();
            SelectState(_menu.CursorLine);
        }
        
        public override void JoystickPosition(double X, double Y)
        {
            if (Y < -0.7)
            {
                _menu.CursorLine++;
                _menu.Draw();
                Thread.Sleep(100);
            }
            else if (Y > 0.7)
            {
                _menu.CursorLine--;
                _menu.Draw();
                Thread.Sleep(100);
            }
        }
    }
}
