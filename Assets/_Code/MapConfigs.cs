﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu()]
public class MapConfigs : ScriptableObject {
    public GameObject TilePrefab;
    public GameObject BlockPrefab;
    public int MapXSize;
    public int MapZSize;
    public int UndergroundBlocks;

    public int[] BlockHealthValues;

    public float TileWidth;
    public float TileDepth;
    public float BlockHeight;
    
}
