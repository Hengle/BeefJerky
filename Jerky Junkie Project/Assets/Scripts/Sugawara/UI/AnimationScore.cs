// 作成者    :菅原 
// 機能      :スコアのテキストUIの演出 
// 作成日    :2017/12/12 
// 最終更新日:2017/12/13 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationScore: MonoBehaviour
{
	[SerializeField] private RectTransform scoreTextTransform = null;
	[SerializeField] private Text scoreText;
	[SerializeField] private float speed = 0.0f;

	private int animStats = 0; 

	private void Update()
	{
		if(animStats != 0)
		{
			TextAnimation();
		}
	}

	public void AnimationPlay(int beforeScore,int afterScore)
	{
		scoreText.text = afterScore.ToString("D3");

		//StartCoroutine(ScoreAnimation(beforeScore, afterScore, speed));
		animStats = 1;
	}

	// スコアをアニメーションさせる
	private IEnumerator ScoreAnimation(int startScore, int endScore, float duration)
	{
		// 開始時間
		float startTime = Time.time;

		// 終了時間
		float endTime = startTime + duration;

		do
		{
			// 現在の時間の割合
			float timeRate = (Time.time - startTime) / duration;

			// 数値を更新
			int updateValue = (int)((endScore - startScore) * timeRate + startScore);

			// テキストの更新
			scoreText.text = updateValue.ToString("D3");

			// 1フレーム待つ
			yield return null;

		} while (Time.time < endTime);

		// 最終的な着地のスコア
			scoreText.text = endScore.ToString("D3");
	}

	private void TextAnimation()
	{
		scoreTextTransform.localScale *= 1 + (animStats * 0.01f);
		if(scoreTextTransform.localScale.x > 1.3f)
		{
			animStats *= -1;
		}
		if (scoreTextTransform.localScale.x <= 1.0f)
		{
			scoreTextTransform.localScale = new Vector3(1.0f,1.0f,1.0f);
			animStats = 0;
		}
	}

}