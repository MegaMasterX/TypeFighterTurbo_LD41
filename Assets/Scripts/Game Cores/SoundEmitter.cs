using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SoundEmitter : MonoBehaviour {

	[SerializeField]
	AudioClip KeyPress1;
	[SerializeField]
	AudioClip KeyPress2;
	[SerializeField]
	AudioClip KeyPress3;
	[SerializeField]
	AudioClip KeyPress4;
	[SerializeField]
	AudioClip KeyPress5;
	[SerializeField]
	AudioClip KeyPress6;
	[SerializeField]
	AudioClip KeyPress7;
	[SerializeField]
	AudioClip KeyPress8;
	[SerializeField]
	AudioClip KeyPress9;
	[SerializeField]
	AudioClip KeyPress10;
	[SerializeField]
	AudioClip KeyPress11;
	[SerializeField]
	AudioClip KeyPress12;
	[SerializeField]
	AudioClip KeyPress13;


	void RandomizeSFX()
	{
		//Returns a randomized SFX in slot 1-13.
		GameObject target;
		if (SceneManager.GetActiveScene().name == "WarningScreen")
		{
			target = GameObject.Find("WarningLabel");
		}else
			target = GameObject.Find("SystemContainer");
		
		AudioSource emitter = target.GetComponent<AudioSource>();
		int IndexReturned = (int)UnityEngine.Random.Range(1.0F, 13.0F);
		switch (IndexReturned)
		{
			case 1:
				emitter.clip = KeyPress1;
				emitter.Play();
				break;
			case 2:
				emitter.clip = KeyPress2;
				emitter.Play();
				break;
			case 3:
				emitter.clip = KeyPress3;
				emitter.Play();
				break;
			case 4:
				emitter.clip = KeyPress4;
				emitter.Play();
				break;
			case 5:
				emitter.clip = KeyPress5;
				emitter.Play();
				break;
			case 6: 
				emitter.clip = KeyPress6;
				emitter.Play();
				break;
			case 7:
				emitter.clip = KeyPress7;
				emitter.Play();
				break;
			case 8:
				emitter.clip = KeyPress8;
				emitter.Play();
				break;
			case 9:
				emitter.clip = KeyPress9;
				emitter.Play();
				break;
			case 10:
				emitter.clip = KeyPress10;
				emitter.Play();
				break;
			case 11:
				emitter.clip = KeyPress11;
				emitter.Play();
				break;
			case 12:
				emitter.clip = KeyPress12;
				emitter.Play();
				break;
			case 13:
				emitter.clip = KeyPress13;
				emitter.Play();
				break;
			default:
				break;
		}
		
	}
	
	void Update () {
		
	}
}
