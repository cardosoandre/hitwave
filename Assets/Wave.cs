using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    //public Vector3 moveDirection;

    public List<WaveSegment> waveSegments;
    public List<WaveSegment> deadWaveSegments;
    public event Action<Wave> OnEnd;
    int destroyCommandsLeft = 0;

	void Start () {

        deadWaveSegments = new List<WaveSegment>();
        waveSegments = new List<WaveSegment>();
        foreach (Transform child in transform)
        {
            waveSegments.Add(child.GetComponent<WaveSegment>());
        }
        foreach (var waveSeg in waveSegments)
        {
            waveSeg.OnDie += WaveSeg_OnCrash;
            waveSeg.OnBackToOcean += WaveSeg_OnBackToOcean;
        }
        destroyCommandsLeft = waveSegments.Count;
	}

    private void WaveSeg_OnBackToOcean(WaveSegment obj)
    {
        destroyCommandsLeft--;
        if (destroyCommandsLeft == 0)
            Destroy(gameObject);
    }

    private void WaveSeg_OnCrash(WaveSegment seg)
    {
        deadWaveSegments.Add(seg);
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
        foreach (var waveSeg in deadWaveSegments)
        {
            waveSeg.GoBack();
        }
    }

 //   void FixedUpdate () {
 //       transform.position += Time.fixedDeltaTime * moveDirection;
	//}
}
