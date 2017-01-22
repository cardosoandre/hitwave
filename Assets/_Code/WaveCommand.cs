using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu()]
public class WaveCommand : ScriptableObject
{
    public bool IsJustWait = false;
    public List<WaveSegment> Segments;
    [Serializable]
    public class WaveSegment
    {
        public int power;
        public int HowMany;
    }
}