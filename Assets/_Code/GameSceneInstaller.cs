using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneInstaller:MonoInstaller {
    public Map map;
    public GameManager gameManager;
    public WaveManager waveManager;
    public MapConfigs configs;
    public InGameUI inGameUI;
    public Tile CastleStart;
    public Flag flag;
    
    public override void InstallBindings()
    {
        Debug.Assert(map != null);
        Debug.Assert(waveManager != null);
        Container.Bind<Map>().FromInstance(map);
        Container.Bind<GameManager>().FromInstance(gameManager);
        Container.Bind<WaveManager>().FromInstance(waveManager);
        Container.Bind<InGameUI>().FromInstance(inGameUI);
        Container.Bind<MapConfigs>().FromInstance(configs);
        Container.Bind<Player>().AsSingle();
        Container.Bind<Tile>().FromInstance(CastleStart);
        Container.Bind<Flag>().FromInstance(flag);
    }
}
