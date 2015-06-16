#pragma strict

function OnTriggerEnter (other : Collider) {
	
	if(other.gameObject.tag == "Player"){
		Application.LoadLevel("EscenaFinal");
	}
	else if(other.gameObject.tag == "cuchilloRango"){
		//Application.LoadLevel("peleaJefe");
	}

}

function Start () {

}

function Update () {

}