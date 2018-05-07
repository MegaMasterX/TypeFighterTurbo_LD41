using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioContainer : MonoBehaviour {
	[SerializeField]
	AudioClip NiceCombo;
	[SerializeField]
	AudioClip GreatCombo;
	[SerializeField]
	AudioClip IncredibleCombo;
	[SerializeField]
	AudioClip Ready;
	[SerializeField]
	AudioClip Fight;
	[SerializeField]
	AudioClip FlawlessVictory;
	[SerializeField]
	AudioClip NeverSeenACombo;
	[SerializeField]
	AudioClip KeyboardMaster;
	[SerializeField]
	AudioClip GameOver;
	[SerializeField]
	AudioClip FighterDown;
	[SerializeField]
	AudioClip RoundComplete;
	[SerializeField]
	AudioClip BetterLuckNextTime;
	[SerializeField]
	AudioClip TimeForNextRound;
	[SerializeField]
	AudioClip PlayerAttack1;
	[SerializeField]
	AudioClip PlayerAttack2;
	[SerializeField]
	AudioClip PlayerAttack3;
	[SerializeField]
	AudioClip PlayerAttack4;
	void Start()
	{
		DontDestroyOnLoad(this);
	}

	public void ApplyClip(string ClipName)
	{//This accepts a sendmessage to apply the attached clip to the audio emitter game object.
		GameObject emitter = GameObject.Find("AudioEmitter");
		AudioSource source = emitter.GetComponent<AudioSource>();
		switch(ClipName)
		{
			case "Ready":
				source.clip = Ready;
				source.Play();				
				break;
			case "Fight":
				source.clip = Fight;
				source.Play();
				break;
			case "NiceCombo":
				source.clip = NiceCombo;
				source.Play();
				break;
			case "GreatCombo":
				source.clip = GreatCombo;
				source.Play();
				break;
			case "IncredibleCombo":
				source.clip = IncredibleCombo;
				source.Play();
				break;
			case "KeyboardMaster":
				source.clip = KeyboardMaster;
				source.Play();
				break;
			case "FighterDown":
				source.clip = FighterDown;
				source.Play();
				break;
			case "NeverSeenACombo":
				source.clip = NeverSeenACombo;
				source.Play();
				break;
			case "TimeForNextRound":
				source.clip = TimeForNextRound;
				source.Play();
				break;
			case "FlawlessVictory":
				source.clip = FlawlessVictory;
				source.Play();
				break;
			case "GameOver":
				source.clip = GameOver;
				source.Play();
				break;
			case "BetterLuck":
				source.clip = BetterLuckNextTime;
				source.Play();
				break;
			case "RoundComplete":
				source.clip = RoundComplete;
				source.Play();
				break;
			case "PlayerAttack1":
				source.clip = PlayerAttack1;
				source.Play();
				break;
			case "PlayerAttack2":
				source.clip = PlayerAttack2;
				source.Play();
				break;
			case "PlayerAttack3":
				source.clip = PlayerAttack3;
				source.Play();
				break;
			default:
				break;

		}
	}
}
