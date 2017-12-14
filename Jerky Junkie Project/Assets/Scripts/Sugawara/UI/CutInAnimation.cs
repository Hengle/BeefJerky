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
	[SerializeField] private RectTransform rectTransform = null;
	[SerializeField] private RectTransform frameTransform = null;

	[SerializeField] private int animStats = 0;

	[SerializeField] private AnimationCurve animCurve;
	[SerializeField] private float curveRate = 0.0f;

	[SerializeField] private float animSpeed = 0.05f;

	private float timeCount = 0.0f;

	[SerializeField] float endPosX = 0.0f;

	[SerializeField] float inSpeed = 1.0f;
	[SerializeField] float stopSpeed = 0.5f;
	[SerializeField] float outSpeed = 1.0f;

	private void Start()
	{
		PlayAnimation();
	}

	public void PlayAnimation()
	{
		animStats = 1;
		PlayAnimation2();
		PlayAnimation3();
	}

	private void Update()
	{
		if (animStats > 0)
		{
			timeCount += Time.deltaTime;
			curveRate = Mathf.Clamp(curveRate + animSpeed * Time.deltaTime,0.0f,1.0f);
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,animCurve.Evaluate(curveRate) * 400);
			frameTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, animCurve.Evaluate(curveRate) * 400);
			if (curveRate == 1)
			{
				animStats = 0;
				curveRate = 0.0f;
			}
		}
	}

	public void PlayAnimation2()
	{
		
		var sequence = DOTween.Sequence();
		sequence.Append(
			rectTransform.DOLocalMoveX(-1080 + 30,inSpeed)
		);

		sequence.Append(rectTransform.DOLocalMoveX(0-1080, stopSpeed));
		sequence.Append(
			rectTransform.DOLocalMoveX(endPosX-1080, outSpeed)).OnComplete(() =>
			{
				Destroy(this.gameObject);
			}).SetEase(Ease.Linear);
			
	}
	public void PlayAnimation3()
	{

		var sequence = DOTween.Sequence();
		sequence.Append(
			frameTransform.DOLocalMoveX(-1080 + 30, inSpeed)
		);

		sequence.Append(frameTransform.DOLocalMoveX(0 - 1080, stopSpeed));
		sequence.Append(
			frameTransform.DOLocalMoveX(endPosX - 1080, outSpeed)).OnComplete(() =>
			{
				Destroy(this.gameObject);
			}).SetEase(Ease.Linear);

	}
}
