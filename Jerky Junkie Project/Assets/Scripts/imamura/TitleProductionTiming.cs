using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleProductionTiming : MonoBehaviour {
<<<<<<< HEAD
    void Start()     //開始直後から流れるBGM
    {
        Debug.Log("シーンスタート");
=======
    void Awake()             //開始直後から流れるBGM
    {
>>>>>>> akinobu
        if (SceneManager.GetActiveScene().name == "Title")
        {
            SoundManager.Instance.PlayBGM("TitleBGM");
        }
        if (SceneManager.GetActiveScene().name == "Main")
        {
            SoundManager.Instance.PlayBGM("GameBGM"); 
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> akinobu
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
<<<<<<< HEAD
    public void CountDownSE()            //カウントダウン。スタート前の３２１と終わる間近の５４３２１    現状。ピッピッピッ、ピーって音が入ってる。そのため、下二つは使わない。
    {
        SoundManager.Instance.PlaySE("CountDown");
    }

    public void STARTSE()            //上のCountDownSEの後のスタートのタイミングで。
    {
        SoundManager.Instance.PlaySE("START");
    }

    public void ENDSE()            //上のCountDownSEの後の終わるタイミングで。
    {
        SoundManager.Instance.PlaySE("END");
    }


=======
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
>>>>>>> akinobu
    public void DropExchangeSE()            //隣り合ったドロップを交換するタイミングで
    {
        SoundManager.Instance.PlaySE("DropExchange");
    }
<<<<<<< HEAD

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

    public void OSSANAscensionSE()                  //おっさん昇天時。
    {
        SoundManager.Instance.PlaySE("OSSANAscension");
    }
    //


    void Update()           //テスト用
    {

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    BeefjerkyCreateSE(0, 0);
        //    SceneManager.LoadScene("Title");
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    DropExchangeSE();
        //}
    }

=======
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
    
>>>>>>> akinobu
}
