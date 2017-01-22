using UnityEngine;
using Zenject;

public class WaveSpawnPoint:MonoBehaviour
{
    [Inject]
    MapConfigs configs;

    public WaveSegment SpawnWaveSegment(int power)
    {
        var wave = GameObject.Instantiate(configs.WavePrefab);
        var seg = wave.GetComponent<WaveSegment>();
        seg.startPower = power;
        wave.transform.position = transform.position;
        return seg;
    }
}