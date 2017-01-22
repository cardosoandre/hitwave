using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class TimerSliderUI : MonoBehaviour
{
    public float[] starts;
    public float[] ends;

    int stage;
    float TimerStart;
    float TimerLength;
    [Inject]
    WaveManager mngr;
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        mngr.OnAdvancedStage += Mngr_OnAdvancedStage;
        mngr.OnStartNewTimer += SetTo;
    }

    private void Mngr_OnAdvancedStage()
    {
        stage = mngr.CurrentStage;
        TimerStart = Time.time;
        TimerLength = mngr.Timers[mngr.CurrentStage];
    }

    void Update()
    {
        float interp = ((Time.time) - TimerStart) / TimerLength;
        if (interp > 1)
        {
            interp = 1;
        }
        slider.value = Mathf.Lerp(starts[stage], ends[stage], interp);
    }
    public void SetTo(float f)
    {
    }
}
