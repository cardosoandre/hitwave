using UnityEngine;
using UnityEditor;


public class MapCreator :EditorWindow {

    int x = 5;
    int z = 5;
    int UndergroundBlocks = 2;
    float tileWidth = 1.0f;
    float tileDepth = 1.0f;
    float blockHeight = 1.0f;
    Transform target;
    GameObject tilePrefab;
    GameObject blockPrefab;
    bool deletePrevious;

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
        tilePrefab = EditorGUILayout.ObjectField("Tile Prefab", tilePrefab, typeof(GameObject), false) as GameObject;
        blockPrefab = EditorGUILayout.ObjectField("Block Prefab", blockPrefab, typeof(GameObject), false) as GameObject;
        x = EditorGUILayout.IntField("X Tiles", x);
        z = EditorGUILayout.IntField("Y Tiles", z);
        UndergroundBlocks = EditorGUILayout.IntField("Underground Blocks", UndergroundBlocks);
        tileWidth = EditorGUILayout.FloatField("Tile Width", tileWidth);
        tileDepth = EditorGUILayout.FloatField("Tile Depth", tileDepth);
        blockHeight = EditorGUILayout.FloatField("Block Height", tileDepth);
        deletePrevious = EditorGUILayout.Toggle("Clear Tiles", deletePrevious);

        if (GUILayout.Button("Generate Map"))
        {
            GenerateMap();

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
        Map.Width = x;
        Map.Height = z;
        Map.tileWidth = tileWidth;
        Map.tileDepth = tileDepth;
        Map.blockHeight = blockHeight;
        Map.BlockPrefab = blockPrefab;
        Map.TilePrefab = tilePrefab;
        for (int i = 0; i < x; i++)
        {
            GameObject row = new GameObject();
            row.name = i.ToString();
            row.transform.parent = target;
            row.transform.localPosition = Vector3.right * tileWidth * (i+0.5f);
            row.transform.localScale = Vector3.one;
            for (int j = 0; j < z; j++)
            {
                GameObject tileObj =PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;
                Tile tile = tileObj.GetComponent<Tile>();
                tileObj.name = string.Format("[{0},{1}]", i, j);
                tileObj.transform.parent = row.transform;
                tileObj.transform.localPosition = Vector3.forward * tileDepth * (j + 0.5f);
                tile.PosX = i;
                tile.PosZ = j;
                for (int k = 0; k < UndergroundBlocks; k++)
                {
                    GameObject block = PrefabUtility.InstantiatePrefab(blockPrefab) as GameObject;
                    block.transform.parent = tileObj.transform;
                    block.transform.position = tileObj.transform.position + Vector3.down * (blockHeight * (k + 0.5f) + 0.001f);
                }
            }
            Undo.RegisterCreatedObjectUndo(row, "Create Map");
        }
    }
}
