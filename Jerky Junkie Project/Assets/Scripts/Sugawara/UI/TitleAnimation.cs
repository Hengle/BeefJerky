using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour {

	[SerializeField] float moveSpeed = 1.0f;
	[SerializeField] float setPositionY = 0.0f;

	[SerializeField] float rotateSpeed = 0.1f;

	[SerializeField] float shakeWidth = 40.0f;
	private bool animationEnd = false;
	private float stats = 1.0f;

	private RectTransform rectTransform;
	private void Start () {
		rectTransform = GetComponent<RectTransform>();
		PlayAnimation();
	}

	private void PlayAnimation()
	{
		AnimationMove();
	}

	private void AnimationMove()
	{
		rectTransform.DOMoveY(setPositionY, moveSpeed).SetEase(Ease.OutBounce);

	}

	private void Update()
	{
		
	}
}
