using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInTest : MonoBehaviour {

	[SerializeField] CutInAnimation cutInAnimation = null;

	public void Play()
	{
		cutInAnimation.PlayAnimation();
	}
}
