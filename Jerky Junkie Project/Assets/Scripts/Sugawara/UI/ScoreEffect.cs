using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreEffect : MonoBehaviour {

	[SerializeField] float startTime = 0.0f;
	[SerializeField] float animSpeed = 0.0f;
	private RectTransform rectTransform;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		Invoke("HighScoreEffect",startTime);
	}

	private void HighScoreEffect()
	{
		Sequence seq = DOTween.Sequence();
		seq.Append(rectTransform.DOScale(3,animSpeed));
		seq.Append(rectTransform.DOScale(0, animSpeed));
		seq.SetLoops(-1);
	}
}
