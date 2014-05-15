using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace SDKGadgeteer
{
    public partial class Program
    {
        #region Accessors
        private Context _Context;

        public Context Context
        {
            get { return _Context; }
        }

        public GHIElectronics.Gadgeteer.FEZCerberus MotherCard
        {
            get { return Mainboard; }
        }

        public Gadgeteer.Modules.GHIElectronics.Button ButtonRight
        {
            get { return buttonRight ; }
        }
        public Gadgeteer.Modules.GHIElectronics.Button ButtonLeft
        {
            get { return buttonLeft; }
        }
        public Gadgeteer.Modules.GHIElectronics.Display_N18 Display_N18
        {
            get { return display_N18; }
        }

        public Gadgeteer.Modules.GHIElectronics.Joystick Joystick
        {
            get { return joystick; }
        }
        public Gadgeteer.Modules.GHIElectronics.SDCard SdCard
        {
            get { return sdCard; }
        }


        public Gadgeteer.Modules.GHIElectronics.Tunes Tunes
        {
            get { return tunes; }
        }

        public Gadgeteer.Modules.GHIElectronics.LED_Strip LED_Strip
        {
            get { return led_Strip; }
        }
        #endregion

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/

            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");


            ConsoleDisplayN18.Screen = display_N18;
            State startState = new SplashScreenState(this);
            ErrorState errorState = new ErrorState(this);
            _Context = new Context(startState, errorState);
            _Context.Start();
        }

    }
}
