using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerInput : MonoBehaviour {

	//The colors for text are supported in the following style:   <color=#hexcode>t</color> - This will need to be inserted before the typed character and </color> after to show richly. 
	public string NextCharacter;
	public string InputAlreadyEntered;

	bool IsAcceptingInput = false; //Can the player type?

	GameObject SystemContainer;
	GameObject WordBox;
	void Start()
	{
		SystemContainer = GameObject.Find("SystemContainer"); //Load the SystemContainer, you'll need it later.
		WordBox = GameObject.Find("txtWordBox"); //Load the WordBox for updating the visuals if a press matches what its supposed to be or not.
		
	}


	public void BeginGame()
	{//This Method is going to be in all the appropriate systems that are working in tandem that denotes that the word list has been loaded in memory and the player can begin typing.
		Debug.Log("Player Input is now being listened to.");
		
		IsAcceptingInput = true;
	}

	public void RoundFinished()
	{
		IsAcceptingInput = false;
	}

	void Update () {
	//Handle input
		if (IsAcceptingInput == true)
		{
			if (Input.GetKey(KeyCode.Return) == false && Input.GetKey(KeyCode.Backspace) == false)
			{
				if (Input.inputString != "")
				{
					GameObject.Find("txtWordBox").GetComponent<WordBox>().SendMessage("CheckLetter",Input.inputString);
					GameObject.Find("playerInput").GetComponent<Text>().text += Input.inputString;
					GameObject.Find("SystemContainer").GetComponent<PerSecondManager>().SendMessage("IncrementCharacters");
				}
				if (Input.inputString == " ")
				{//The player reset the box.

					//Reset the input box.
					GameObject.Find("playerInput").GetComponent<Text>().text = "";
					
				}
				
			}
				
				
			}
				
			

	}
	void LateUpdate()
	{//Maintain the currently shown input.

	}

	void WordCompleted()
	{

	}
}
