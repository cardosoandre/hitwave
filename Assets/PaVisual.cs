using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaVisual : MonoBehaviour {
    public float life = 2;
    LayerMask l;
	void Start () {
        Destroy(gameObject, life);
        //RaycastHit hit;
        //Ray r = new Ray(transform.position + Vector3.up * 3 ,Vector3.down);
        //Physics.Raycast(r, out hit, float.PositiveInfinity,  l.value);
        //if(hit.point != null)
        //    transform.position = hit.point;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
