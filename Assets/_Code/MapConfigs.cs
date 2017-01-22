using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu()]
public class MapConfigs : ScriptableObject {
    public GameObject TilePrefab;
    public GameObject BlockPrefab;
    public GameObject WavePrefab;
    public GameObject FlagPrefab;
    public int MapXSize;
    public int MapZSize;
    public int UndergroundBlocks;
    public int MaxDig = 1;
    public int[] BlockHealthValues;
    public Vector3 waveMovement = new Vector3(0, 0, 3);

    public float TileWidth;
    public float TileDepth;
    public float BlockHeight;

    public float WaveSpawnerDistance = 1.0f;
    public WaveSequence waveSequence;
}
