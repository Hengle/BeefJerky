using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    //このスクリプトをボタンに設定してシーン遷移する

    public void Click(string SceneName)
    {
        if (SceneName == "Title")
        {
            FadeManager.Instance.LoadLevel(SceneName);//フェードをつける
        }
        else if(SceneManager.GetActiveScene().name == "Option" || SceneName == "Option")//現在のシーンがオプションの場合、もしくはオプションへシーン遷移する場合
        {
            ScenesManager.Instance.Scenes(SceneName);//何もせずにシーン遷移
        }
        else//それ以外の場合
        {
            FadeManager.Instance.LoadLevel(SceneName);//フェードをつける
        }
        
    }
}
