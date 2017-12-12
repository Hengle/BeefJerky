using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

	private static EffectManager instance;
	[System.Serializable]
	class EffectClass
	{
		public GameObject effect = null;
		public string effectKey = null;
	}
	[SerializeField]
	EffectClass[] effectClass = new EffectClass[0];
	public Dictionary<string, GameObject> effectDate = new Dictionary<string, GameObject>();

	GameObject nullObj;

	[SerializeField] float posZ= 0.0f;

	public static EffectManager Instance
	{
		get
		{
			if (null == instance)
			{
				instance = (EffectManager)FindObjectOfType(typeof(EffectManager));
				if (null == instance)
				{
					Debug.Log(" SoundManager Instance Error ");
				}
			}
			return instance;
		}
	}

	void Awake()
	{

		GameObject[] obj = GameObject.FindGameObjectsWithTag("EffectManager");
		if (1 < obj.Length)
		{
			// 既に存在しているなら削除
			Destroy(gameObject);
		}
		else
		{
			// シーン遷移では破棄させない
			DontDestroyOnLoad(gameObject);
		}

		for (int i = 0; i < effectClass.Length; i++)
		{
			effectDate.Add(effectClass[i].effectKey, effectClass[i].effect);
		}

	}

	public void PlayEffect(string effectKey,Vector3 pos,float lifeTime) //Vector3型、消滅時間を指定
	{
		if (effectDate.TryGetValue(effectKey, out nullObj))
		{
			GameObject obj =  Instantiate(nullObj,pos,transform.rotation)as GameObject;
			Destroy(obj,lifeTime);
		}
		else
		{
			Debug.Log(effectKey+"に対応したSEはありません");
		}
	}

	public void PlayEffect(string effectKey, Vector2 pos, float lifeTime) //Vector2型、消滅時間を指定
	{
		if (effectDate.TryGetValue(effectKey, out nullObj))
		{
			GameObject obj = Instantiate(nullObj, new Vector3(pos.x,pos.y,posZ), transform.rotation) as GameObject;
			Destroy(obj, lifeTime);
		}
		else
		{
			Debug.Log(effectKey + "に対応したSEはありません");
		}
	}

	public void PlayEffect(string effectKey, Vector3 pos) //Vector3型、消滅時間を指定しない
	
	{
		if (effectDate.TryGetValue(effectKey, out nullObj))
		{
			GameObject obj = Instantiate(nullObj, pos, transform.rotation) as GameObject;
		}
		else
		{
			Debug.Log(effectKey + "に対応したSEはありません");
		}
	}

	public void PlayEffect(string effectKey, Vector2 pos) //Vector2型、消滅時間を指定しない
	{
		if (effectDate.TryGetValue(effectKey, out nullObj))
		{
			GameObject obj = Instantiate(nullObj, new Vector3(pos.x, pos.y, posZ), transform.rotation) as GameObject;
		}
		else
		{
			Debug.Log(effectKey + "に対応したSEはありません");
		}
	}
}