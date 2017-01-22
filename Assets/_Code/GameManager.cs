using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour {
    [Inject]
    WaveManager waveManager;
    [Inject]
    InGameUI ui;
    [Inject]
    Map map;
    [Inject]
    MapConfigs configs;
    
    enum State { Game, Win, Lose }
    State state = State.Game;

	void Start () {
        map.Castle.OnBroughtDown += Lose;
        waveManager.OnEndLastWave += () => Win();
	}
	
	void Update () {
		
	}

    public void Lose()
    {
        waveManager.gameObject.SetActive(false);
        if (state == State.Game)
        {
            state = State.Lose;
            ui.ShowLoseUI();
        }
    }
    public void Win()
    {
        waveManager.gameObject.SetActive(false);
        if (state == State.Game)
        {
            state = State.Win;
            ui.ShowWinUI();
        }
    }
}
