using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {
    public enum Mode { Pa, Forma}
    public Mode CurrentMode { get; private set; }


    [SerializeField]
    private GameObject CursorPa;
    [SerializeField]
    private GameObject CursorForma;
    Dictionary<Mode, GameObject> cursors;


    public void SetMode (Mode mode)
    {
        if(mode != CurrentMode)
        {
            cursors[CurrentMode].SetActive(false);
            CurrentMode = mode;
            cursors[CurrentMode].SetActive(true);
        }
    }
	void Start () {
        cursors = new Dictionary<Mode, GameObject>();
        cursors[Mode.Pa] = CursorPa;
        cursors[Mode.Forma] = CursorForma;
    }
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentMode == Mode.Forma)
                SetMode(Mode.Pa);
            else
                SetMode(Mode.Forma);
        }
	}
}
