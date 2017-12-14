using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowAnimation : MonoBehaviour {

	private Image image;
	private RectTransform rectTransform;

	public void AnimationOn()
	{
		image = GetComponent<Image>();
		rectTransform = GetComponent<RectTransform>();


	}

	public void AnimationOff()
	{

	}

	private void ImageChange()
	{

	} 
}
