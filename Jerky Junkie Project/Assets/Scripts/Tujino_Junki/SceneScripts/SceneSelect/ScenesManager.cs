using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : SingletonMonoBehaviour<ScenesManager>
{
    //このスクリプトをシーン上に設置してシーン遷移制御する

    public void Scenes(string SceneName)//引数でシーン名を割り当てる
       {
           SceneManager.LoadScene(SceneName);//シーン遷移
       }
    
}
