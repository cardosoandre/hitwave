using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ButtonForCursorMode :MonoBehaviour
{
    [Inject]
    CursorManager manager;
    public CursorManager.Mode mode;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetToActive);
    }

    void Update()
    {
        button.interactable = manager.CurrentMode != mode;
    }
    void SetToActive()
    {
        manager.SetMode(mode);
    }

}

