using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] private Slider timeSlider = null;
	[SerializeField] private GameObject pauseWindow = null;
	[SerializeField] private bool pause = false;
	[SerializeField] private Image pauseButtonImage = null;
	[SerializeField] private Sprite[] pauseButtonSprite = null;

	[SerializeField] private AudioSource[] audioSource = null;
	private bool[] audioMute = { false,false}; //trueのときミュート
	public float[] saveValue = { 0.0f, 0.0f };
	[SerializeField] private Image[] musicButtonImage = null;
	[SerializeField] private Sprite[] musicButtonSprite = null;

	[SerializeField] private Slider[] audioSlider = null;

	[SerializeField] private Text scoreText = null;

	[SerializeField] private ScenesManager sceneManager = null;

	[SerializeField] private WindowMove windowMove = null;

	private float maxT = 100f;
	private float t;

	private int score = 0;

	private void Start()
	{
		t = maxT;
		//pauseWindow.SetActive(false);
		GameObject soundObj = GameObject.Find("SoundManager");
		audioSource[0] = soundObj.transform.Find("BGMManager").GetComponent<AudioSource>();
		audioSource[1] = soundObj.transform.Find("SEManager").GetComponent<AudioSource>();
	}

	void Update () {
		t -= Time.deltaTime * 5.0f;
		TimeUpdate(t);

		score += (int)(Time.deltaTime * 100);
		ScoreUpdate(score);
	}

	public void TimeUpdate(float time)
	{
		timeSlider.value = t/maxT;
	}

	public void ScoreUpdate(int score)
	{
		scoreText.text = "Score:" + score.ToString("D6");
	}

	public void Pause()
	{
		pause = !pause;
	//	pauseWindow.SetActive(pause);
		windowMove.MoveOn(pause);
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

	public void MusicButton(int audioNum)
	{
		audioMute[audioNum] = !audioMute[audioNum];

		if (audioMute[audioNum])
		{
			saveValue[audioNum] = audioSource[audioNum].volume;
			musicButtonImage[audioNum].sprite = musicButtonSprite[1];
			audioSource[audioNum].volume = 0.0f;
		}
		else 
		{
			musicButtonImage[audioNum].sprite = musicButtonSprite[0];
			audioSource[audioNum].volume = saveValue[audioNum];
		}
	}

	public void OnValueChange(int audioNum)
	{
		if (audioMute[audioNum])
		{
			saveValue[audioNum] = audioSlider[audioNum].value;
		}
		else
		{
			audioSource[audioNum].volume = audioSlider[audioNum].value;
		}
		if(audioNum == 1)
		{
			SoundManager.Instance.PlaySE("Bottan");
		}
	}

	public void ExitButton()
	{
		Time.timeScale = 1.0f;
		sceneManager.Scenes("Title");
	}

}
