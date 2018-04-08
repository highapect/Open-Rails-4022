// cab_controls/cab_controls.js
var hr = new XMLHttpRequest;

function GetCabControls () {
	hr.open("GET", "/API/1/GetCabControls", true);
	hr.send("");
	hr.onreadystatechange = function () {
		if (this.readyState == 4 && this.status == 200) {
			var Obj = JSON.parse(hr.responseText);
			var Rows = Object.keys(Obj).length;
			Str = "<table>";
			Str += "<theader><th>Type</th><th>Min</th><th>Max</th><th>Fraction</th></theader>";  
			Str += "<tbody>";  
			for (var row = 0; row < Rows; row++) {
				Str += "<tr>";
				Str += "<td>" + Obj[row].ControlType + "</td>";
				Str += "<td class=number>" + Obj[row].ControlMinValue + "</td>";
				Str += "<td class=number>" + Obj[row].ControlMaxValue + "</td>";
				Str += "<td>" + Obj[row].ControlRangeFraction + "</td>";
				Str += "</tr>";
			}
			Str += "</tbody></table>";
			cab_controls.innerHTML = Str;
		}
	}
}