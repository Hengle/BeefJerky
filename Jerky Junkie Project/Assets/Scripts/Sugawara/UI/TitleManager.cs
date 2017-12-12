using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	public FadeManager fadeManager = null;

	private void Awake()
	{
		fadeManager = transform.Find("SceneFade").GetComponent<FadeManager>();
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			fadeManager.LoadLevel("Sugawara_Main");
		}
	}
}
