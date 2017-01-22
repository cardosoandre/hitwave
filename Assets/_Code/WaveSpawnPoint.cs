using UnityEngine;
using Zenject;

public class WaveSpawnPoint:MonoBehaviour
{
    [Inject]
    MapConfigs configs;

    public void SpawnWave(int power)
    {
        var wave = GameObject.Instantiate(configs.WavePrefab);
        wave.GetComponent<WaveSegment>().startPower = power;
    }
}