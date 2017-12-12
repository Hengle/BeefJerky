using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMove : MonoBehaviour {

	[SerializeField] private float upPosition;
	[SerializeField] private float downPosition;

	public void MoveOn(bool down) //downでウインドウが降りてくる、upでウインドウが下がってくる
	{
		if (down)
		{
			transform.position = Vector2.MoveTowards(transform.position,new Vector2(transform.position.x,downPosi));
		}
	}


}
