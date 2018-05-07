using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class WarningManager : MonoBehaviour {


	[SerializeField]
	public int CharacterRenderDelayInFrames = 6;

	GameObject TextLabel;

	int CurrentFrameCount;

	int MaxIndex;
	int CurrentIndex; //The current index of the cursor
	string WarningText; //Local container for whatever is in the Warning text file in the Word Library directory.
	bool ScrollEnabled = false; //Is the text now scrolling?
	bool HasScrollCompleted = false;
	[SerializeField]
	int TransitionDelayInSeconds = 5;
	int TransitionDelayCurrentSecondsElapsed = 0;
	int TransitionDelayCurrentMS = 0; //60 of these are one TransitionDelayCurrentSecondsElapsed.
	bool IsFading = false;
	
	// Use this for initialization
	void Start () {
		//This class kicks off the timer that spells out the warning character-by-character
		WarningText = File.ReadAllText(Directory.GetCurrentDirectory() + "/Assets/WordLibraries/Warning.txt"); //Loads the warning text from Disk.
		TextLabel = GameObject.Find("WarningLabel"); //Caches the warning label so GetComponent is easier.
		MaxIndex = WarningText.Length; //Sets the target desired length to whatever the Warning text is, allowing it to be edited later.
		ScrollEnabled = true; //This is set so that the frame counter doesn't start before the text is loaded for whatever reason. 
	}
	
	// Update is called once per frame, 60 fps, every character is rendered every 6 frames.
	
	void Update () {
		if (ScrollEnabled == true)
		{//Scrolling is currently active.
			RenderUpdate(CurrentIndex);
		}
		else if (HasScrollCompleted == true)
		{
			if (TransitionDelayCurrentSecondsElapsed == 3)
				{//Start the screen fade.
					Animator FadeAnimator = GameObject.Find("ScreenFade").GetComponent<Animator>();
					if (FadeAnimator.GetBool("IsTransitioning") == false)
					{
						//Set the animator trigger to true and let it handle it from there. 
						FadeAnimator.SetBool("IsTransitioning", true);

					}	
				}
			if (TransitionDelayCurrentMS != 60)
			{//Game runs at 60FPS, increment up the current MS
				TransitionDelayCurrentMS++;
			}else
			{//60 frames have been rendered, one second has passed.
				if (TransitionDelayCurrentSecondsElapsed != TransitionDelayInSeconds)
				{
					TransitionDelayCurrentSecondsElapsed++;
					TransitionDelayCurrentMS = 0;
				}
				else
				{//The number of seconds specified in the editor have elapsed. Transition to the Menu.
					SceneManager.LoadScene("MainMenu");
				}
				
			}
			
		}
		//Use SceneManager.LoadScene to transition as the old method of moving scenes was deprectaed a long time ago.
	}



	void RenderUpdate(int IndexToRenderTo)
	{
		string tempDisplayString = WarningText.Remove(CurrentIndex);
		TextLabel.GetComponent<Text>().text = tempDisplayString;
		if (CurrentIndex != MaxIndex - 1)
		{
			CurrentIndex++; //Increment the counter for the current index if it hasn't finished.
			GameObject.Find("KeySoundManager").SendMessage("RandomizeSFX");
		}
			
		else
		{
			ScrollEnabled = false; //The scrolling has stopped.
			HasScrollCompleted = true; //The scrolling has finished. The update timer will now delay until the screen transitions.
		}
			
		
	}
}
