using UnityEngine;

public class WaveSegment : MonoBehaviour
{

    public int startPower = 3;

    public int currentPower { get; private set; }

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
    }

    void Hit(Tile t)
    {
        Debug.Assert(currentPower > 0);
        //Wave Gets Destroyed
        if(t.Resistance >= currentPower)
        {
            t.HitWith(currentPower);
            DoDestruction();

        } else
        {
            currentPower -= t.Resistance;
            t.BringDown();
        }
    }

    void DoDestruction()
    {
        currentPower = 0;
        Destroy(gameObject);
    }
}