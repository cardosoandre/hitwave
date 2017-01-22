using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour {
    public RectTransform YouLoseUI;
    public RectTransform YouWinUI;

    void Start () {
		
	}
	
	void Update () {
		
	}
    public void ShowLoseUI()
    {
        YouLoseUI.gameObject.SetActive(true);
    }
    public void ShowWinUI()
    {
        YouWinUI.gameObject.SetActive(true);
    }
}
