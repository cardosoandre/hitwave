using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {
    public LayerMask first;
    public LayerMask second;
    public Vector3 extraDist = Vector3.up;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(r, out hit, float.PositiveInfinity, first.value); ;

        if (hit.collider != null)
        {
            Ray r2 = new Ray(hit.point + Vector3.up * 3, Vector3.down);
            Debug.DrawRay(r2.origin, r2.direction, Color.red);

            Physics.Raycast(r2, out hit, float.PositiveInfinity, second.value); ;
            if (hit.collider != null)
            {
                Debug.Log("Hit", hit.collider.gameObject);
                transform.position = hit.point + extraDist;
                Debug.DrawLine(transform.position, hit.point);
            }
        }
    }
}
