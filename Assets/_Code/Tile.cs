using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(MeshRenderer))]
public class Tile : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler {
    [Inject]
    MapConfigs configs;
    [Inject]
    Player player;
    [Inject]
    Map Map;
    [Inject]
    CursorManager cursorManager;

    MeshRenderer mr;
    LinkedList<Block> blocks;

    
    public bool isCastle = false;
    public event Action OnBroughtDown;



    [HideInInspector]
    public int PosX, PosZ;

    public int Height{ get; private set; }
    public bool CanBuildHere
    {
        get
        {
            if (Height == configs.BlockHealthValues.Length-1)
                return false;
            int baseNeighbors = 0;
            foreach (var neighbor in GetNeighbors())
            {
                if (neighbor.Height >= Height)
                    baseNeighbors++;
            }
            return baseNeighbors >= 2;
        }
    }
    public bool CanDigHere
    {
        get
        {
            if(Height == -configs.MaxDig || isCastle && Height == 1)
                return false;

            return true;
        }
    }


    public int Resistance
    {
        get; private set;
    }


    private void Update()
    {
        //if (!CanBuildHere)
        //    meshRenderer.material = Blocked;
        //else if (HasCursor)
        //    meshRenderer.material = Hover;
        //else
        //    meshRenderer.material = Standard;
    }
    void Awake()
    {
        Map = GetComponentInParent<Map>();
        blocks = new LinkedList<Block>();
        foreach (Transform child in transform)
        {
            blocks.AddLast(child.GetComponent<Block>());
        }
        mr = GetComponent<MeshRenderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var block in blocks)
        {
            if(cursorManager.CurrentMode == CursorManager.Mode.Forma)
            {
                block.OnHover(CanBuildHere);
            }
            else if(cursorManager.CurrentMode == CursorManager.Mode.Pa)
            {
                block.OnHover(CanDigHere);
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var block in blocks)
        {
            block.OnIdle();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        

    }

    public void Raise()
    {
        
        var block = GameObject.Instantiate(configs.BlockPrefab);
        block.transform.parent = transform;
        block.transform.position = Map.GetTargetPositionFor(PosX, Height, PosZ);
        block.transform.SetAsFirstSibling();
        blocks.AddFirst(block.GetComponent<Block>());
        Height++;
        
        if(Height > 0)
        {
            Resistance = configs.BlockHealthValues[Height];
        }

        UpdateBlocks();

        Debug.Assert(blocks.First.Value != null);
    }
    public void Lower()
    {
        Destroy(blocks.First.Value.gameObject);
        blocks.RemoveFirst();
        Height--;
        if(Height == 0)
        {
            if(OnBroughtDown != null)
            {
                OnBroughtDown();
            }
        }
        UpdateBlocks();
    }
    public List<Tile> GetNeighbors(bool includeNull = false)
    {
        var result = new List<Tile>();
        result.Add(Map.GetTileAt(PosX - 1, PosZ));
        result.Add(Map.GetTileAt(PosX, PosZ - 1));
        result.Add(Map.GetTileAt(PosX + 1, PosZ));
        result.Add(Map.GetTileAt(PosX, PosZ + 1));
        if(!includeNull)
            result.RemoveAll((Tile t) => t == null);
        return result;
    }

    public void HitWith(int damage)
    {
        Resistance -= damage;
        for (int i = Height; i > 0 && Resistance < configs.BlockHealthValues[i-1] ; i--)
        {
            Lower();
        }
        if( Resistance != configs.BlockHealthValues[Height-1])
        {
            blocks.First.Value.SetDamaged();
        }
    }

    public void Flatten()
    {
        Resistance = 0;
        while (Height > 0)
        {
            Lower();
        }
        while(Height < 0)
        {
            Raise();
        }
    }

    public Vector3 GetTip()
    {
        return Map.GetTargetPositionFor(PosX, Height, PosZ);
    }
    public void UpdateBlocks()
    {

        //int i = -configs.MaxDig - configs.ExtraGroundBlock;
        int i = Height-1;
        bool top = true;
        foreach (var block in blocks)
        {
            block.UpdateVisual(i, top);
            Debug.Log(string.Format("{0}, {1}, Height({2}", i, top, Height), block);
            i--;
            top = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //if (eventData.button == PointerEventData.InputButton.Left)
        if (cursorManager.CurrentMode == CursorManager.Mode.Forma)
        {
            if (CanBuildHere && player.Sand > 0)
            {
                player.Sand--;
                Raise();

                GameObject.Instantiate(configs.formaVisual).transform.position = transform.position + Vector3.up * Height;
            }
        }
        else
        //if (eventData.button == PointerEventData.InputButton.Right)
        if (cursorManager.CurrentMode == CursorManager.Mode.Pa)
        {
            if (CanDigHere)
            {
                player.Sand++;
                Lower();

                GameObject.Instantiate(configs.paVisual).transform.position = transform.position + Vector3.up * Height;
            }
        }
    }
}
