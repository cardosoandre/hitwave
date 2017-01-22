using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaveManager : MonoBehaviour {

    [Inject]
    MapConfigs configs;
    public bool JustStart;
    WaveSpawnPoint[] SpawnPoints;

	void Start () {
        SpawnPoints = new WaveSpawnPoint[transform.childCount];
        int i = 0;
        foreach (var point in SpawnPoints)
        {
            SpawnPoints[i] = point;
        }

        if (JustStart)
            StartCoroutine(RunWaveSequence(configs.waveSequence));
	}
    IEnumerator RunWaveSequence(WaveSequence seq)
    {
        int i = 0;
        foreach (var wave in seq.waves)
        {
            yield return new WaitForSeconds(wave.waitTime);
            Debug.LogFormat("Spawned wave {0}", i++);
            RunWaveCommand(wave.cmd);
        }
    }
    void RunWaveCommand(WaveCommand cmd)
    {
        int i = 0;
        foreach (var segment in cmd.Segments)
        {
            for (int j = 0; j < segment.HowMany; j++)
            {
                if (i >= SpawnPoints.Length)
                {
                    Debug.LogError("Too many waves");
                    return;
                }
                SpawnPoints[i].SpawnWave(segment.power);

            }
        }
    }
}
