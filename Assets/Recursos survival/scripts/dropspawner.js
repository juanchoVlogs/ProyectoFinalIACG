
var spawnPoints : Transform[];  // Array of spawn points to be used.
var Prefabs : GameObject[]; // Array of different Enemies that are used.
var amount = 20;  // Total number of enemies to spawn.
var yieldTimeMin = 2;  // Minimum amount of time before spawning enemies randomly.
var yieldTimeMax = 5;  // Don't exceed this amount of time between spawning enemies randomly.

function Start(){
    Spawn();
}

function Spawn(){ 
   for (i=0; i<amount; i++){ 
      yield WaitForSeconds(Random.Range(yieldTimeMin, yieldTimeMax));  

      var obj : GameObject = Prefabs[Random.Range(0, Prefabs.length)]; 
      var pos: Transform = spawnPoints[Random.Range(0, spawnPoints.length)]; 

      Instantiate(obj, pos.position, pos.rotation); 
	} 
}  