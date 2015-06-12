#pragma strict

private var survivalScript : Survivalcontroller;

function Start () 
{
	survivalScript = GameObject.Find("First Person Controller").GetComponent(Survivalcontroller);
}

function OnTriggerEnter (Col : Collider)
{
	if(Col.tag == "Player")
	{
		Destroy(gameObject);
		survivalScript.currentHealth -= 100;
	}
}