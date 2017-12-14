using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowCry : MonoBehaviour {

    public TitleProductionTiming titleProductionTiming = null;//SEを管理しているスクリプトを取得

	// Use this for initialization
	void Start () {
        titleProductionTiming.CowCrySE();//ゲーム開始時に牛の鳴き声を鳴らす
	}
	
}
