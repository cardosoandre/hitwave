using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    public Vector3 moveDirection;

    public List<WaveSegment> waveSegments;
    public event Action<Wave> OnEnd;

	void Start () {
        waveSegments = new List<WaveSegment>();
        foreach (Transform child in transform)
        {
            waveSegments.Add(child.GetComponent<WaveSegment>());
        }
        foreach (var waveSeg in waveSegments)
        {
            waveSeg.OnDie += WaveSeg_OnCrash;
        }
	}

    private void WaveSeg_OnCrash(WaveSegment seg)
    {
        waveSegments.Remove(seg);
        if(waveSegments.Count == 0)
        {
            End();
        }
    }

    void End()
    {
        if(OnEnd != null)
        {
            OnEnd(this);
        }
        Destroy(this);
    }

    void FixedUpdate () {
        transform.position += Time.fixedDeltaTime * moveDirection;
	}
}
