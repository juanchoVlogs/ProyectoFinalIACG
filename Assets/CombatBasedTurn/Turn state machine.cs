using UnityEngine;
using System.Collections;

public class Turnstatemachine : MonoBehaviour {


	public enum BattleStates{

		START,
		PLAYERCHOICE,
		PLAYERANIMATE,
		ENEMYANIMATE,
		ENEMYCHOICE,
		LOSE,
		WIN

	}

	private BattleStates currentState;

	// Use this for initialization
	void Start () {

		currentState = BattleStates.START;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
