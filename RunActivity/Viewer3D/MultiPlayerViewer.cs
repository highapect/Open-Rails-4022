﻿// COPYRIGHT 2012, 2013, 2014, 2015 by the Open Rails project.
// 
// This file is part of Open Rails.
// 
// Open Rails is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Open Rails is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Open Rails.  If not, see <http://www.gnu.org/licenses/>.

using Orts.MultiPlayer;
using ORTS.Settings;

namespace Orts.Viewer3D
{
    public static class MultiPlayerViewer
    {
        //count how many times a key has been stroked, thus know if the panto should be up or down, etc. for example, stroke 11 times means up, thus send event with id 1
        static int PantoSecondCount;
        static int PantoFirstCount;
        static int BellCount;
        static int WiperCount;
        static int HeadLightCount;
        static int DoorLeftCount;
        static int DoorRightCount;
        static int MirrorsCount;

        public static void HandleUserInput()
        {
            //In Multiplayer, I maybe the helper, but I can request to be the controller
            if (UserInput.IsPressed(UserCommands.GameRequestControl))
            {
                MPManager.RequestControl();
            }

            if (UserInput.IsPressed(UserCommands.ControlHorn)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "HORN", 1)).ToString());

            if (UserInput.IsReleased(UserCommands.ControlHorn)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "HORN", 0)).ToString());

            if (UserInput.IsPressed(UserCommands.ControlPantograph2)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "PANTO2", (++PantoSecondCount) % 2)).ToString());

            if (UserInput.IsPressed(UserCommands.ControlPantograph1)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "PANTO1", (++PantoFirstCount) % 2)).ToString());

            if (UserInput.IsPressed(UserCommands.ControlBell)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "BELL", 1)).ToString());

            if (UserInput.IsReleased(UserCommands.ControlBell)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "BELL", 0)).ToString());

            if (UserInput.IsPressed(UserCommands.ControlBellToggle)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "BELL", (++BellCount) % 2)).ToString());

            if (UserInput.IsPressed(UserCommands.ControlWiper)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "WIPER", (++WiperCount) % 2)).ToString());

            if (UserInput.IsPressed(UserCommands.ControlDoorLeft)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "DOORL", (++DoorLeftCount) % 2)).ToString());

            if (UserInput.IsPressed(UserCommands.ControlDoorRight)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "DOORR", (++DoorRightCount) % 2)).ToString());

            if (UserInput.IsPressed(UserCommands.ControlMirror)) MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "MIRRORS", (++MirrorsCount) % 2)).ToString());

            if (UserInput.IsPressed(UserCommands.ControlHeadlightIncrease))
            {
                HeadLightCount++; if (HeadLightCount >= 3) HeadLightCount = 2;
                MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "HEADLIGHT", HeadLightCount)).ToString());
            }

            if (UserInput.IsPressed(UserCommands.ControlHeadlightDecrease))
            {
                HeadLightCount--; if (HeadLightCount < 0) HeadLightCount = 0;
                MPManager.Notify((new MSGEvent(MPManager.GetUserName(), "HEADLIGHT", HeadLightCount)).ToString());
            }

        }
    }
}
