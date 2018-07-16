// COPYRIGHT 2009, 2010, 2011, 2012, 2013, 2014 by the Open Rails project.
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

// This file is the responsibility of the 3D & Environment Team. 


using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Orts.Viewer3D;
using Orts.Viewer3D.WebServices;
using ORTS.Common;
using ORTS.Settings;
using Orts.Processes;
using System.Net.Sockets; // for Socket

namespace Orts.Viewer3D.Processes
{
    public class WebServerProcess
    {
        public readonly Profiler Profiler = new Profiler("WebServer");
        readonly ProcessState State = new ProcessState("WebServer");
        readonly Game Game;
        readonly Thread Thread;
        //readonly WatchdogToken WatchdogToken;
        //readonly CancellationTokenSource CancellationTokenSource;

        WebServer webServer;
        string LocalIp4Address;
        int Port = 2150; // Port is not officially reserved for any other application - CJ 1-Apr-2018
        //string WebPath = @"\Open Rails\Program\Content\Web";

        public WebServerProcess(Game game)
        {
            Game = game;
            LocalIp4Address = GetLocalIp4Address();

            Thread = new Thread(WebServerThread);
        //    WatchdogToken = new WatchdogToken(Thread);
        //    WatchdogToken.SpecialDispensationFactor = 6;    // ???
        //    CancellationTokenSource = new CancellationTokenSource(WatchdogToken.Ping);
        }

        public void Start()
        {
        //    Game.WatchdogProcess.Register(WatchdogToken);
            Thread.Start();
        }

        public void Stop()
        {
            webServer.Stop();

        //    Game.WatchdogProcess.Unregister(WatchdogToken);
        //    CancellationTokenSource.Cancel();
            State.SignalTerminate();
        }

        public bool Finished
        {
            get
            {
                return State.Finished;
            }
        }

        /// <summary>
        /// Returns a token (copyable object) which can be queried for the cancellation (termination) of the loader.
        /// </summary>
        /// <remarks>
        /// <para>
        /// All loading code should periodically (e.g. between loading each file) check the token and exit as soon
        /// as it is cancelled (<see cref="CancellationToken.IsCancellationRequested"/>).
        /// </para>
        /// <para>
        /// Reading <see cref="CancellationToken.IsCancellationRequested"/> causes the <see cref="WatchdogToken"/> to
        /// be pinged, informing the <see cref="WatchdogProcess"/> that the loader is still responsive. Therefore the
        /// remarks about the <see cref="WatchdogToken.Ping()"/> method apply to the token regarding when it should
        /// and should not be used.
        /// </para>
        /// </remarks>
        //public CancellationToken CancellationToken
        //{
        //    get
        //    {
        //        return CancellationTokenSource.Token;
        //    }
        //}

        public void WaitTillFinished()
        {
           State.WaitTillFinished();
        }

        /// <summary>
        /// Gets IP4 address for local network (if there is one).
        /// Based on https://stackoverflow.com/questions/6803073/get-local-ip-address
        /// </summary>
        /// <returns></returns>
        private string GetLocalIp4Address()
        {
            string localIp4;
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIp4 = endPoint.Address.ToString();
                }
                return localIp4;
            }
            catch
            {
                //throw new Exception("No network adapters found with an IPv4 address");
                return null;
            }
        }

        [ThreadName("WebServer")]
        void WebServerThread()
        {
            Profiler.SetThread();
            Game.SetThreadLanguage();

            //webServer = new WebServer(LocalIp4Address, Port, 1, WebPath);
            webServer = new WebServer(LocalIp4Address, Port, 1);
            webServer.Run();
        }
    }
}
