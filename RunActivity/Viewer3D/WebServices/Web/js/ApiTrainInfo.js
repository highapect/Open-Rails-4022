var PageNo = 0;
var hr = new XMLHttpRequest;

function ApiTrainInfo() {
    hr.open("POST", "/API/TRAININFO", true);
    hr.send("pageno=" + PageNo);
    hr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var obj = JSON.parse(hr.responseText);

			enumControlMode.innerHTML = obj.ControlMode;                // present control mode 
            floatSpeedMpS.innerHTML = obj.speedMpS;                           // present speed
            floatProjectedSpeedMpS.innerHTML = obj.projectedSpeedMpS;                  // projected speed
            floatAllowedSpeedMps.innerHTML = obj.allowedSpeedMpS;                    // max allowed speed
            floatCurrentElevationPercent.innerHTML = obj.currentElevationPercent;            // elevation %
            intDirection.innerHTML = obj.direction;                            // present direction (0=forward, 1=backward)
            intCabOrientation.innerHTML = obj.cabOrientation;                       // present cab orientation (0=forward, 1=backward)
            boolIsOnPath.innerHTML = obj.isOnPath;                            // train is on defined path (valid in Manual mode only)
            // List<TrainObjectItem> ObjectInfoForward;  // forward objects
            // List<TrainObjectItem> ObjectInfoBackward; // backward objects

        }
    }
}