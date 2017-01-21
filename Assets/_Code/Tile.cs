using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MeshRenderer))]
public class Tile : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    MeshRenderer meshRenderer;



    public Material Standard;
    public Material Hover;


    public Map Map{ get; set; }



    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void OnDisable()
    {
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
        throw new NotImplementedException();
    }
}
