using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeffeLineFace : MonoBehaviour
{
    bool startAnimation = false;
    
    public float endTime = 0.2f;

    private Vector3 beefEulerAnglesL = new Vector3(0, 0, 10);
    private Vector3 beefEulerAnglesR = new Vector3(0, 0,-10);
    private RectTransform rTransform;


    public Sprite naki;
    public Sprite warai;

    private bool beffeStart = false;
	
	TimerController timerController;

	private void Start()
    {
        rTransform = GetComponent<RectTransform>();
		timerController = GameObject.Find("TimerController 1").GetComponent<TimerController>();

	}
    float time = 0;
    private void Update()
    {
        if (beffeStart)
        {
            time += Time.deltaTime;
            if ((time-(time%1)) % 2 == 0)
            {
                rTransform.eulerAngles = beefEulerAnglesL;
            }
            else
            {
                rTransform.eulerAngles = beefEulerAnglesR;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            BeffeLineStart();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            BeffeLineEnd();
        Debug.Log("a");
        }
    }


    public void BeffeLineStart()                //こいつをすれば起動
    {
        beffeStart = true;
        this.GetComponent<Image>().sprite = naki;
        time = timerController.TimerCnt;
    }
    public void BeffeLineEnd()//こいつをすれば終わり。
    {
        beffeStart = false;
        rTransform.eulerAngles = new Vector3(0, 0, 0);
        this.GetComponent<Image>().sprite = warai;
    }
}