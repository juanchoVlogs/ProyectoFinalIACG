#pragma strict

function OnTriggerEnter (other : Collider) {
	
	if(other.gameObject.tag == "Player"){
		Application.LoadLevel("EscenaFinal");
	}

}

function Start () {

}

function Update () {

}