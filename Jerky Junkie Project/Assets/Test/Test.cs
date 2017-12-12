using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

	void Update()
	{
		if (Input.GetMouseButtonDown(0)) //マウス左クリックしたら
		{
			SoundManager.Instance.PlayBGM("bgmA"); //キー"bgmA"に対応したBGMを鳴らす 
			SoundManager.Instance.PlayBGM("se"); //キー"se"に対応したSEを鳴らす 

		}
		if (Input.GetMouseButtonDown(1)) //マウス右クリックしたら
		{
			SoundManager.Instance.PlayBGM("bgmB");
            SoundManager.Instance.PlaySE("bgmb");

			EffectManager.Instance.PlayEffect("effectB", new Vector2(1,1), 1.0f); //キー"effectA"に対応したEffectを表示する、Vector2型、指定した時間で消滅
			EffectManager.Instance.PlayEffect("effectD", new Vector2(1,1)); //キー"effectA"に対応したEffectを表示する、Vector2型、消滅しない
		}
	}
}
