using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleProductionTiming : MonoBehaviour {

    void Start()             //開始直後から流れるBGM
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            SoundManager.Instance.PlayBGM("TitleBGM");
        }
        if (SceneManager.GetActiveScene().name == "Main")
        {
            SoundManager.Instance.PlayBGM("GameBGM"); 
        }
    }

 //シーン区別なくどちらでも使用
    public void TapBottanSE()            //ボタンをタッチしたときの音。（オプション統一）
    {
        SoundManager.Instance.PlaySE("Bottan");
    }
    //

    //シーン「タイトル」の中でのみ使用
    public void GameStartSE()               //ボタンの中のゲームがスタートするときの音。
    {
        SoundManager.Instance.PlaySE("GameStart");
    }
    //

    //シーン「メイン」の中でのみ使用
    public void CountDownSE()            //カウントダウン。スタート前の３２１と終わる間近の５４３２１    現状。ピッピッピッ、ピーって音が入ってる。そのため、下二つは使わない。
    {
        SoundManager.Instance.PlaySE("CountDown");
    }

    //public void STARTSE()            //上のCountDownSEの後のスタートのタイミングで。
    //{
    //    SoundManager.Instance.PlaySE("START");                //音を１２３とGO!で変えると時用
    //}

    //public void ENDSE()            //上のCountDownSEの後の終わるタイミングで。
    //{
    //    SoundManager.Instance.PlaySE("END");
    //}
    

    public void DropExchangeSE()            //隣り合ったドロップを交換するタイミングで
    {
        SoundManager.Instance.PlaySE("DropExchange");
    }

    public void BeefjerkyCreateSE(int x,int y)            //ジャーキーに変わったタイミングで        エフェクト出現させたい位置のX,Y軸の入力をお願いします
    {
        SoundManager.Instance.PlaySE("BeefjerkyCreate");
        EffectManager.Instance.PlayEffect("Beefjerky", new Vector2(x,y), 2.0f);
    }

    public void OSSANEatSE(int x, int y)            //おっさんが食べるときのSE。        エフェクト出現させたい位置のX,Y軸の入力をお願いします
    {
        SoundManager.Instance.PlaySE("OSSANEat");
        EffectManager.Instance.PlayEffect("Beefjerky", new Vector2(x, y), 2.0f);
    }

    public void DoropDestroy(int x, int y)              //ドロップを消したときの処理。モワモワ～           音大丈夫？
    {
        SoundManager.Instance.PlaySE("DoropDestroy");
        EffectManager.Instance.PlayEffect("DoropDestroy", new Vector2(x, y), 2.0f);
    }

    public void OSSANAscensionSE()                  //おっさん昇天時。
    {
        SoundManager.Instance.PlaySE("OSSANAscension");
    }

    public void BeefjerkyCreateSE()            //ジャーキ―に変わったタイミングで
    {
        SoundManager.Instance.PlaySE("BeefjerkyCreate");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) TapBottanSE();
        if (Input.GetKeyDown(KeyCode.W)) GameStartSE();
        if (Input.GetKeyDown(KeyCode.E)) CountDownSE();
        if (Input.GetKeyDown(KeyCode.R)) DropExchangeSE();
        if (Input.GetKeyDown(KeyCode.T)) BeefjerkyCreateSE(0, 0);
        if (Input.GetKeyDown(KeyCode.Y)) OSSANEatSE(0, 0);
        if (Input.GetKeyDown(KeyCode.U)) DoropDestroy(0, 0);
        if (Input.GetKeyDown(KeyCode.I)) OSSANAscensionSE();
        if (Input.GetKeyDown(KeyCode.O)) BeefjerkyCreateSE();
    }
}
