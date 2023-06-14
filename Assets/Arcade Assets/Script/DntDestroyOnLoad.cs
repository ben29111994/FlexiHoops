using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DntDestroyOnLoad : MonoBehaviour
{

	public static DntDestroyOnLoad bgMusicInstance;

	void Awake ()
	{
		if (bgMusicInstance) {
			DestroyImmediate (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			bgMusicInstance = this;
		}
	}
}