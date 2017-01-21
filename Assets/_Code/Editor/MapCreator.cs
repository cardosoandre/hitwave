using UnityEngine;
using UnityEditor;
using NUnit.Framework;


public class MapCreator :EditorWindow {

    int x = 5;
    int y = 5;
    float tileWidth = 1.0f;
    float tileHeight = 1.0f;
    Transform target;
    GameObject tilePrefab;
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
        x = EditorGUILayout.IntField("X Tiles", x);
        y = EditorGUILayout.IntField("Y Tiles", y);
        tileWidth = EditorGUILayout.FloatField("Tile Width", tileWidth);
        tileHeight = EditorGUILayout.FloatField("Tile Height", tileHeight);
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
        Map.Height = y;
        for (int i = 0; i < x; i++)
        {
            GameObject row = new GameObject();
            row.name = i.ToString();
            row.transform.parent = target;
            row.transform.localPosition = Vector3.forward * tileHeight * (i+0.5f);
            row.transform.localScale = Vector3.one;
            for (int j = 0; j < y; j++)
            {
                GameObject newObj =PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;
                newObj.name = string.Format("[{0},{1}]", i, j);
                newObj.transform.parent = row.transform;
                newObj.transform.localPosition = Vector3.right * tileWidth * (j + 0.5f);
            }
            Undo.RegisterCreatedObjectUndo(row, "Create Map");
        }
    }
}
