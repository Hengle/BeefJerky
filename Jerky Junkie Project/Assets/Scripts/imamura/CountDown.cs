using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
    public bool enableFade = false;
    public bool enableFadeIn = false;
    public bool enableFadeOut = false;
    public bool enableFadeOn = false;

    public float speed = 1f;

    public Text countDownStart;
    private int countDownTextNumber=3;

    private float count = 1f;
    Text text;

    [SerializeField] private TitleProductionTiming titleScript = null;

	private void Start()
	{
		//CountDownStart();
	}

	public void CountDownStart ()                   //こいつを起動させれば起動
    {
        countDownTextNumber = 3;
        count = 1f;
        enableFade = true;
        enableFadeIn = true;
        setAlpha(countDownStart, count);
        countDownStart.enabled = true;
        titleScript.CountDownSE();
    }
    void Update()
    {
        if (countDownTextNumber == 0)
        {
            countDownStart.text = "GO!";
        }
        else
        {
            countDownStart.text = "" + countDownTextNumber;        //intをstringへ変換できないマンですごめんなさい
            
        }
        if (enableFadeIn)
            {
                FadeIn(countDownStart);
            }
    }

    void setAlpha(Text text, float alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    void FadeIn(Text text)
    {
        if (enableFade)
        {
            count -= speed*Time.deltaTime;
            setAlpha(text, count);
            if (text.color.a <= 0f)
            {
                if (countDownTextNumber == 0)
                {
                    enableFade = false;
                    enableFadeIn = false;
                    countDownStart.enabled = false;

                }
                else
                {
                    count = 1;
                    countDownTextNumber--;
                }
            }
        }
    }
    public void InOut(int i)
    {
        enableFade = true;
        enableFadeOut = true;
        enableFadeIn = false;
    }
}
