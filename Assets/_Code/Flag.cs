using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Flag : MonoBehaviour {

    [Inject]
    Tile castle;

	void Start () {
        castle.isCastle = true;
        castle.Raise();
	}
	
	void Update () {
        transform.position = castle.GetTip();
	}
}
