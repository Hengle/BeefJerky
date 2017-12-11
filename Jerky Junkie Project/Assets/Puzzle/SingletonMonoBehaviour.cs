using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

    private static T instance;
    public static T Instance {
        get {
            if (instance == null) {
                Type t = typeof(T);
                instance = (T)FindObjectOfType(t);
                if (instance == null) {
                    Debug.LogError(t + " をアタッチしているGameObjectが存在しません");
                }

            }
            return instance;
        }
    }

    virtual protected void Awake() {
        if (this != Instance) {
            Destroy(this);

            Debug.LogError("既に" + typeof(T) + "が存在したため、" + gameObject + "の" + typeof(T) + "削除されました");
        }
    }
}
