﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour{

	protected static T instance;

	public static T Instance
	{
		get
		{
			if(instance == null)
			{
				instance = (T)FindObjectOfType(typeof(T));

				if(instance == null)
				{
					Debug.LogWarning(typeof(T) + "is nothing");
				}
			}
			return instance;
		}
	}
}