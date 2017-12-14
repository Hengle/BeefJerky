// 作成者    :菅原 
// 機能      :メインシーンのUI処理全般
// 作成日    :2017/12/12 
// 最終更新日:2017/12/13 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] private Slider timeSlider = null; //残り時間ゲージ

	[SerializeField] private bool pause = false; //ポーズ中かどうか
	[SerializeField] private Image pauseButtonImage = null; //ポーズボタンの現在の画像
	[SerializeField] private Sprite[] pauseButtonSprite = null; //ポーズボタンの画像素材。0が停止、1が再生画像

	[SerializeField] private AudioSource[] audioSource = null; //BGM、SEそれぞれのAudioSource。オプションで音量を変更する際に使用
	[SerializeField] private Slider[] audioSlider = null;

	[SerializeField] private FadeManager fadeManager = null; //フェードを行うスクリプト
	[SerializeField] private WindowMove windowMove = null; //ポーズウインドウのアニメーションを行うスクリプト
	[SerializeField] private AnimationScore animationScore = null; //スコアテキストのアニメーションを行うスクリプト
	[SerializeField] private CutInAnimation cutInAnimation = null;

	[SerializeField] private Text highScoreText = null;

	[SerializeField] private Transform cutInParent = null;
	[SerializeField] private GameObject cutInPrefab = null;
	[SerializeField] private Vector3 cutInPos = Vector3.zero;

	public int saveScore = 0; //直前のスコアを保存する

	private float maxT = 60.0f; //制限時間

	private void Awake()
	{
		fadeManager = transform.Find("SceneFade").GetComponent<FadeManager>(); //FadeManagerスクリプトをキャッシュ
		GameObject soundObj = GameObject.Find("SoundManager"); //SoundManagerオブジェクトのキャッシュ
		audioSource[0] = soundObj.transform.Find("BGMManager").GetComponent<AudioSource>(); //BGMのAudioSourceのキャッシュ
		audioSource[1] = soundObj.transform.Find("SEManager").GetComponent<AudioSource>(); //SEのAudioSourceのキャッシュ
	}
	 /// <summary>
	 /// ゲーム残り時間のUIを更新する関数。
	 /// 現在の残り時間をtimeに渡してください。
	 /// </summary>
	 /// <param name="time"></param>
	public void TimeUpdate(float time)
	{
		timeSlider.value = time/maxT; //（現在時間/最大の時間）をTimeゲージの値に入れる
	}

	/// <summary>
	/// スコアのUIテキストを更新する関数。
	/// 現在のスコアをscoreに渡してください。
	/// </summary>
	/// <param name="score"></param>
	public void ScoreUpdate(int score)
	{
		animationScore.AnimationPlay(saveScore,score); //直前のスコア、現在のスコアを渡す
		saveScore = score; //現在のスコアを保存
	}

	public void HighScoreUpdate()
	{
		highScoreText.color = new Color(255,255,0);
		//音を鳴らす
	}

	public void PlayCutIn()
	{
		GameObject obj = Instantiate(cutInPrefab,cutInPos,transform.rotation) as GameObject;
		obj.transform.parent = cutInParent;
	}

	/// <summary>
	/// ポーズを行う関数。
	/// ポーズボタンが押されたときのみ呼ばれます。
	/// </summary>
	public void Pause()
	{
		pause = !pause; //ポーズ状態を反転
		windowMove.MoveOn(pause); //現在のポーズ状態を渡すと、それに対応したアニメーションをWindowMoveスクリプトで実行
		if (pause) //ポーズ状態になった時
		{
			pauseButtonImage.sprite = pauseButtonSprite[1]; //ポーズボタンを停止に変える
			Time.timeScale = 0.0f; //時間を止める
		}
		else
		{
			pauseButtonImage.sprite = pauseButtonSprite[0]; //ポーズボタンを再生に変える
			Time.timeScale = 1.0f; //時間を動かす
		}
	}
	/// <summary>
	/// UIで変更した音量をAudioSourceに適用させる関数。
	/// </summary>
	/// <param name="audioNum"></param>
	public void OnValueChange(int audioNum) 
	{
		audioSource[audioNum].volume = audioSlider[audioNum].value; //AudioSourceの値にスライダーの値を代入
	}

	/// <summary>
	/// 「終わる」ボタンを押したときのみ呼ばれる関数。
	/// 時間を動かし、タイトルシーンへ戻ります。
	/// </summary>
	public void ExitButton()
	{
		Time.timeScale = 1.0f; //時間を動かす
		fadeManager.LoadLevel("Title"); //タイトルシーンへ遷移
	}

}
