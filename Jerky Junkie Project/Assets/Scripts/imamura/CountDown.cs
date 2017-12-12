using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    public bool enableFade = false;
    public bool enableFadeIn = false;
    public bool enableFadeOut = false;
    public bool enableFadeOn = false;

    public float speed = 2f;

    public Text countDownStart;
    private int countDownTextNumber;

    private float count = 1f;
    
    void Start ()
    {
        enableFade = true;
        enableFadeIn = true;
        setAlpha(countDownStart, count);
    }
    void Update()
    {
        if (enableFadeIn)
        {
            FadeIn(countDownStart);
        }

        if (enableFadeOut)
        {
            FadeOut(countDownStart);
        }
    }

    void setAlpha(Text text, float alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
    public void FadeOut(Text text)
    {
        if (enableFade)
        {
            count += speed;
            setAlpha(text, count);
            if (text.color.a >= 1.0f)
            {
                enableFade = false;
                if (enableFadeOut)
                {
                    //SceneManager.LoadScene (1);
                    //フェードアウトした時の処理をここに書く
                }
            }
        }
    }

    void FadeIn(Text text)
    {
        if (enableFade)
        {
            count -= speed;
            setAlpha(text, count);
            if (text.color.a <= 0f)
            {
                count = 1;
                countDownStart.text = ""+ countDownTextNumber;//intをstringへ変換できないマンですごめんなさい
                //enableFade = false;
                //enableFadeIn = false;
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
