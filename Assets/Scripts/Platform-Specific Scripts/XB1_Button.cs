using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class XB1_Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("XB1_Start"))
		{
			SceneManager.LoadScene("CombatLevel1");
			Debug.Log("Moving to scene based on XB1 Input");
		}
	}
}
