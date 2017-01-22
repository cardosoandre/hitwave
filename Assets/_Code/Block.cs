using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MeshRenderer))]
public class Block : MonoBehaviour
{
    public Material Selected;
    public Material Idle;
    MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material = Idle;
    }

    public void OnHover()
    {
        mr.material = Selected;
    }
    public void OnIdle()
    {
        mr.material = Idle;
    }

    void UpdateVisual(int Height, bool isTop)
    {

    }

    internal void SetDamaged()
    {
        transform.localScale = .8f * Vector3.one;
    }
}
