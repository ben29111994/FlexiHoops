using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DntDestroySkin : MonoBehaviour {
	public static DntDestroySkin skinInstance;

    // Use this for initialization
    void Awake()
    {
        if (skinInstance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            skinInstance = this;
            DontDestroyOnLoad(gameObject);
    }
}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
