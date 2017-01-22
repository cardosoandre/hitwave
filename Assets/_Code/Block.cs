using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MeshRenderer))]
public class Block : MonoBehaviour
{
    public Material Selected;
    public Material Blocked;
    public Material Idle;
    MeshRenderer mr;
    MeshFilter mf;
    public BlockConfig blockConfig;
    

    public Mesh ShouldBeMesh;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mf = GetComponent<MeshFilter>();
        mr.material = Idle;
    }

    private void Update()
    {

    }
    public void OnHover(bool CanBuild)
    {
        if (CanBuild)
            mr.material = Selected;
        else
            mr.material = Blocked;
    }
    public void OnIdle()
    {
        mr.material = Idle;
    }

    public void UpdateVisual(int Height, bool isTop)
    {
        if(Height < 0)
        {
            ShouldBeMesh = blockConfig.GroundBlock;
        } else if(!isTop)
        {
            ShouldBeMesh = blockConfig.TowerCol;
        } else if(Height == 0)
        {
            ShouldBeMesh = blockConfig.TowerTop1;
        }
        else if (Height == 1)
        {
            ShouldBeMesh = blockConfig.TowerTop2;
        }
        else if (Height == 2)
        {
            ShouldBeMesh = blockConfig.TowerTop2;
        }
        mf.mesh = ShouldBeMesh;
    }

    internal void SetDamaged()
    {
        transform.localScale = .8f * Vector3.one;
    }
}
