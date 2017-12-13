using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour {

	[SerializeField] private float startTime = 0.0f;
	[SerializeField] private float animSpeed = 1.0f;
	[SerializeField] private float intervalTime = 5.0f;

	private RectTransform rectTransform;
	[SerializeField] float countTime = 0.0f;
	[SerializeField] float magnification = 1.2f; //倍率

	private float startscale;

	private Vector3 objScale;

	private int stats = 0;

	void Start () {
		rectTransform = GetComponent<RectTransform>();
		startscale = rectTransform.localScale.x;
		Debug.Log(startscale * magnification);
	}

	private void Update()
	{
		Debug.Log(rectTransform.localScale.x);
		countTime += Time.deltaTime;
		if(startTime == 0)
		{
			if (countTime > intervalTime)
			{
				PlayAnimation();
				countTime = 0.0f;
			}
		}
		else
		{
			if (countTime > startTime)
			{
				PlayAnimation();
				countTime = 0.0f;
				startTime = 0.0f;
			}
		}

		if(stats!= 0)
		{
			rectTransform.localScale *= (1 + (stats * animSpeed));
			objScale = rectTransform.localScale;
			objScale.x = Mathf.Clamp(objScale.x,startscale,startscale * magnification);
			objScale.y = Mathf.Clamp(objScale.y, startscale, startscale * magnification);
			objScale.z = Mathf.Clamp(objScale.z, startscale, startscale * magnification);
			if (rectTransform.localScale.x >= startscale * magnification)
			{
				stats = -1;
			}
			if (rectTransform.localScale.x <= startscale)
			{
				rectTransform.localScale = new Vector3(startscale, startscale, startscale);
				stats = 0;
			}
		}
	}

	private void PlayAnimation()
	{
		stats = 1;
	}


}
