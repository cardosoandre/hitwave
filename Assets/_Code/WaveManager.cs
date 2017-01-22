using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaveManager : MonoBehaviour {

    [Inject]
    MapConfigs configs;
    public bool JustStart;
    WaveSpawnPoint[] SpawnPoints;
    LinkedList<Wave> waves;



    public event Action<float> OnStartNewTimer;
    public event Action OnEndAllTimers;

    public event Action OnEndLastWave;



    void Start () {
        waves = new LinkedList<Wave>();
        SpawnPoints = new WaveSpawnPoint[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            SpawnPoints[i++] = child.GetComponent<WaveSpawnPoint>();
        }

        if (JustStart)
            StartCoroutine(RunWaveSequence(configs.waveSequence));
	}
    IEnumerator RunWaveSequence(WaveSequence seq)
    {
        int i = 0;
        Wave wave = null;
        foreach (var waveCommand in seq.waves)
        {
            if (OnStartNewTimer != null)
                OnStartNewTimer(waveCommand.waitTime);
            yield return new WaitForSeconds(waveCommand.waitTime);
            Debug.LogFormat("Spawned wave {0}", i++);
            wave =RunWaveCommand(waveCommand.cmd);
        }
        if(wave != null)
        {
            wave.OnEnd += EndedLastWave;
        }
    }
    Wave RunWaveCommand(WaveCommand cmd)
    {
        GameObject waveObject = new GameObject();
        waveObject.transform.position = transform.position;
        int i = 0;
        foreach (var segment in cmd.Segments)
        {
            for (int j = 0; j < segment.HowMany; j++)
            {
                if (i >= SpawnPoints.Length)
                {
                    Debug.LogError("Too many wave Segments");
                    return null;
                }
                var seg = SpawnPoints[i].SpawnWaveSegment(segment.power);
                seg.transform.parent = waveObject.transform;
                i++;
            }
        }
        var wave = waveObject.AddComponent<Wave>();
        wave.moveDirection = configs.waveMovement;
        waves.AddLast(wave);
        wave.OnEnd += Wave_OnEnd;
        wave.name = "Wave";
        return wave;
    }

    void Wave_OnEnd(Wave obj)
    {
        Debug.LogFormat("Manager knows that {0} ended", obj.name);
        waves.Remove(obj);
    }


    void EndedLastWave(Wave obj)
    {
        if (OnEndLastWave != null)
        {
            OnEndLastWave();
        }
    }

}
