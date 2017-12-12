using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleProductionTiming : MonoBehaviour {
    void Awake()             //開始直後から流れるBGM
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
    public void CountDownSE()            //カウントダウン。スタート前の３２１と終わる間近の５４３２１
    {
        SoundManager.Instance.PlaySE("CountDown");
    }
    public void STARTSE()            //スタートのタイミングで。上のCountDownSEの後に
    {
        SoundManager.Instance.PlaySE("START");
    }
    public void ENDSE()            //終わるタイミングで。上のCountDownSEの後に
    {
        SoundManager.Instance.PlaySE("END");
    }
    public void DropExchangeSE()            //隣り合ったドロップを交換するタイミングで
    {
        SoundManager.Instance.PlaySE("DropExchange");
    }
    public void BeefjerkyCreateSE()            //ジャーぎ―に変わったタイミングで
    {
        SoundManager.Instance.PlaySE("BeefjerkyCreate");
    }
    public void OSSANEatSE()            //おっさんが食べるときのSE。
    {
        SoundManager.Instance.PlaySE("OSSANEat");
        EffectManager.Instance.PlayEffect("Beefjerky", new Vector2(1, 1), 1.0f); //キー"effectA"に対応したEffectを表示する、Vector2型、指定した時間で消滅

    }
    //

   
    void Update()           //テスト用
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OSSANEatSE();
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DropExchangeSE();
        }
    }
    
}
