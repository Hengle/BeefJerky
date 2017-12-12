using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	[SerializeField] private ScenesManager sceneManager = null;

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			sceneManager.Scenes("Sugawara_Main");
		}
	}
}
