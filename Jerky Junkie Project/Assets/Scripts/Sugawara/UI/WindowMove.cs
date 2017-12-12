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
<<<<<<< HEAD
			rectTransform.DOMoveY(downPosition,speed).SetUpdate(true);
		}
		else
		{
			rectTransform.DOMoveY(upPosition, speed).SetUpdate(true);
=======
			//transform.position = Vector2.MoveTowards(transform.position,new Vector2(transform.position.x,downPosi));
>>>>>>> 8bcd0eb4770d77d287da3a5bd3e88eaf10e038da
		}
	}


}
