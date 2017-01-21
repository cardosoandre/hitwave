using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MapConfigs : ScriptableObject {
    public GameObject TilePrefab;
    public GameObject BlockPrefab;
    public int MapXSize;
    public int MapZSize;
    public int UndergroundBlocks;

    public float TileWidth;
    public float TileDepth;
    public float BlockHeight;
    
}
