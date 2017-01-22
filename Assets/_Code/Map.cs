using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Map : MonoBehaviour {
    Tile[,] tiles;

    [Inject]
    public MapConfigs configs;



    public Tile Castle { get; private set; }


    void Awake()
    {
        SetupTiles();
        Castle = GetTileAt(configs.castleStartX, configs.castleStartZ);
    }
	void Start () {
	}
	void Update () {
		
	}

    void SetupTiles()
    {
        tiles = new Tile[configs.MapXSize, configs.MapZSize];
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
        return transform.position + new Vector3((x + 0.5f) * configs.TileWidth , (y + 0.5f) * configs.BlockHeight, (z + 0.5f) * configs.TileDepth);
    }
    public Tile GetTileAt(int x, int z)
    {
        if (x < 0 || z < 0 || x >= configs.MapXSize|| z >= configs.MapZSize)
            return null;
        return tiles[x, z];
    }
}
