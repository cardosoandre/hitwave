using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    Tile[,] tiles;


    [HideInInspector]
    public int Width, Height;
    [HideInInspector]
    public float tileWidth, tileDepth, blockHeight;
    public GameObject BlockPrefab;
    public GameObject TilePrefab;


    
    void Awake()
    {
        SetupTiles();
    }
	void Start () {
	}
	void Update () {
		
	}

    void SetupTiles()
    {
        tiles = new Tile[Width, Height];
        int i = 0;
        foreach (Transform row in transform)
        {
            int j = 0;
            foreach (var item in row.GetComponentsInChildren<Tile>())
            {
                tiles[i, j] = item;
                j++;
            }
            i++;
        }
    }
    
    public Vector3 GetTargetPositionFor(int x, int y, int z)
    {
        return transform.position + new Vector3((x + 0.5f) * tileWidth , (y + 0.5f) * blockHeight, (z + 0.5f) * tileDepth);
    }
    public Tile GetTileAt(int x, int z)
    {
        if (x < 0 || z < 0 || x >= Width || z >= Height)
            return null;
        return tiles[x, z];
    }
}
