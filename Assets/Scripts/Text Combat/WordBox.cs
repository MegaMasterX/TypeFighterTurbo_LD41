using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class WordBox : MonoBehaviour {

	//Color Codes: Black: #000000ff. Green: #008000ff. Red: #ff0000ff. Purple: #800080ff.
	//The player's correct inputs change to Green. Errors are changed to Red.  
	//After a word is completed (A space input), Generate a 1-20 chance for the next word to be Critical, changing the word to purple (The number to detect would be 13).
	//The Display Block supports up to 330 characters.
	public string CurrentPlayer2 = "Player2_stand";

	public int CurrentPlayerIndex = 1; //The player's current index in powering through the text.
	public int CurrentLogicalIndex = 0; //This is the index accounting for the current formatting strings. <color=#000000ff></color>
	public string CurrentlyDisplayedText; //This is the contents of the box.
	public bool IsNextWordCritical = false;

	

	bool ErrorOccuredThisWord = false;

	public bool IsRound2 = false;
	public string CurrentlyLoadedText; //This is for the contents that will be checked by player input to see if its value.

	GameObject SystemContainer;
	int HitCheck = 0; //This is incremented so it does 1 damage every two words successfully typed.
	
	public void AdvanceRound()
	{
		IsRound2 = true;
	}

	public void UpdatePlayerObjectName(string newObjectName)
	{
		CurrentPlayer2 = newObjectName;
	}


	void UpdateLetter(bool wasCorrect)
	{//This updates the color of the letter provided. The color tags add 25 characters (Tag+endtag) to the length of the string.
		if (wasCorrect == true)
		{
			CurrentPlayerIndex ++;
		
			GameObject.Find("txtWordBox").GetComponent<Text>().text = CurrentlyDisplayedText;
			if (CurrentLogicalIndex != 0)
			{
				string updated = CurrentlyDisplayedText.Insert(CurrentLogicalIndex, "<color=#008000ff>"); //Inserts the color tag.
				CurrentlyDisplayedText = updated;
				updated = CurrentlyDisplayedText.Insert(CurrentLogicalIndex + 18, "</color>"); //Inserts the end tag.
				CurrentlyDisplayedText = updated;
				CurrentLogicalIndex += 26;
				
			}else
			{
				CurrentlyDisplayedText = "<color=#008000ff>" + CurrentlyLoadedText;
				string updated = CurrentlyDisplayedText.Insert(CurrentLogicalIndex + 18, "</color>"); //Inserts the end tag.
				CurrentlyDisplayedText = updated;
				CurrentLogicalIndex += 26;
				
			}
			GameObject.Find("txtWordBox").GetComponent<Text>().text = CurrentlyDisplayedText;
		}
		else if (wasCorrect == false)
		{
			GameObject.Find("SystemContainer").GetComponent<Counters>().SendMessage("IncrementErrors");
			
			CurrentPlayerIndex ++;
			
			

			//Reset the Combo Counter.
			GameObject.Find("SystemContainer").GetComponent<Counters>().SendMessage("ResetComboCounter");
			ErrorOccuredThisWord = true; // An error occured this word.
			//Update the color accordingly.
			if (CurrentLogicalIndex != 0)
			{
				string updated = CurrentlyDisplayedText.Insert(CurrentLogicalIndex, "<color=#ff0000ff>"); //Inserts the color tag.
				CurrentlyDisplayedText = updated;
				updated = CurrentlyDisplayedText.Insert(CurrentLogicalIndex + 18, "</color>"); //Inserts the end tag.
				CurrentlyDisplayedText = updated;
				CurrentLogicalIndex += 26;
				
			}else
			{
				CurrentlyDisplayedText = "<color=#ff0000ff>" + CurrentlyLoadedText;
				string updated = CurrentlyDisplayedText.Insert(CurrentLogicalIndex + 18, "</color>"); //Inserts the end tag.
				CurrentlyDisplayedText = updated;
				CurrentLogicalIndex += 26;
				
			}

			GameObject.Find("txtWordBox").GetComponent<Text>().text = CurrentlyDisplayedText;
		}

		if (CurrentPlayerIndex == CurrentlyLoadedText.Length || CurrentPlayerIndex > CurrentlyLoadedText.Length) //Is the index out of bounds somehow?
		{ //The player has reached the end. Generate a new string of text until the enemy is defeated.
			if (IsRound2 == false)
			{
				//Reset the game state.
				GameObject.Find("txtWordBox").GetComponent<Text>().text = "<Loading list...>"; //This should theoretically never be seen
				GameObject.Find("playerInput").GetComponent<Text>().text = ""; //Clear the players entered text.
				CurrentLogicalIndex = 0; //Reset the logical index.
				CurrentPlayerIndex = 0; //Reset the player index
				CurrentlyDisplayedText = ""; //Reset the Displayed Text container.
				CurrentlyLoadedText = ""; //Reset the cached Loaded Text container.
				//Generate a new set of words.
				GameObject SystemContainer = GameObject.Find("SystemContainer");
				switch(SceneManager.GetActiveScene().name)
				{
					case "CombatLevel1":
						SystemContainer.GetComponent<WordGenerator>().SendMessage("GenerateWordList", 1);
						break;
					case "CombatLevel2":
						SystemContainer.GetComponent<WordGenerator>().SendMessage("GetNextPage");
						break;
					case "CooldownLevel1":
						break;
					case "CooldownLevel2":
						break;
					case "ChallengeMode1":
						break;
					default:
						break;
				}
				CurrentlyDisplayedText = GameObject.Find("txtWordBox").GetComponent<Text>().text;
				CurrentlyLoadedText = GameObject.Find("txtWordBox").GetComponent<Text>().text;
			}else
			{
				GameObject.Find("txtWordBox").GetComponent<Text>().text = "<Loading list...>"; //This should theoretically never be seen
				GameObject.Find("playerInput").GetComponent<Text>().text = ""; //Clear the players entered text.
				CurrentLogicalIndex = 0; //Reset the logical index.
				CurrentPlayerIndex = 0; //Reset the player index
				CurrentlyDisplayedText = ""; //Reset the Displayed Text container.
				CurrentlyLoadedText = ""; //Reset the cached Loaded Text container.
				//Generate a new set of words.
				GameObject SystemContainer = GameObject.Find("SystemContainer");
				SystemContainer.GetComponent<WordGenerator>().SendMessage("GetNextPage");
			}
			

		}
	}
	
	void CheckLetter(string CharToCheck)
	{
		//If the letter is space and it matches, reset the Error check bit. If the error bit is False, the player finished the word without error.
		if (CharToCheck.ToCharArray()[0] == CurrentlyLoadedText[CurrentPlayerIndex] && CurrentPlayerIndex != CurrentlyLoadedText.Length)
		{
			
				if (CurrentlyLoadedText[CurrentPlayerIndex] == ' ')
				{//The player pressed space to start a new word.
					if (ErrorOccuredThisWord == false)
					{//The player's previous word had no errors.

						HitCheck++;
						if (HitCheck == 2)
						{
							if (GameObject.Find("SystemContainer").GetComponent<Counters>().CurrentCombo > 5)
							{
								GameObject.Find("EnemyHealth").GetComponent<Slider>().value -= 10; //Hits will do more damage after 5.
								
							}
							else
								GameObject.Find("EnemyHealth").GetComponent<Slider>().value -= 1;
							HitCheck = 0;
						}
						GameObject.Find("SystemContainer").GetComponent<Counters>().SendMessage("IncrementCombo");
						if (GameObject.Find("SystemContainer").GetComponent<Counters>().CurrentCombo == 3)
						{
							GameObject.Find("AudioEmitter").GetComponent<AudioContainer>().SendMessage("ApplyClip","NiceCombo");
							GameObject.Find("EnemyHealth").GetComponent<Slider>().value -= 10;
							System.Random rnd = new System.Random();
							if (rnd.Next(0,1) == 0)
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack1");
								GameObject.Find("EnemyHitMarker").GetComponent<Animator>().SetTrigger("IsHit");
							}
							else
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack2");
								GameObject.Find("EnemyHitMarker2").GetComponent<Animator>().SetTrigger("IsHit");
							}
								
							GameObject.Find(CurrentPlayer2).GetComponent<Animator>().SetTrigger("Damaged");
							
						}else if (GameObject.Find("SystemContainer").GetComponent<Counters>().CurrentCombo == 8)
						{
							GameObject.Find("AudioEmitter").GetComponent<AudioContainer>().SendMessage("ApplyClip","GreatCombo");
							GameObject.Find(CurrentPlayer2).GetComponent<Animator>().SetTrigger("Damaged");
							System.Random rnd = new System.Random();
							if (rnd.Next(0,1) == 0)
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack1");
								GameObject.Find("EnemyHitMarker").GetComponent<Animator>().SetTrigger("IsHit");
							}
							else
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack2");
								GameObject.Find("EnemyHitMarker2").GetComponent<Animator>().SetTrigger("IsHit");
							}
							GameObject.Find("EnemyHealth").GetComponent<Slider>().value -= 30;
						}else if (GameObject.Find("SystemContainer").GetComponent<Counters>().CurrentCombo == 15)
						{
							GameObject.Find("AudioEmitter").GetComponent<AudioContainer>().SendMessage("ApplyClip","IncredibleCombo");
							GameObject.Find("EnemyHealth").GetComponent<Slider>().value -= 50;
							System.Random rnd = new System.Random();
							if (rnd.Next(0,1) == 0)
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack1");
								GameObject.Find("EnemyHitMarker").GetComponent<Animator>().SetTrigger("IsHit");
							}
							else
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack2");
								GameObject.Find("EnemyHitMarker2").GetComponent<Animator>().SetTrigger("IsHit");
							}
							GameObject.Find(CurrentPlayer2).GetComponent<Animator>().SetTrigger("Damaged");
						}else if (GameObject.Find("SystemContainer").GetComponent<Counters>().CurrentCombo == 20)
						{
							GameObject.Find("AudioEmitter").GetComponent<AudioContainer>().SendMessage("ApplyClip","KeyboardMaster");
							GameObject.Find("EnemyHealth").GetComponent<Slider>().value -= 100;
							System.Random rnd = new System.Random();
							if (rnd.Next(0,1) == 0)
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack1");
								GameObject.Find("EnemyHitMarker").GetComponent<Animator>().SetTrigger("IsHit");
							}
							else
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack2");
								GameObject.Find("EnemyHitMarker2").GetComponent<Animator>().SetTrigger("IsHit");
							}
							GameObject.Find(CurrentPlayer2).GetComponent<Animator>().SetTrigger("Damaged");
						}else if (GameObject.Find("SystemContainer").GetComponent<Counters>().CurrentCombo == 40)
						{
							//This won't have a sound effect tied to it but it'll do a great deal of damage.
							GameObject.Find("EnemyHealth").GetComponent<Slider>().value -= 150;
							System.Random rnd = new System.Random();
							if (rnd.Next(0,1) == 0)
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack1");
								GameObject.Find("EnemyHitMarker").GetComponent<Animator>().SetTrigger("IsHit");
							}
							else
							{
								GameObject.Find("Player1").GetComponent<Animator>().SetTrigger("Attack2");
								GameObject.Find("EnemyHitMarker2").GetComponent<Animator>().SetTrigger("IsHit");
							}
							GameObject.Find(CurrentPlayer2).GetComponent<Animator>().SetTrigger("Damaged");
						}
					}else
					{
						//Increase the Enemy's damage done per-error.
						GameObject.Find(CurrentPlayer2).GetComponent<EnemyScript>().SendMessage("IncreaseDamage");
						ErrorOccuredThisWord = false;
					}
				}
			
			
		}
		
		//Checks to see if the letter matches what is in the box and calls UpdateLetter accordingly.
		if (CharToCheck.ToCharArray()[0] == CurrentlyLoadedText[CurrentPlayerIndex])
			UpdateLetter(true); 
		else
			UpdateLetter(false);

		
	}

	void Update () {
		
	}

	public void ResetState()
	{
		CurrentPlayerIndex = 0;
		CurrentLogicalIndex = 0;
	}

	public void BeginGame()
	{
		//Updates the logical strings.
		CurrentlyDisplayedText = GameObject.Find("txtWordBox").GetComponent<Text>().text;
		CurrentlyLoadedText = GameObject.Find("txtWordBox").GetComponent<Text>().text;
	}
}
