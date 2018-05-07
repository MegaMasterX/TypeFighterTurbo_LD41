using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

	public bool IsActive = false;
	public bool PlayerDefeated = false;
	System.Random rad = new System.Random(); 
	public int SecondsElapsed = 0;
	public int FramesElapsed = 0;

	public int SecondIntervalToAttack = 2;

	public int BonusDamage = 0;
	public int TotalDamage = 1;
	
	public string CurrentPlayer2 = "Player2_stand";

	public void UpdatePlayerObjectName(string newObjectName)
	{
		CurrentPlayer2 = newObjectName;
	}

	void Update () {
		if (IsActive == true)
			{
					//Update the Timer
			FramesElapsed++;
			if (FramesElapsed == 60)
			{
				SecondsElapsed++;
				FramesElapsed = 0;
			}

			//Is the interval for seconds elapsed?
			if (SecondsElapsed == SecondIntervalToAttack)
			{
				SecondIntervalToAttack = rad.Next(1,5); //Between one and 5 seconds
				SecondsElapsed = 0; //Reset elapsed seconds interval.
				BonusDamage = 0; //Reset the bonus damage based on mistakes.
				//Do an attack
				GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Damaged");
				if (rad.Next(0,1) == 0)
				{
					GameObject.Find(CurrentPlayer2).GetComponent<Animator>().SetTrigger("IsAttacking");
					GameObject.Find("PlayerHealth").GetComponent<Slider>().value -= TotalDamage + BonusDamage;
					BonusDamage = 0;
				}else
				{
					GameObject.Find(CurrentPlayer2).GetComponent<Animator>().SetTrigger("IsAttacking2");
					GameObject.Find("PlayerHealth").GetComponent<Slider>().value -= TotalDamage + BonusDamage;
					BonusDamage = 0;
				}
			}
		}
		if (IsActive == true && GameObject.Find("PlayerHealth").GetComponent<Slider>().value == 0)
		{//The player was defeated.
			GameObject.Find("AudioEmitter").GetComponent<AudioContainer>().SendMessage("ApplyClip", "FighterDown");  
			GameObject.Find("playerInput").GetComponent<PlayerInput>().SendMessage("RoundFinished"); //Stop accepting player input in the fields.
			GameObject.Find("txtWordBox").GetComponent<Text>().text = "Round completed!!"; //Clear and replace the displayed text.
			GameObject.Find("playerInput").GetComponent<Text>().text = ""; //Clear the input box.
			GameObject.Find("pnlResults").SetActive(true);
			GameObject.Find("pnlResults").GetComponent<EndResults>().SendMessage("GetResults");
			GameObject.Find("pnlResults").GetComponent<EndResults>().SendMessage("ActivateResults");
			GameObject.Find("Player2_stand").GetComponent<Animator>().SetTrigger("Resting");
			GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Defeated");
			PlayerDefeated = true;
			GameObject.Find("pnlResults").GetComponent<Animator>().SetTrigger("OpenResults");

			IsActive = false;
		}
		if (IsActive == true && GameObject.Find("EnemyHealth").GetComponent<Slider>().value == 0)
		{//The player's defeated the enemy; Set the trigger to fire the defeated animation and play the Fighter Down sound.
			if (IsActive == true)
			{
				GameObject.Find("AudioEmitter").GetComponent<AudioContainer>().SendMessage("ApplyClip", "FighterDown");  
				//GameObject.Find("EnemyHealth").SetActive(false); //Deactivate the enemy health bar.
				//Set the player animation to normal stance with a trigger.
				GameObject.Find("playerInput").GetComponent<PlayerInput>().SendMessage("RoundFinished"); //Stop accepting player input in the fields.
				GameObject.Find("txtWordBox").GetComponent<Text>().text = "Round completed!!"; //Clear and replace the displayed text.
				
				GameObject.Find("playerInput").GetComponent<Text>().text = ""; //Clear the input box.

				//Transition to the Results Scene.
				GameObject.Find("pnlResults").SetActive(true);
				GameObject.Find("pnlResults").GetComponent<EndResults>().SendMessage("GetResults");
				GameObject.Find("pnlResults").GetComponent<EndResults>().SendMessage("ActivateResults");
				GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Resting");
				GameObject.Find("Player2_stand").GetComponent<Animator>().SetTrigger("IsDefeated");
				GameObject.Find("pnlResults").GetComponent<Animator>().SetTrigger("OpenResults");
				IsActive = false;

				
			}
			
		}
	}

	void ActivateEnemy()
	{
		IsActive = true;
	}
	public void IncreaseDamage()
	{
		BonusDamage++;
	}
}
