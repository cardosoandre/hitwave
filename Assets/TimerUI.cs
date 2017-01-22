using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class TimerUI : MonoBehaviour {
    Text t;
    float time;
    [Inject]
    WaveManager mngr;
	void Awake () {
        t = GetComponent<Text>();
        mngr.OnStartNewTimer += SetTo;
	}
	
	void Update () {
        time -= Time.deltaTime;
        if (time < 0)
            time = 0;
        t.text = string.Format("{0:##}",time);
	}
    public void SetTo(float f)
    {
        time = f;
    }
}
