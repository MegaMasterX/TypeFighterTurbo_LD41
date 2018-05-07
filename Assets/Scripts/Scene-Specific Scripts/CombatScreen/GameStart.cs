using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CountdownActive = true;
	}
	
	public int CountdownSeconds = 5;
	int FrameCount = 0;
	int SecondsElapsed = 0;
	public bool CountdownActive = false;
	public bool IsRound2 = false;
	public void StartCountdown()
	{//For use with round transitions
		CountdownSeconds = 5;
		SecondsElapsed = 0;
		CountdownActive = true;
		IsRound2 = true;
	}

	void Update () {
		if (CountdownActive == true)
		{
			if (FrameCount != 59)
				FrameCount++;
			else
			{
				FrameCount = 0;
				SecondsElapsed++;	
			}
			if (FrameCount == 0)
			{
				if (SecondsElapsed == 2)
				{
					GameObject temp = GameObject.Find("AudioEmitter");
					temp.GetComponent<AudioContainer>().SendMessage("ApplyClip","Ready");
					GameObject.Find("Ready").GetComponent<Animator>().SetBool("IsReady", true);

					Debug.Log("Emitted Ready");
					Debug.Log("Countdown Started");
					

				}
				if (SecondsElapsed == 3)
				{
					GameObject.Find("playerInput").GetComponent<Text>().text = "2";
				}
				if (SecondsElapsed == 4)
				{
					GameObject.Find("playerInput").GetComponent<Text>().text = "1";
				}
				if (SecondsElapsed == CountdownSeconds)
				{
					GameObject temp = GameObject.Find("AudioEmitter");
					temp.GetComponent<AudioContainer>().SendMessage("ApplyClip","Fight");
					GameObject.Find("Ready").GetComponent<Animator>().SetBool("IsFight", true);
					GameObject.Find("Ready").GetComponent<Animator>().SetBool("IsReady", false);
					GameObject.Find("playerInput").GetComponent<Text>().text = "";
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
					GameObject.Find("playerInput").GetComponent<PlayerInput>().SendMessage("BeginGame");
					GameObject.Find("txtWordBox").GetComponent<WordBox>().SendMessage("BeginGame");
					GameObject.Find("Player2_stand").GetComponent<EnemyScript>().SendMessage("ActivateEnemy");
					//CountdownSeconds = 0; //Reset the countdown for the round transition.
					CountdownActive = false; //Deactivate the countdown so it can be reactivated.

					//
					Debug.Log("GAME START!");
				}
			}
		}
	}
}
