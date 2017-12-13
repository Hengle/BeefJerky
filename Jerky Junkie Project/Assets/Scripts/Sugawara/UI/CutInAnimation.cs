// 作成者    :菅原 
// 機能      :おじさんのカットインアニメーション
// 作成日    :2017/12/13
// 最終更新日:2017/12/13 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CutInAnimation : MonoBehaviour
{
	float startPosX;
	[SerializeField] float endPosX = 0.0f;

	[SerializeField] float inSpeed = 1.0f;
	[SerializeField] float outSpeed = 1.0f;

	[SerializeField] RectTransform cutInTransform = null;

	public void Awake()
	{
		startPosX = cutInTransform.position.x;

		PlayAnimation();
	}

	public void PlayAnimation()
	{
		cutInTransform.DOMoveX(0, inSpeed).OnComplete(() =>
		 {
			 cutInTransform.DOMoveX(endPosX, outSpeed).SetDelay(1.0f);
		 });
	}
}
