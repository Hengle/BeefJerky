using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//空のゲームオブジェクトに割り当てて利用

public class CountDown : MonoBehaviour {
     bool enableFade = false;
     bool enableFadeIn = false;

    bool Mozi;

    public float speed = 1f;

    public Text countDownStart;
    private float countDownTextNumber;      //最初は３の予定

    private float count = 1f;
    Text text;

    [SerializeField] private TitleProductionTiming titleScript = null;
    

	public void CountDownStart (bool SorE,float time)                   //こいつを起動させれば起動      trueならスタート前。　falseなら終わり前.1の桁が前回と変わるタイミングで呼ぶ
    {
        if ((time <= 4 && time >= 0))
        {
            countDownTextNumber = time - (time % 1);
            count = time % 1;
            if (count == 0)
            {
                count = 1;
            }
            countDownStart.enabled = true;
            setAlpha(countDownStart, count);

            enableFade = true;
            enableFadeIn = true;
            Mozi = SorE;
            if (countDownTextNumber == 0)
            {
                if (Mozi)
                {
                    countDownStart.text = "GO!";
                    titleScript.STARTSE();
                }
                else
                {
                    countDownStart.text = "END";
                    titleScript.ENDSE();
                }
            }
            else
            {
                countDownStart.text = "" + countDownTextNumber;        //intをstringへ変換できないマンですごめんなさい
                titleScript.CountDownSE();

            }
        }
    }
    void setAlpha(Text text, float alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    void Update()
    {
        
        if (enableFadeIn)
            {
                FadeIn(countDownStart);
            }
    }

    void FadeIn(Text text)
    {
        if (enableFade)
        {
            count -= speed*Time.deltaTime;
            setAlpha(text, count);
            if (text.color.a <= 0f)                 //３２１までは毎回呼び出されるので必要ないのだが、ENDの時はあっち側の時間がが－1のタイミングで、呼び出す処理を入れるのかが不明なので念のため。
            {
                if (countDownTextNumber == 0)
                {
                    enableFade = false;
                    enableFadeIn = false;
                    countDownStart.enabled = false;
                }
                //else
                //{
                //    count = 1;
                //    countDownTextNumber--;
                //}
            }
        }
    }
}
