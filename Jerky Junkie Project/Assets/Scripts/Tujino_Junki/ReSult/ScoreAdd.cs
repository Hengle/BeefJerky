using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdd : MonoBehaviour {

    public int Point;//得点の設定

    public void PointAdd()
    {
        FindObjectOfType<HiScore>().AddPoint(Point);// ハイスコアコンポーネントを取得してポイントを追加
    }
}
