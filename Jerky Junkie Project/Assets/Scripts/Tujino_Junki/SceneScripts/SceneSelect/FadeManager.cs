using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
	//float alpha;
	Texture2D blackTexture;		//暗転用黒テクスチャ.
	float Alpha = 0;			//フェード中の透明度.
	bool fadeflg = true;        //フェード中かどうかのフラグ.
                                //public bool fade_playFLg;

    private float interval = 1;//フェード時間の設定

    void Awake()
	{
		if(this != Instance)
		{
			Destroy(this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);    //オブジェクトを破棄しない.

		StartCoroutine(Pixel());
	}

	private IEnumerator Pixel()
	{
		//ここで黒テクスチャ作る.
		blackTexture = new Texture2D(32, 32, TextureFormat.RGB24, false);
		yield return new WaitForEndOfFrame();
		blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
		blackTexture.SetPixel(0, 0, Color.white);
		blackTexture.Apply();
	}

	void OnGUI()
	{
		if(fadeflg == false)
		{
			return;
		}
			//透明度を更新して黒テクスチャを描画.
			GUI.color = new Color (0, 0, 0, Alpha);
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), blackTexture);
	}
			
	// 画面遷移
	public void LoadLevel(string SceneName)
	{
		StartCoroutine (TransScene (SceneName));
	}

	IEnumerator TransScene (string SceneName)
	{
		//だんだん暗く
		fadeflg = true;
		float time = 0;
        while (time <= interval)
		{
			Alpha = Mathf.Lerp (0f, 1f, time / interval);      
			time += Time.unscaledDeltaTime;
            //DownBGM (fade_playFLg);//音の再生
            yield return 0;
		}
		
		//シーン切替
		SceneManager.LoadScene(SceneName);//引数で得た名前のシーンに遷移
		
		//だんだん明るく
		time = 0;
		while (time <= interval)
		{
			Alpha = Mathf.Lerp (1f, 0f, time / interval);
			time += Time.unscaledDeltaTime;
            yield return 0;
		}
        fadeflg = false;
	}

	//void DownBGM(bool f)
	//{
 //       if (f)
 //       {
 //           //AudioManager.BGMSource.volume -= Time.unscaledDeltaTime;
 //       }
	//}
}

