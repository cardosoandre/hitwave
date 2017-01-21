using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MeshRenderer))]
public class Tile : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    MeshRenderer meshRenderer;
    LinkedList<Block> blocks;


    public Material Standard;
    public Material Hover;

    //[HideInInspector]
    public int PosX, PosZ;


    public Map Map{ get; set; }
    public int Height{ get; private set; }




    void Awake()
    {
        Map = GetComponentInParent<Map>();
        blocks = new LinkedList<Block>();
        foreach (Transform child in transform)
        {
            blocks.AddLast(child.GetComponent<Block>());
        }
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void OnDisable()
    {
        if(meshRenderer != null)
            meshRenderer.material = Standard;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        meshRenderer.material = Hover;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        meshRenderer.material = Standard;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
            Raise();
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Lower();
        }
    }

    public void Raise()
    {
        var block = GameObject.Instantiate(Map.BlockPrefab);
        block.transform.parent = transform;
        block.transform.position = Map.GetTargetPositionFor(PosX, Height, PosZ);
        block.transform.SetAsFirstSibling();
        blocks.AddFirst(block.GetComponent<Block>());
        Height++;

        Debug.Assert(blocks.First.Value != null);
    }
    public void Lower()
    {
        Destroy(blocks.First.Value.gameObject);
        blocks.RemoveFirst();
        Height--;
    }
}
