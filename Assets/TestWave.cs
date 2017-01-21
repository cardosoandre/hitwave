using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWave : MonoBehaviour {
    public Vector3 v;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = v;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
