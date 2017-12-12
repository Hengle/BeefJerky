using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScoreTest))]
public class ScoreEditor : Editor {

	public override void OnInspectorGUI()
	{
		var scoreTest = target as ScoreTest;

		DrawDefaultInspector();
		if (GUILayout.Button("Button"))
		{
			scoreTest.Push();
		}
	}
}
