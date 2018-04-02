using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orts.Viewer3D.WebServices
{
    /// <summary>
    /// Publishes values for all controls in the cabview - currently readonly
    /// </summary>
    public class ApiCabControls
    {
        public class ApiData
        {
            public string ControlType;
            public double ControlMinValue;
            public double ControlMaxValue;
            public float ControlRangeFraction;
        }

        public static Viewer Viewer;

        public object GetCabControls(string parameters)
        {
            var dataList = new List<ApiData>();

            var playerLocomotive = Program.Simulator.PlayerLocomotive;
            if (playerLocomotive != null)
            {
                var locoViewer = Viewer.PlayerLocomotiveViewer as Orts.Viewer3D.RollingStock.MSTSLocomotiveViewer;
                var controlList = locoViewer._CabRenderer.CabViewControlRenderersList;
                var i = (locoViewer.Locomotive.UsingRearCab) ? 1 : 0;
                foreach (var cvcr in controlList[i])
                {
                    var controlData = new ApiData();
                    controlData.ControlType = cvcr.GetControlType().ToString();
                    controlData.ControlMinValue = cvcr.Control.MinValue;
                    controlData.ControlMaxValue = cvcr.Control.MaxValue;
                    controlData.ControlRangeFraction = cvcr.GetRangeFraction();

                    dataList.Add(controlData);
                }
            }

            return dataList;
        }

        public object CabControls(string parameters, Viewer viewer)
        {
            var dataList = new List<ApiData>();

            var playerLocomotive = Program.Simulator.PlayerLocomotive;
            if (playerLocomotive != null)
            {
                var locoViewer = viewer.PlayerLocomotiveViewer as Orts.Viewer3D.RollingStock.MSTSLocomotiveViewer;
                var controlList = locoViewer._CabRenderer.CabViewControlRenderersList;
                var i = (locoViewer.Locomotive.UsingRearCab) ? 1 : 0;
                foreach (var cvcr in controlList[i])
                {
                    var controlData = new ApiData();
                    controlData.ControlType = cvcr.GetControlType().ToString();
                    controlData.ControlMinValue = cvcr.Control.MinValue;
                    controlData.ControlMaxValue = cvcr.Control.MaxValue;
                    controlData.ControlRangeFraction = cvcr.GetRangeFraction();

                    dataList.Add(controlData);
                }
            }

            return dataList;
        }
    }
}
