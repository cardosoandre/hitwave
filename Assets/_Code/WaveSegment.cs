using System;
using UnityEngine;

public class WaveSegment : MonoBehaviour
{
    public int startPower = 3;
    public bool isDead = false;
    enum State { Going, Still, GoBack}
    State state = State.Going;
    public int currentPower { get; private set; }
    public Vector3 speed;
    public float maxRandomOffset = .5f;
    
    public event Action<WaveSegment, Tile> OnStoppedBy;
    public event Action<WaveSegment> OnDie;
    public event Action<WaveSegment> OnBackToOcean;
    public event Action<WaveSegment, Tile> OnHit;

    //SimpleMovement mov;

    private void Start()
    {
        //mov = GetComponent<SimpleMovement>();
        currentPower = startPower;
        float f = UnityEngine.Random.RandomRange(-maxRandomOffset, maxRandomOffset);
        transform.position += Vector3.forward * f;
    }
    private void Update()
    {
        Vector3 move = Vector3.zero;
        switch (state)
        {
            case State.Going:
                move = speed;
                break;
            case State.Still:
                move = Vector3.zero;
                break;
            case State.GoBack:
                move = -speed;
                break;
            default:
                break;
        }
        transform.position += move * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        var tile = other.GetComponent<Tile>();
        if(tile != null && ! isDead)
        {
            Hit(tile);
        }
        var killer = other.GetComponent<WaveKiller>();
        if(killer != null)
        {
            if (!killer.destroyer)
                Kill();
            else
                DestroyWholeWave();
        }
    }

    void Hit(Tile t)
    {
        Debug.Assert(currentPower > 0);
        //Wave Gets Destroyed
        if(t.Resistance >= currentPower)
        {
            t.HitWith(currentPower);
            if(OnStoppedBy != null)
            {
                OnStoppedBy(this, t);
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
    public void GoBack()
    {
        state = State.GoBack;
    }
    public void Kill()
    {
        if(OnDie != null)
        {
            OnDie(this);
        }
        currentPower = 0;
        isDead = true;
        if(state == State.Going)
            state = State.Still;
    }

    public void DestroyWholeWave()
    {
        if (OnBackToOcean != null)
        {
            OnBackToOcean(this);
        }
    }
}