using UnityEngine;
using System.Collections;

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
}
