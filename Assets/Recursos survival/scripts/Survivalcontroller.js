#pragma strict

var currentHealth : float = 100.0;
var maxHealth : int = 100;

private var barLength = 0.0;

function Start()
{
	barLength = Screen.width / 8;
}

function Update()
{
	if(currentHealth <= 0)
	{
		currentHealth = 0;
	}
}

function CharacterDeath()
{
	Application.LoadLevel("2");
}

function OnGUI()
{
	
	GUI.Box(new Rect(5, 30, 100, 23), "Vida");
		
	GUI.Box(new Rect(120, 30, barLength, 20), currentHealth.ToString("0") + "/" + maxHealth);
	
}
