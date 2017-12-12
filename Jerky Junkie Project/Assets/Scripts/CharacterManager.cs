using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterManager : SingletonMonoBehaviour<CharacterManager> {

	[System.Serializable]
	private struct CharacterData
	{
		public GameObject m_CharacterSprite;
		public string m_SpriteName;
		public int m_SpriteNum{ get; set; }
	}

	[SerializeField]
	int xLength = 8;
	[SerializeField]
	int yLength = 8;

	float Width;
	float Height;

	[SerializeField]
	Transform InitPos;
	Transform Box;

	[SerializeField]
	private CharacterData[] CharaData;
	private Dictionary<string,int> CharaNum = new Dictionary<string, int>();


	private CharacterData[,] CharacterInstance;

	// Use this for initialization
	private void Awake () {
		// CharacterManagerを登録
//		if (this != Instance) {
//			Destroy (this);
//			return;
//		}
		DontDestroyOnLoad (this.gameObject);

		Box = GameObject.Find ("DropBox").GetComponent<Transform> ();

		Init ();

		for (int i = 0; i < CharaData.Length; i++) {
			CharaNum [CharaData [i].m_SpriteName] = i;
		}


	}

	private void Start()
	{
		for (int i = 0; i < CharacterInstance.GetLength(0); i++) {
			for (int j = 0; j < CharacterInstance .GetLength(1); j++) {

				int l_random = Random.Range (0, CharaData.Length);
				Width = CharaData [0].m_CharacterSprite.GetComponent<RectTransform> ().sizeDelta.x;
				Height = CharaData [0].m_CharacterSprite.GetComponent<RectTransform> ().sizeDelta.y;

				Vector2 vec2Pos = new Vector2 (InitPos.position.x + Width * j,InitPos.position.y - Height * i);
				RandomCreate(i,j, vec2Pos,0,CharaData.Length - 1);

			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void Init()
	{
		CharacterInstance = new CharacterData[xLength,yLength];

		for (int i = 0; i < CharacterInstance.GetLength(0); i++) {
			for (int j = 0; j < CharacterInstance.GetLength (1); j++) {

				CharacterInstance [i, j].m_SpriteName = null;
				CharacterInstance[i,j].m_SpriteNum = CharaData.Length + 1;
				Debug.Log (CharacterInstance [i, j].m_SpriteNum);
			}
		}
	}

	public void CreateCharaInstance_No(int Chara_Num,int x_Num,int y_Num,Vector2 vec2Pos)
	{
		CharacterInstance [x_Num,y_Num].m_CharacterSprite = GameObject.Instantiate(CharaData [Chara_Num].m_CharacterSprite,new Vector3(vec2Pos.x,vec2Pos.y,0),Quaternion.identity) as GameObject;
		CharacterInstance [x_Num, y_Num].m_CharacterSprite.transform.SetParent (Box);
		CharacterInstance [x_Num,y_Num].m_SpriteNum = CharaData [Chara_Num].m_SpriteNum;
	}

	public void CreateCharaInstance_Name(string Chara_Name,int x_Num,int y_Num,Vector2 vec2Pos)
	{
		CharacterInstance [x_Num,y_Num].m_CharacterSprite = Instantiate(CharaData [CharaNum[Chara_Name]].m_CharacterSprite,new Vector3 (vec2Pos.x, vec2Pos.y, 0),Quaternion.identity);
		CharacterInstance [x_Num,y_Num].m_SpriteNum = CharaData [CharaNum[Chara_Name]].m_SpriteNum;
	}

	public void getCharaInstance(int x_Num,int y_Num,ref GameObject _output,ref int _Num)
	{
		_output = CharacterInstance [x_Num,y_Num].m_CharacterSprite;
		_Num = CharacterInstance [x_Num,y_Num].m_SpriteNum;
	}

	void RandomCreate(int x_Num,int y_Num,Vector2 vec2Pos,int rangeMin,int rangeMax)
	{
		int l_ramdom = Random.Range (rangeMin, rangeMax);
		CreateCharaInstance_No (l_ramdom, x_Num, y_Num, vec2Pos);
	}

	public void DestoryInstance(int x_Num,int y_Num)
	{
		Destroy (CharacterInstance [x_Num,y_Num].m_CharacterSprite);
		CharacterInstance [x_Num,y_Num].m_SpriteName = null;
		CharacterInstance [x_Num,y_Num].m_SpriteNum = CharaData.Length + 1;
	}
}
