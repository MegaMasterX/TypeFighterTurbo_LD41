using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndResults : MonoBehaviour {


	public bool IsCountdownEnabled = false;
	public bool IsResultsEnabled = false;

	

	string ResultsBreakdown = "";

	public void GetResults()
	{//Obtains all the pertinent data from various systems. 
		ResultsBreakdown += "Best Combo: " + GameObject.Find("SystemContainer").GetComponent<Counters>().BestCombo.ToString() + "\n";
		ResultsBreakdown += "Words per Minute: " + GameObject.Find("SystemContainer").GetComponent<PerSecondManager>().CurrentWPM.ToString() + "\n";
		ResultsBreakdown += "Remaining Health: " + GameObject.Find("PlayerHealth").GetComponent<Slider>().value.ToString() + "\n\n";
		GameObject.Find("txtResults").GetComponent<Text>().text = ResultsBreakdown;
		IsCountdownEnabled = true; //Enable the Countdown

	}

	public void ActivateResults()
	{
		IsResultsEnabled = true;
	}
	void Update () {
		
		if (IsResultsEnabled == true && Input.GetKeyUp(KeyCode.Return))
		{//Move to the next round!
			//GameObject.Find("EnemyHealth").SetActive(true);
			if (SceneManager.GetActiveScene().name == "CombatLevel1")
			{
				if (GameObject.Find("PlayerHealth").GetComponent<Slider>().value == 0)
				{
					SceneManager.LoadScene("GameOver - Lost");
				}else
				{
					GameObject.Find("SystemContainer").GetComponent<Counters>().SendMessage("MovingLevels", GameObject.Find("PlayerHealth").GetComponent<Slider>().value);
					SceneManager.LoadScene("CombatLevel2");
			
				}
				
				IsResultsEnabled = false;
			}else
			{
				if (GameObject.Find("EnemyHealth").GetComponent<Slider>().value == 0)
					SceneManager.LoadScene("GameOver - Won");

				if (GameObject.Find("PlayerHealth").GetComponent<Slider>().value == 0)
					SceneManager.LoadScene("GameOver - Lost");
			}
			
		}
	}
}
