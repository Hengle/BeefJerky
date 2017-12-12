using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : SingletonMonoBehaviour<InstantiateManager> {
    class desetObj {
        List<GameObject> objs;
        GameObject prefab;
        public GameObject Instantiate() {
            if (objs.Count > 0) {
                GameObject obj = objs[0];
                objs.Remove(obj);
                return obj;
            }
            return GameObject.Instantiate(prefab);
        }

        public void Destroy(GameObject obj) {
            objs.Add(obj);

        }
    }

    List<desetObj> destObjList = new List<desetObj>();

}
