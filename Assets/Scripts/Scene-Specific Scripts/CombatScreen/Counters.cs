using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counters : MonoBehaviour {

	public int CurrentErrors;
	public int CurrentCombo;
	public int CurrentPlayerHealth = 100;

	public int BestCombo;
	public bool Transitioning = false;
	public void IncrementErrors()
	{
		CurrentErrors++;
	}

	void Start()
	{
		DontDestroyOnLoad(this);
	}

	public void MovingLevels(int PlayerHealth)
	{
		Transitioning = true;
		CurrentPlayerHealth = PlayerHealth;
	}
	public void ResetComboCounter()
	{
		CurrentCombo = 0;
	}

	public void IncrementCombo()
	{
		CurrentCombo++;
		if (CurrentCombo < BestCombo)
		{
			BestCombo = CurrentCombo;
		}
	}
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "CombatLevel2" && Transitioning == true)
		{//The scene has moved. Populate the data from the previous round accordingly.
			GameObject.Find("lblWPM").GetComponent<Text>().text = GameObject.Find("SystemContainer").GetComponent<PerSecondManager>().CurrentWPM.ToString();
			GameObject.Find("lblErrors").GetComponent<Text>().text = CurrentErrors.ToString();
			GameObject.Find("lblCombo").GetComponent<Text>().text = CurrentCombo.ToString();
			GameObject.Find("PlayerHealth").GetComponent<Slider>().value = CurrentPlayerHealth;
			Transitioning = false;
		}
		GameObject.Find("lblErrors").GetComponent<Text>().text = CurrentErrors.ToString();
		GameObject.Find("lblCombo").GetComponent<Text>().text = CurrentCombo.ToString();
	}
}
