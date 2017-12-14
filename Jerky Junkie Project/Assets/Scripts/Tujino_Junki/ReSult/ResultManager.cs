using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : SingletonMonoBehaviour<ResultManager>//シングルトンを継承
{
	public bool isResult = false;//リザルトを開いている状態

	public void ResultOn()//リザルトを開くときに
	{
		isResult = true;//フラグを立てる
	}

	public void ResultOff()//リザルトを閉じる時に
	{
		isResult = false;//フラグを下す
	}

}
