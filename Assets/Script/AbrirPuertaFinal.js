#pragma strict
var AngleY : float = 90.0;

private var targetValue : float = 0.0;
private var currentValue : float = 0.0;
private var easing : float = 0.05;
var minijefesMuertos : int = 0; 
var jefeFinal : int = 0; 

var Target : GameObject;


function Update(){

	if(minijefesMuertos >= 3 && jefeFinal >= 1){
		currentValue = currentValue + (targetValue - currentValue) * easing;
		Target.transform.rotation = Quaternion.identity; 
		Target.transform.Rotate(0, currentValue, 0); 
	}

}



function OnTriggerEnter (other : Collider) {
	
	targetValue = AngleY;
	currentValue = 0;
}



function OnTriggerExit (other : Collider) {

	currentValue = AngleY;
	targetValue = 0.0;

}