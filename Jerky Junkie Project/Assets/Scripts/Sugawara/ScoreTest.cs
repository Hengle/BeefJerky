using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTest : MonoBehaviour {

	[SerializeField] private UIManager ui = null;

	public void Push()
	{
		ui.ScoreUpdate(ui.saveScore+100);
	}
}
