using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//This class is responsible for randomly generating the words based on the current level.  The lists that are available are editable by the player.

public  class WordGenerator : MonoBehaviour {

	 //Locate the Word Box for loading of new generated words


	enum DifficultyLevel
	{
		Level1,
		Level2,
		ChallengeMode1,
		CooldownRound1,
		CooldownRound2
	}

	string[] Level1WordList;
	string[] Level2WordList;
	string[] ChallengeLevel1WordList;
	string[] CooldownRound1WordList;

	int Level2WordLocation = 0; //Index of where the current place is in the Level 2 array.

	DifficultyLevel CurrentDifficulty = DifficultyLevel.Level1; //This determines the list that is pulled from to generate the random words.

	void Start () {
		//Cache the word lists here.
		//WordBox = GameObject.Find("txtWordBox");
		Level1WordList = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Assets/WordLibraries/Level1.txt");
		Debug.Log("Cached Level 1 Word List. Number of words loaded: " + Level1WordList.Length.ToString());
		Level2WordList = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Assets/WordLibraries/Level2.txt");
		Debug.Log("Cached Level 2 Word List. Number of words loaded: " + Level2WordList.Length.ToString());
		CooldownRound1WordList = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Assets/WordLibraries/CooldownRoundList.txt");
		Debug.Log("Cached Cooldown Round 1 Word List. Number of words loaded: " + CooldownRound1WordList.Length.ToString());
		
		System.Random rnd = new System.Random();
		RandomExtension.Shuffle(rnd, Level1WordList);
		Debug.Log("Shuffled Level 1 Word List.");
		
	}

	public void GetNextPage()
	{//Gets the next 15 words in level 2's word list.
		int NumGeneratedWords = 0;		
		ShuffleWordLists();
		GameObject WordBox = GameObject.Find("txtWordBox");
		WordBox.GetComponent<Text>().text = "";
		string CurrentContents = WordBox.GetComponent<Text>().text; 
		while (NumGeneratedWords != 15)
		{
			CurrentContents += Level2WordList[NumGeneratedWords] + " ";
			NumGeneratedWords++;
		}
		WordBox.GetComponent<Text>().text = CurrentContents;

		Debug.Log("LOADED LIST 2");
	}
	
	public void GenerateWordList(int NumberOfWordsToGenerate)
	{//This is callable with SendMessage("GenerateWordList", <Number>) and will send the display object the number of words requested for the currently-set difficulty.
		int NumGeneratedWords = 0;		
		ShuffleWordLists();
		GameObject WordBox = GameObject.Find("txtWordBox");
		WordBox.GetComponent<Text>().text = "";
		string CurrentContents = WordBox.GetComponent<Text>().text; 
		while (NumGeneratedWords != NumberOfWordsToGenerate)
		{
			CurrentContents += Level1WordList[NumGeneratedWords] + " ";
			NumGeneratedWords++;
		}
		WordBox.GetComponent<Text>().text = CurrentContents;
	}

	public void ShuffleWordLists()
	{
		System.Random rnd = new System.Random();
		RandomExtension.Shuffle(rnd, Level1WordList);
		Debug.Log("Shuffled Level 1 Word List.");
		RandomExtension.Shuffle(rnd, Level2WordList);
		Debug.Log("Shuffled Level 2 Word List.");
	}
}

public static class RandomExtension
{
	public static void Shuffle<t> (this System.Random rng, t[] array)
	{
		//Fischer-Yates algorighthm to randomize the word list.
		int n = array.Length;
		while (n > 1)
		{
			int k = rng.Next(n--);
			t temp = array[n];
			array[n] = array[k];			
			array[k] = temp;
		}
	}
}
