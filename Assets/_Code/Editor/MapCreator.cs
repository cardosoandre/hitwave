using UnityEngine;
using UnityEditor;


public class MapCreator :EditorWindow {
    
    Transform target;
    bool deletePrevious = true;
    MapConfigs configs;

    [MenuItem("Window/Map Creator")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MapCreator window = (MapCreator)EditorWindow.GetWindow(typeof(MapCreator));
        window.Show();
    }

    void OnGUI()
    {
        target = EditorGUILayout.ObjectField("Target", target, typeof(Transform), true) as Transform;
        configs = EditorGUILayout.ObjectField("Map Configuration", configs, typeof(MapConfigs), false) as MapConfigs;
        deletePrevious = EditorGUILayout.Toggle("Clear Tiles", deletePrevious);

        if (GUILayout.Button("Generate Map"))
        {
            GenerateMap();
        }
        if(GUILayout.Button("Generate Spawn Point"))
        {
            GenerateWaveSpanwers();
        }
    }

    void GenerateMap()
    {
        if (deletePrevious)
        {
            var children = target.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                var child = target.GetChild(i);
                Undo.DestroyObjectImmediate(child.gameObject);
            }
                
        }

        var Map = target.GetComponent<Map>();
        if (Map == null)
            Map = target.gameObject.AddComponent<Map>();
        for (int i = 0; i < configs.MapXSize; i++)
        {
            GameObject row = new GameObject();
            row.name = i.ToString();
            row.transform.parent = target;
            row.transform.localPosition = Vector3.right * configs.TileWidth * (i+0.5f);
            row.transform.localScale = Vector3.one;
            for (int j = 0; j < configs.MapZSize; j++)
            {
                GameObject tileObj =PrefabUtility.InstantiatePrefab(configs.TilePrefab) as GameObject;
                Tile tile = tileObj.GetComponent<Tile>();
                tileObj.name = string.Format("[{0},{1}]", i, j);
                tileObj.transform.parent = row.transform;
                tileObj.transform.localPosition = Vector3.forward * configs.TileDepth * (j + 0.5f);
                tile.PosX = i;
                tile.PosZ = j;
                for (int k = 0; k < configs.UndergroundBlocks; k++)
                {
                    GameObject block = PrefabUtility.InstantiatePrefab(configs.BlockPrefab) as GameObject;
                    block.transform.parent = tileObj.transform;
                    block.transform.position = tileObj.transform.position + Vector3.down * (configs.BlockHeight * (k + 0.5f) + 0.001f);
                }
            }
            Undo.RegisterCreatedObjectUndo(row, "Create Map");
        }
    }
    
    void GenerateWaveSpanwers()
    {
        if(deletePrevious)
        {
            var prev = FindObjectOfType<WaveManager>();
            Undo.DestroyObjectImmediate(prev.gameObject);
        }
        GameObject waveManager = new GameObject();
        waveManager.name = "Wave Manager";
        waveManager.AddComponent<WaveManager>();
        waveManager.transform.position = target.position + Vector3.back * configs.WaveSpawnerDistance;
        for (int i = 0; i < configs.MapXSize; i++)
        {
            GameObject waveSpawner = new GameObject();
            waveSpawner.name = "Spawn Point " + i;
            waveSpawner.AddComponent<WaveSpawnPoint>();
            waveSpawner.transform.parent = waveManager.transform;
            waveSpawner.transform.position = waveManager.transform.position + Vector3.right * configs.TileWidth * (.5f + i);
        }
        Undo.RegisterCreatedObjectUndo(waveManager, "Create Wave Manager");
    }   
}
