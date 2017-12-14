using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleProductionTiming : MonoBehaviour {
    
       //BGMは一つでループ変更することはない。ので、Startはけした
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

    public void CowCrySE()
    {
        SoundManager.Instance.PlaySE("CowCry");//牛の鳴き声
    }
    //

    //シーン「メイン」の中でのみ使用

    public void GayaSE()//居酒屋のガヤ
    {
        SoundManager.Instance.PlaySE("OSSANGaya");
    }

    public void CountDownSE()            //カウントダウン。スタート前の３２１と終わる間近の５４３２１    現状。ピッの単音を入れる予定。まだ音を手に入れてない。
    {
        SoundManager.Instance.PlaySE("CountDown");
    }

    public void STARTSE()            //上のCountDownSEの後のスタートのタイミングで。
    {
        SoundManager.Instance.PlaySE("START");                //音を１２３とGO!で変える時用
    }

    public void ENDSE()            //上のCountDownSEの後の終わるタイミングで。
    {
        SoundManager.Instance.PlaySE("END");                //音を１２３とENDで変える時用
    }


    public void DropExchangeSE()            //隣り合ったドロップを交換するタイミングで
    {
        SoundManager.Instance.PlaySE("DropExchange");
    }

    public void BeefjerkyCreateSE(int x,int y)            //ジャーキーに変わったタイミングで        エフェクト出現させたい位置のX,Y軸の入力をお願いします　もやもや
    {
        SoundManager.Instance.PlaySE("BeefjerkyCreate");
        EffectManager.Instance.PlayEffect("Beefjerky", new Vector2(x,y), 2.0f);
    }

    //OSSANEat（音）はなくして、昇天時のSEにそのためサウンドマネージャーのElement7は必要なし。
    public void OSSANEatSE(int x, int y)            //おっさんが食べるときのSE。        エフェクト出現させたい位置のX,Y軸の入力をお願いします　　キラキラ
    {
        SoundManager.Instance.PlaySE("OSSANAscension");
        EffectManager.Instance.PlayEffect("DoropDestroy", new Vector2(x, y), 2.0f);
        //焦点おじさんを出す（今後
    }
    //カットインは別の人
    //ビールが巻き込まれたときは（SE無）エフェクト（肉と同じ？）時間、と相談



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) TapBottanSE();
        if (Input.GetKeyDown(KeyCode.W)) GameStartSE();
        if (Input.GetKeyDown(KeyCode.E)) CountDownSE();
        if (Input.GetKeyDown(KeyCode.R)) DropExchangeSE();
        if (Input.GetKeyDown(KeyCode.T)) BeefjerkyCreateSE(0, 0);
        if (Input.GetKeyDown(KeyCode.Y)) OSSANEatSE(0, 0);
    }
}
