using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


		
public class TurnBattlePhase : MonoBehaviour {

	private int selectedToolbar = 0;
	private string[] toolbarStrings = {"Arma", "Cuchillo", "Esperar"};
	public Text textLifeMons;
	public Text textLifeChar ;
	private int lifeMonster = 100; 
	private int lifeCharacter = 100; 
	private minimax ia;
	private double prob11;
	private double prob12;
	private double prob21;
	private double prob22;
	private double prob31;
	private double prob32;
	private int hit1;
	private int hit2;



	public enum BattleStance{
		WIN,
		LOSE, 
		PLAYERCHOICE,
		ENEMYCHOICE,
		START
	}

	private BattleStance currentState;
	// Use this for initialization
	void Start () {	
		textLifeChar.text =  "100/100";
		textLifeMons.text = "100/100";
		currentState = BattleStance.START;
		prob11 = 0.8;
		prob12 = 0.5;
		prob21 = 0.1;
		prob22 = 0.4;
		prob31 = 0.1;
		prob32 = 0.1;
		hit1 = 15;
		hit2 = 30;
		ia = new minimax(prob11, prob12, prob21, prob22, prob31, prob32, hit1, hit2);
		 
	}
	
	// Update is called once per frame
	void Update () {

		if (lifeMonster <= 0) {
			currentState = BattleStance.WIN;
		} else if (lifeCharacter <= 0) {
			currentState = BattleStance.LOSE;
		}
		switch (currentState) {

		case (BattleStance.START):
			currentState = BattleStance.PLAYERCHOICE;
			break;
		case (BattleStance.PLAYERCHOICE):
			break;
		case (BattleStance.ENEMYCHOICE):
			Debug.Log (currentState);
			int depth = 3;
			int damage = 0;
			Node origin = new Node(1.0, ia.makeTree(depth, 40, 10, true), 30, 50, 0, "max");
			double des = ia.makeMinimax(origin, depth);
			System.Random random = new System.Random();
			int randomNumber = random.Next(0, 100);
			int hitType = ia.getDecisionHitType(des, origin);
			switch(hitType){
				case 1:
					if (randomNumber <=(prob11*100)){
						damage = hit1;
					}else if(randomNumber <= ((prob21+prob11)*100)){
						damage = 0;
					}else{
						damage = hit1*2;
					}
					break;
				case 2:
					if (randomNumber <=(prob12*100)){
						damage = hit2;
					}else if(randomNumber <= ((prob22+prob12)*100)){
						damage = 0;
					}else{
						damage = hit2*2;
					}
					break;
			}
			lifeCharacter = lifeCharacter - damage;
			textLifeChar.text = lifeCharacter.ToString() + "/100";
			currentState = BattleStance.PLAYERCHOICE;
			break;
		case (BattleStance.WIN):
			int prevLevel = PlayerPrefs.GetInt( "previousLevel" );
			Application.LoadLevel(prevLevel);
			break;
		case (BattleStance.LOSE):
			Application.LoadLevel("GameOver");
			break;

		}

	
	}

	void OnGUI(){

		// Determine which button is active, whether it was clicked this frame or not
		selectedToolbar = GUI.Toolbar (new Rect (50, 10, Screen.width - 100, 30), selectedToolbar, toolbarStrings);

		// If the user clicked a new Toolbar button this frame, we'll process their input
		if (currentState == BattleStance.PLAYERCHOICE) {
			if (GUI.changed) {
				System.Random random = new System.Random();
				int randomNumber = random.Next(0, 100);
				int damage = 0;			
				if (0 == selectedToolbar) {
					if (randomNumber <=(prob11*100)){
						damage = hit1;
					}else if(randomNumber <= ((prob21+prob11)*100)){
						damage = 0;
					}else{
						damage = hit1*3;
					}
					lifeMonster = lifeMonster - damage;
					textLifeMons.text = lifeMonster.ToString () + "/100";

				} else if (1 == selectedToolbar) {
					if (randomNumber <=(prob12*100)){
						damage = hit2;
					}else if(randomNumber <= ((prob22+prob12)*100)){
						damage = 0;
					}else{
						damage = hit2*3;
					}
					lifeMonster = lifeMonster -damage;
					textLifeMons.text = lifeMonster.ToString () + "/100";
				} else {
					textLifeMons.text = lifeMonster.ToString () + "/100";
				}
				currentState = BattleStance.ENEMYCHOICE;
			}

		}
	}

}
