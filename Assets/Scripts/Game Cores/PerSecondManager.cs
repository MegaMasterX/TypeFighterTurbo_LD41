using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class manages the Character-Per-Second and Word-Per-Second calculations that will be used by various game systems.

public class PerSecondManager : MonoBehaviour {

	public int CurrentCPS; //Current calculated Characters Per Second.
	public int CurrentWPM; //Current Words Per Second, averaged out based on the length of words.
	public int NumberOfErrors; //How many errors are detected by the key parser
	public int ElapsedTimeInMinutes; //Amount of time in minutes that have elapsed.
	public int CharactersEntered; //How many characters has the player entered so far.

	public bool IsActive = false;

	public int SecondsElapsed = 0;
	public int FramesElapsed = 0;
	

	//Equation Explanation
	//Gross WPM:  (All Typed characters / 5) / Time(Minutes)   
	//Net WPM: Gross WPM - (Number of Errors / Time(Minutes)) = ((All Typed Characters / 5) - Number of Errors) / Time(Minutes)
	
	void Start()
	{

	}

	public void BeginGame()
	{//Activates WPM calculation
		IsActive = true;
	}
	void IncrementCharacters()
	{
		CharactersEntered++;
	}
	void Update () {
		//Increment the seconds timer regardless of whether or not its active.
		
		if (FramesElapsed == 60)
		{
			FramesElapsed = 0;
			if (SecondsElapsed == 60)
			{//1 minute has elapsed.
				ElapsedTimeInMinutes ++;
				IsActive = true; //Set the calculation to activate.
				SecondsElapsed = 0;
				
			}else
				SecondsElapsed++;
		}
		FramesElapsed++;
		if (IsActive == true)
		{//This way there's data; The equation can't return something insane by dividing by zero or some crap.
			CurrentWPM = (((CharactersEntered / 5) - NumberOfErrors) / ElapsedTimeInMinutes);
			GameObject.Find("lblWPM").GetComponent<Text>().text = CurrentWPM.ToString();
		}
	}
}
