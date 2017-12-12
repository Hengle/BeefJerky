using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindowMove : MonoBehaviour {

	[SerializeField] private float speed = 0.0f;
	[SerializeField] private float downPosition = 0.0f;
	private float upPosition;

	[SerializeField] private RectTransform rectTransform = null;

	private void Start()
	{
		upPosition = rectTransform.position.y;
	}

	public void MoveOn(bool down) //downでウインドウが降りてくる、upでウインドウが下がってくる
	{
		if (down)
		{
			rectTransform.DOMoveY(downPosition,speed).SetUpdate(true).SetEase(Ease.Linear);
		}
		else
		{
			rectTransform.DOMoveY(upPosition, speed).SetUpdate(true).SetEase(Ease.Linear);
		}
	}


}
