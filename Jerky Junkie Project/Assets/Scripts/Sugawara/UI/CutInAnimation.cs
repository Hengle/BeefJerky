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
	[SerializeField] RectTransform rectTransform;

	[SerializeField] float openSpeed = 0.4f;
	[SerializeField] float stopSpeed = 0.1f;
	[SerializeField] float stopSpeed2 = 0.1f;
	[SerializeField] float closeSpeed = 0.4f;

	private int animStats = 0;

	[SerializeField] private AnimationCurve animCurve;
	private float curveRate = 0.0f;

	[SerializeField] private float animSpeed = 1.0f;

	public void PlayAnimation()
	{
		animStats = 1;
	}

	private void Update()
	{
		if (animStats > 0)
		{

		}
	}
	/*
	[SerializeField] float startPosX = 1080.0f;
	[SerializeField] float endPosX = 0.0f;

	[SerializeField] float inSpeed = 1.0f;
	[SerializeField] float stopSpeed = 0.5f;
	[SerializeField] float outSpeed = 1.0f;

	[SerializeField] RectTransform cutInTransform = null;

	public void PlayAnimation()
	{
		
		var sequence = DOTween.Sequence();
		sequence.Append(
			cutInTransform.DOLocalMoveX(0,inSpeed)
		);

		sequence.Append(cutInTransform.DOLocalMoveX(-30, stopSpeed));
		sequence.Append(
			cutInTransform.DOLocalMoveX(endPosX, outSpeed)).OnComplete(() =>
			{
				cutInTransform.position = new Vector3(startPosX,cutInTransform.position.y,cutInTransform.position.z);
				Debug.Log(cutInTransform.position.x);
			}).SetEase(Ease.Linear);
			
	}*/
}
