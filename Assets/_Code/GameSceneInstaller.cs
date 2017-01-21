using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu()]
public class GameSceneInstaller:ScriptableObjectInstaller {
    public GameObject TilePrefab;
    public GameObject BlockPrefab;
    public Map map;
    
}
