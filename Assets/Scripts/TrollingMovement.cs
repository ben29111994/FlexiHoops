using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollingMovement : MonoBehaviour {
    public GameObject player;
    public float escapeTime = 3;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(player != null && Vector3.Distance(player.transform.position, transform.position) <= 2 && escapeTime >= 0)
        {
            escapeTime--;
            this.GetComponent<Rigidbody>().AddForce(0, 100, 0);
        }
	}
}
