using System;
using System.Collections;
using System.Diagnostics;

namespace ConsoleStateMachine
{
    public class Context
    {
        private State _startState;
        private ErrorState _errorState;
        private State _currentState;
        private Stack _precedentStates;
        private bool _isBack = false;

        public Context(State startState, ErrorState errorState)
        {
            _startState = startState;
            _errorState = errorState;
            _precedentStates = new Stack();
        }

        public State CurrentState
        {
            get { return _currentState; }
            set
            {
                if (_currentState != null && value.GetType() == _currentState.GetType())
                    return;


                /* try
                 {*/
                if (_currentState != null)
                {
                    _currentState.Exit();
                }
                if (value != null)
                {
                    if (!_isBack)
                    {
                        Debug.Print("Empile " + _currentState);
                        _precedentStates.Push(_currentState);
                    }
                    _currentState = value;
                    Debug.Print("Current state => " + _currentState);
                    _currentState.Entry();
                }
                _isBack = false;
                /* }
                 catch (Exception ex)
                 {
                     _errorState.Error = ex;
                     CurrentState = _errorState;
                 }*/

            }
        }

        public void Start()
        {
            CurrentState = _startState;
        }
        public void GoStart()
        {
            _precedentStates.Clear();
            Start();
        }
        public void GoBack()
        {
            if (_precedentStates.Count > 0)
            {
                _isBack = true;
                CurrentState = (State)_precedentStates.Pop();
                Debug.Print("Depile " + CurrentState);
            }
        }
    }
}
