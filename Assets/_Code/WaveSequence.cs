using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu()]
public class WaveSequence:ScriptableObject
{
    public List<WaveEvent> waves;
    [Serializable]
    public class WaveEvent
    {
        public WaveCommand cmd;
        public float waitTime;
        //public bool FirstWaveInStage = false;
    }
}
