using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneInstaller:MonoInstaller {
    public Map map;
    public MapConfigs configs;
    
    public override void InstallBindings()
    {
        Container.Bind<Map>().FromInstance(map);
        Container.Bind<MapConfigs>().FromInstance(configs);
        Container.Bind<Player>().AsSingle();
    }
}
