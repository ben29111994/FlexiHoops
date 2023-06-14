using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour {

	public GameObject control;

	void Update () {
		transform.position = control.transform.position;
	}


}
