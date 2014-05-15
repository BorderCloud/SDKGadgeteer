using System;
using Microsoft.SPOT;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace SDKGadgeteer
{
    public abstract class State
    {
        private GT.Timer _Timer;

        private int _timerIntervalMilliseconds;
        private Program _MainHandle;

        public enum TypeState {Error,Start,Final,Normal}; 

        public State(Program handle,TypeState type, int timerIntervalMilliseconds=100)
        {
            _MainHandle = handle;
            _typeState = type;
            _timerIntervalMilliseconds = timerIntervalMilliseconds;
        }

        protected GT.Timer Timer
        {
            get { return _Timer; }
        }
        
        protected Program MainHandle
        {
            get { return _MainHandle; }
        }
        private TypeState _typeState;

        public TypeState Type
        {
            get { return _typeState; }
        }

        public abstract void Entry();
        public abstract void Exit();
        public abstract void Do();

        public void StartListen()
        {
            if (Timer == null)
            {
                _Timer = new GT.Timer(_timerIntervalMilliseconds);

                _Timer.Tick += _timer_Tick;
                _Timer.Start();

                _MainHandle.Joystick.Calibrate();
                _MainHandle.Joystick.JoystickPressed += JoystickPressed;
                _MainHandle.Joystick.JoystickReleased += JoystickReleased;

                _MainHandle.ButtonLeft.ButtonPressed += ButtonLeftPressed;
                _MainHandle.ButtonLeft.ButtonReleased += ButtonLeftReleased;
                _MainHandle.ButtonRight.ButtonPressed += ButtonRightPressed;
                _MainHandle.ButtonRight.ButtonReleased += ButtonRightReleased;
            }
        }
        public void StopListen()
        {
            if (_Timer != null)
            {
                _Timer.Tick -= _timer_Tick;
                _Timer.Stop();
                _Timer = null;

                _MainHandle.Joystick.JoystickPressed -= JoystickPressed;
                _MainHandle.Joystick.JoystickReleased -= JoystickReleased;

                _MainHandle.ButtonLeft.ButtonPressed -= ButtonLeftPressed;
                _MainHandle.ButtonLeft.ButtonReleased -= ButtonLeftReleased;
                _MainHandle.ButtonRight.ButtonPressed -= ButtonRightPressed;
                _MainHandle.ButtonRight.ButtonReleased -= ButtonRightReleased;
            }
        }

        private void _timer_Tick(GT.Timer timer)
        {
            ListenJoystick();
        }

        private void ListenJoystick()
        {
            Joystick.Position myPosition = MainHandle.Joystick.GetPosition();
            this.JoystickPosition(myPosition.X, myPosition.Y);        
        }

        public virtual void ButtonLeftPressed(Button sender, Button.ButtonState state)
        {
            MainHandle.Context.GoBack();
        }
        public virtual void ButtonLeftReleased(Button sender, Button.ButtonState state)
        {
        }

        public virtual void ButtonRightPressed(Button sender, Button.ButtonState state)
        {
            MainHandle.Context.GoStart();
        }
        public virtual void ButtonRightReleased(Button sender, Button.ButtonState state)
        {
        }

        public virtual void JoystickPressed(Joystick sender, Joystick.JoystickState state)
        {
        }
        public virtual void JoystickReleased(Joystick sender, Joystick.JoystickState state)
        {
        }

        public virtual void JoystickPosition(double X, double Y)
        {
        }
    }
}
