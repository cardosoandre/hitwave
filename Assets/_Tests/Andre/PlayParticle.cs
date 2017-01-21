using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticle : MonoBehaviour {

	public ParticleSystem sandust;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sanDust(){
		sandust.Play ();
	}
}
