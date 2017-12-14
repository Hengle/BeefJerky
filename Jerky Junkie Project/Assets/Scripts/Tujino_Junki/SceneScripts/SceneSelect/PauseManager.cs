using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : SingletonMonoBehaviour<PauseManager>//シングルトンを継承
{
    private bool isPause = false;//ポーズ画面を起動しているかの情報を持つ

    public void PauseOn()//ポーズ画面起動時に呼び出す
    {
        isPause = true;
    }

    public void PauseOff()//ポーズ画面を閉じる際に呼び出す
    {
        isPause = false;
    }
}
