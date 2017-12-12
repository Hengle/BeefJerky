using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    //このスクリプトをボタンに設定してシーン遷移する

    public void Click(string SceneName)
    {
        ScenesManager.Instance.Scenes(SceneName);//シーン名を引数で渡す
    }
}
