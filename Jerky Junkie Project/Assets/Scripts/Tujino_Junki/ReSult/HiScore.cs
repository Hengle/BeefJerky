using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScore : MonoBehaviour {

    public Text ResultScoreText;//リザルトのスコア表示

    public Text ResultHighScoreText;//リザルトのハイスコア表示

	public UIManager uIManager;

    private int score;// スコア

	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
			uIManager.ScoreUpdate(score);
		}
	}

    private int highScore = 0;// ハイスコア

    private string highScoreKey = "highScore";// PlayerPrefsで保存するためのキー

    void Start()
    {
        Initialize();//最初に初期化
    }

    void Update()
    {
        // HighScore();
        TextDraw();
    }
        
    public void Initialize()// ゲーム開始前の状態に戻す
    {       
        score = 0; // スコアを0に戻す
        
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);// ハイスコアを取得する。保存されてなければ0を取得する。
    }
       
    public void HighScore()//ハイスコア処理
    {
        if (highScore < score)// スコアがハイスコアより大きければ
        {
            highScore = score;//更新
        }

        ResultScoreText.text = score.ToString();//リザルトのスコア表示
        ResultHighScoreText.text = highScore.ToString();//リザルトのハイスコア表示
    }


    private void TextDraw()
    {
    //    scoreText.text = score.ToString();// スコア・ハイスコアを表示する
    //    highScoreText.text = highScore.ToString();
    }

    public void AddPoint(int point) //引数で得たポイントを追加
    {
        Score = score + point;
    }

    public void Save() // ハイスコアの保存 リザルトを抜ける時に呼び出す
    {       
        PlayerPrefs.SetInt(highScoreKey, highScore); // ハイスコアを保存する
        PlayerPrefs.Save();
        
        //Initialize();// ゲーム開始前の状態に戻す
    }
}