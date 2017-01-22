using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneInstaller:MonoInstaller {
    public Map map;
    public WaveManager waveManager;
    public MapConfigs configs;
    
    public override void InstallBindings()
    {
        Debug.Assert(map != null);
        Debug.Assert(waveManager != null);
        Container.Bind<Map>().FromInstance(map);
        Container.Bind<WaveManager>().FromInstance(waveManager);
        Container.Bind<MapConfigs>().FromInstance(configs);
        Container.Bind<Player>().AsSingle();
        
    }
}
