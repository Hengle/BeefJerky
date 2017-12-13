using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIManager))]
public class CutInEditor : Editor {

	public override void OnInspectorGUI()
	{
		var uIManager = target as UIManager;

		DrawDefaultInspector();

		if (GUILayout.Button("Button"))
		{
			uIManager.PlayCutIn();
		}

	}
}
