// 作成者    :菅原 
// 機能      :タイトルシーンの遷移処理
// 作成日    :2017/12/12 
// 最終更新日:2017/12/12 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	public FadeManager fadeManager = null;

	private void Awake()
	{
		fadeManager = transform.Find("SceneFade").GetComponent<FadeManager>();
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			fadeManager.LoadLevel("Main");
		}
	}
}
