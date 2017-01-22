using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof (Text))]
public class SandUI : MonoBehaviour {
    [Inject]
    Player player;

    Text text;

	void Start () {
        text = GetComponent<Text>();
        player.OnSandChange += Player_OnSandChange;	
	}
    void Update () {
		
	}


    private void Player_OnSandChange()
    {
        text.text = string.Format("{0}", player.Sand);
    }
}
