using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticle : MonoBehaviour {

	public ParticleSystem sandust;
	private AudioSource aud;

	// Use this for initialization
	void Start () {

		aud = GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sanDust(){
		sandust.Play ();
		aud.Play ();
	}
}
