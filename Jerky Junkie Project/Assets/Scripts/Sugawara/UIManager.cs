using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] private Slider timeSlider = null;
	[SerializeField] private GameObject pauseWindow = null;
	[SerializeField] private bool pause = false;
	[SerializeField] private Button pauseButton = null;
	private Image pauseButtonImage = null;

	[SerializeField] private Sprite[] pauseButtonSprite = null;

	private float maxT = 100f;
	private float t;

	private void Start()
	{
		t = maxT;
		pauseWindow.SetActive(false);
		pauseButtonImage = pauseButton.GetComponent<Image>();
	}

	void Update () {
		t -= Time.deltaTime * 5.0f;
		TimeUpdate(t);
	}

	public void TimeUpdate(float time)
	{
		timeSlider.value = t/maxT;
	}

	public void Pause()
	{
		pause = !pause;
		pauseWindow.SetActive(pause);
		if (pause)
		{
			pauseButtonImage.sprite = pauseButtonSprite[1];
			Time.timeScale = 0.0f;
		}
		else
		{
			pauseButtonImage.sprite = pauseButtonSprite[0];
			Time.timeScale = 1.0f;
		}
	}

	public void ExitButton()
	{
		//タイトルシーンへ遷移
	}
}
