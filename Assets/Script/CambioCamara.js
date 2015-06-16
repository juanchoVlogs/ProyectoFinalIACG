#pragma strict
var camaraUno : Camera;
var camaraDos : Camera;

function Start () {
	camaraUno.enabled = true;
	camaraDos.enabled = false;

}

function Update () {
	if(Input.GetKeyDown("1")){
		camaraUno.enabled = true;
		camaraDos.enabled = false;
	}
	if(Input.GetKeyDown("2")){
		camaraUno.enabled = false;
		camaraDos.enabled = true;
	}
}