var choke : Transform;
private var puertaScript : AbrirPuertaFinal;

function Start () 
{
	puertaScript = GameObject.Find("Puerta/Activador").GetComponent(AbrirPuertaFinal);
}

function OnTriggerEnter(other : Collider){
	if (other.tag == "cuchilloRango"){
		puertaScript.minijefesMuertos += 1;
		Destroy(gameObject);
	}
}