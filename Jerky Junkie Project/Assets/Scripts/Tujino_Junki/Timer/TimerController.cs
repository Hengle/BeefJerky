using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//空のゲームオブジェクトに割り当てて利用
public class TimerController : SingletonMonoBehaviour<TimerController> {

    public float TimerCnt = 60.0f;

    public int Cnt = 0;

    public int Test;

    private int StartWaitTime = 3;//ゲーム開始前のカウント数値。

    private int BeforeCnt = 0;//前のフレームのカウントの値を代入

    [SerializeField] UIManager uiManager = null;

    [SerializeField] CountDown countDown = null;

    [SerializeField] Result result = null;

    private int countStart = 0;
	// Use this for initialization
	void Start () {
        //countDown.CountDownStart(true, StartWaitTime);
        StartCoroutine(TimerCount());//一秒を計算する
	}

    IEnumerator TimerCount()
    {
        while(countStart<4)
        {
            countDown.CountDownStart(true, StartWaitTime);
            countStart++;
            StartWaitTime--;
            Debug.Log("１秒経過");
            yield return new WaitForSeconds(1.0f);
        }
    }

	// Update is called once per frame
	void Update () {

        StartCoroutine(StartCount());//三秒間タイマーを停止

        //if(Input.GetKeyDown(KeyCode.Space))//とりあえずスペースキーを押したら
        //{
        //    Debug.Log("スペース押した");
        //    TimerCnt += 1.2f;//タイマーを延長
        //}
    }

    IEnumerator StartCount()
    {
        yield return new WaitForSeconds(3.0f);//処理を3秒待機

        Timer();
        uiManager.TimeUpdate(TimerCnt);
    }


    private void Timer()
    {


        TimerCnt -= Time.deltaTime;//タイマーを進める

        Cnt = (int)TimerCnt+1;//タイマーの数値の小数点切り捨て
        //↑は、3.0,2.9999,2.9,2.8,2.7,,,は３と表示するため、0.1,-0.1の差が生まれなかったためそれを防ぐため、後の作業を楽にするための＋１byカウントダウン担当今村

        if (BeforeCnt != Cnt)//前フレームと異なっていたら
        {
            countDown.CountDownStart(false, TimerCnt);//カウントダウンを呼び出し
        }

        BeforeCnt = Cnt;//前の数値を格納
        //Test = (Cnt / 10);

        //Debug.Log("Test"+Test);

        if (TimerCnt < -2.0f)//タイマーが０より下回ったら
        {
            TimerCnt = -2.0f;//０のままに

            result.OnResult();//リザルトの起動

        }
        

    }

    public void AddTime(float value) {
        TimerCnt += value;
    }
}
