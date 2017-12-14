using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {

    public GameObject ResultCanvas;

	public WindowMove windowMove;

    public void OnResult()
    {
        ResultManager.Instance.ResultOn();//リザルトを開いた状態にする

        ResultCanvas = GameObject.Find("ResultCanvas");//ResultCanvasを検索

		windowMove.MoveOn(true);

        //ResultCanvas.GetComponent<CanvasGroup>().alpha = 1;//アルファを１にして表示

        //ResultCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;//レイに反応するようにする

        FindObjectOfType<HiScore>().HighScore();//ハイスコアかどうか判定
        FindObjectOfType<HiScore>().Save();//得点を保存


        Debug.Log("リザルト起動");
    }
}
