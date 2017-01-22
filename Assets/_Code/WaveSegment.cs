using System;
using UnityEngine;

public class WaveSegment : MonoBehaviour
{
    public int startPower = 3;

    public int currentPower { get; private set; }

    public event Action<WaveSegment, Tile> OnCrash;
    public event Action<WaveSegment> OnDie;
    public event Action<WaveSegment, Tile> OnHit;

    private void Start()
    {
        currentPower = startPower;
    }
    private void OnTriggerEnter(Collider other)
    {
        var tile = other.GetComponent<Tile>();
        if(tile != null)
        {
            Hit(tile);
        }
        var killer = other.GetComponent<WaveKiller>();
        if(killer != null)
        {
            Kill();
        }
    }

    void Hit(Tile t)
    {
        Debug.Assert(currentPower > 0);
        //Wave Gets Destroyed
        if(t.Resistance >= currentPower)
        {
            t.HitWith(currentPower);
            if(OnCrash != null)
            {
                OnCrash(this, t);
            }
            Kill();

        } else
        {
            currentPower -= t.Resistance;
            t.Flatten();
            if (OnHit != null)
            {
                OnHit(this, t);
            }
        }
    }

    void Kill()
    {
        if(OnDie != null)
        {
            OnDie(this);
        }
        currentPower = 0;
        Destroy(gameObject);
    }
}