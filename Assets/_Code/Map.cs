using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    Tile[,] tiles;



    public int Width, Height;


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

}
