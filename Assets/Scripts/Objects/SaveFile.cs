using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class SaveFile : MonoBehaviour {
	public int HighScore;
	public string PlayerName;
	public string LastLevel;
	public string LastCheckpoint;

	public void GameSave()
	{
		File.WriteAllText(Directory.GetCurrentDirectory() + "/Data/save.json",JsonUtility.ToJson(this));
		Debug.Log("Saved game to " + Directory.GetCurrentDirectory() + "/Data/save.json");
	}

	public static SaveFile GameLoad(string savedData)
	{
		//Check to see if the Save file exists first before attempting to load.
		if (File.Exists(Directory.GetCurrentDirectory() + "/Data/save.json"))
		{//The save file is present. Load it.
			Debug.Log("Save game detected. Attempting to load data.");
			return JsonUtility.FromJson<SaveFile>(savedData);
			
		}else
		{
			Debug.Log("No save file was found. Empty save returned.");
			return new SaveFile();
		}
	}

}
