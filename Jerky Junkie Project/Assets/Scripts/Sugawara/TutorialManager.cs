using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	[SerializeField] private GameObject[] panels;
	[SerializeField] private GameObject[] buttons;

	[SerializeField] private float buttonSpeed = 0.5f; //パネル表示からボタンが出てくるまでの時間

	[SerializeField] private TimerController timerController = null;

    public TitleProductionTiming titleProductionTiming = null;//SEを管理しているスクリプトを取得

    private int phase;

    private bool isTest = false;

	void Start () {


        // titleProductionTiming.GayaSE(0);
        SoundManager.Instance.PlayBGM("GayaBGM");//ガヤの音を流す
        Debug.Log("ガヤ開始");
        phase = 0;
		panels[0].SetActive(true);
		panels[1].SetActive(false);

		Invoke("ButtonMake",buttonSpeed);

        
	}

	private void ButtonMake()
	{

        buttons[phase].SetActive(true);
    }

	public void ButtonPush(int num)
	{
		phase++;
        if (phase == 1)
		{
            
            panels[0].SetActive(false);
			panels[1].SetActive(true);
			Invoke("ButtonMake", buttonSpeed);
		}
		else if(phase == 2)
		{
            SoundManager.Instance.StopBGM("GayaBGM");
            Debug.Log("ガヤ停止");
            timerController.GameStart();
			Destroy(this.gameObject);
		}
	}

}
