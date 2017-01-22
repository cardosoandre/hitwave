using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Flag : MonoBehaviour {

    [Inject]
    Map map;

	void Start () {
        map.Castle.isCastle = true;
        map.Castle.Raise();
	}
	
	void Update () {
        transform.position = map.Castle.GetTip();
	}
}
