using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour {

    public float TimerCnt = 60.0f;

    public int Cnt = 0;

    public int Test;

    [SerializeField] UIManager uiManager = null;

    [SerializeField] CountDown countDown = null;

    [SerializeField] Result result = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer();
        uiManager.TimeUpdate(TimerCnt);

    }

    private void Timer()
    {
        TimerCnt -= Time.deltaTime;

        Cnt = (int)TimerCnt;

        //Test = (Cnt / 10);

        //Debug.Log("Test"+Test);

        if (TimerCnt < -2.0f)//タイマーが０より下回ったら
        {
            TimerCnt = -2.0f;//０のままに

            result.OnResult();//リザルトの起動

        }
    }
    
    
}
