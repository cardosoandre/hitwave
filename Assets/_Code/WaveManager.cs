using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class WaveManager : MonoBehaviour {

    [Inject]
    MapConfigs configs;
    public bool JustStart;
    WaveSpawnPoint[] SpawnPoints;
    LinkedList<Wave> waves;



    AudioSource audioSource;
    public event Action<float> OnStartNewTimer;
    public event Action OnAdvancedStage;
    public event Action OnEndAllTimers;


    public event Action OnEndLastWave;

    public List<float> Timers { get; private set; }
    public int CurrentStage { get; private set; }

    void SetupTimers()
    {
        Timers = new List<float>();
        int i = -1;
        foreach (var item in configs.waveSequence.waves)
        {
            if (item.cmd.IsJustWait)
            {
                Timers.Add(item.waitTime);
                i++;
            }
        }
    }

    void Start () {
        audioSource = GetComponent<AudioSource>();
        waves = new LinkedList<Wave>();
        SpawnPoints = new WaveSpawnPoint[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            SpawnPoints[i++] = child.GetComponent<WaveSpawnPoint>();
        }
        SetupTimers();
        if (JustStart)
            StartCoroutine(RunWaveSequence(configs.waveSequence));
	}
    IEnumerator RunWaveSequence(WaveSequence seq)
    {
        CurrentStage = -1;
        int i = 0;
        Wave wave = null;
        foreach (var waveCommand in seq.waves)
        {
            if (OnStartNewTimer != null)
                OnStartNewTimer(waveCommand.waitTime);
            if (waveCommand.cmd.IsJustWait)
            {
                CurrentStage++;
                if(OnAdvancedStage != null)
                {
                    OnAdvancedStage();
                }
            }
            yield return new WaitForSeconds(waveCommand.waitTime);
            Debug.LogFormat("Spawned wave {0}", i++);
            if(!waveCommand.cmd.IsJustWait)
                wave = RunWaveCommand(waveCommand.cmd);
        }
        if(wave != null)
        {
            wave.OnEnd += EndedLastWave;
        }
    }
    Wave RunWaveCommand(WaveCommand cmd)
    {
        audioSource.Play();
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
        //wave.moveDirection = configs.waveMovement;
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
