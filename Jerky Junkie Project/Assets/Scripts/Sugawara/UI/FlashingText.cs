using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class FlashingText : MonoBehaviour
{
	[SerializeField] private Text flashingText = null;
	[SerializeField] private float value;
	[SerializeField] private float stats;

	private void Start()
	{
		value = 0.0f;
		stats = 1.0f;
	}
	private void Update()
	{
		value += Time.deltaTime * stats;
		if(value > 1.0f)
		{
			value = 1.0f;
			stats *= -1.0f;
		}
		if (value < 0.0f)
		{
			value = 0.0f;
			stats *= -1.0f;
		}
		flashingText.color = new Color(flashingText.color.r,flashingText.color.g,flashingText.color.g,value);
	}
}