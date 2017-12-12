using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {

    public GameObject ResultCanvas;

    public void OnResult()
    {
        ResultCanvas = GameObject.Find("ResultCanvas");//ResultCanvasを検索

        ResultCanvas.GetComponent<CanvasGroup>().alpha = 1;//アルファを１にして表示

        ResultCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;//レイに反応するようにする

        FindObjectOfType<HiScore>().Save();//得点を保存
    }
}
