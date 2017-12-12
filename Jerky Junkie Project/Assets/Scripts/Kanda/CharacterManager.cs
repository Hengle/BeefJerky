using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum direction
{
	Left,
	Right,
	Up,
	Down
}

[System.Serializable]
internal struct CharacterData
{
	public GameObject m_CharacterSprite;
	public string m_SpriteName;
	internal int m_SpriteNum;
}

public class CharacterManager : SingletonMonoBehaviour<CharacterManager> {


	[SerializeField]
	int xLength = 8;
	[SerializeField]
	int yLength = 8;

	int DestroyCount = 0;
	int searchX,searchY;

	float Width;
	float Height;

	[SerializeField]
	Transform InitPos;
	Transform Box;

	[SerializeField]
	private CharacterData[] CharaData;
	private Dictionary<string,int> CharaNum = new Dictionary<string, int>();

	List<GameObject> quere;
	internal CharacterData[,] CharacterInstance;

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
			CharaData [i].m_SpriteNum = CharaNum [CharaData [i].m_SpriteName];
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
				RandomCreate(i,j, vec2Pos,0,CharaData.Length);

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

	// つながっていれば削除する処理
	void RootDestroy()
	{
//		// 探す
//		for (int i = 0; i < CharacterInstance.GetLength (0); i++) {
//			for (int j = 0; j < CharacterInstance.GetLength (1); j++) {
//				while (RootFlg (i, j)) {
//					DestroyCount++;
//					quere.Add (CharacterInstance [i, j].m_CharacterSprite);
//					CharacterInstance [i, j].m_SpriteNum = CharaData.Length + 1;
//
//				}
//			}
//		}
	}


	internal void search(int num, ref List<CharacterData> objList,int x,int y)
	{
		
//		var s(int x,int y) => {
//			if (!objList.Find (CharacterInstance [x != 0? x + direction : 0, y].m_CharacterSprite)) {
//				objList.Add (CharacterInstance [x != 0? x + direction : 0, y].m_CharacterSprite);
//				search (objList, x != 0? x + direction : 0, y);
//			}
//		}
			/*((int x,int y)=>
			if (!objList.Find (CharacterInstance [x != 0? x + direction : 0, y].m_CharacterSprite)) {
			objList.Add (CharacterInstance [x != 0? x + direction : 0, y].m_CharacterSprite);
			search (objList, x != 0? x + direction : 0, y);
		})*/
		int direction = -1;
		int _x,_y;
		// 上探索
		_x = x != 0? x + direction : 0;
		if (CharacterInstance [_x, y].m_SpriteNum != CharaData.Length + 1) {
			CharacterData up = objList.Find (z => z.m_CharacterSprite.GetInstanceID () == CharacterInstance [_x, y].m_CharacterSprite.GetInstanceID ());
			if (up.m_CharacterSprite == null && CharacterInstance [_x, y].m_SpriteNum == num) {
				DestroyCount++;
				objList.Add (CharacterInstance [_x, y]);
				search (num, ref objList, _x, y);
			}
		}
		// 左探索
		_y = y != 0? y + direction : 0;
		if (CharacterInstance [x, _y].m_SpriteNum != CharaData.Length + 1) {
			CharacterData left = objList.Find (z => z.m_CharacterSprite.GetInstanceID () == CharacterInstance [x, _y].m_CharacterSprite.GetInstanceID ());
			if (left.m_CharacterSprite == null && CharacterInstance [x, _y].m_SpriteNum == num) {
				DestroyCount++;
				objList.Add (CharacterInstance [x, _y]);
				search (num,ref objList, x, _y);
			}
		}

		direction *= -1;
		// 下探索
		_x = x != CharacterInstance.GetLength(0) - 1? x + direction : CharacterInstance.GetLength(0) - 1;
		if (CharacterInstance [_x, y].m_SpriteNum != CharaData.Length + 1) {
			CharacterData down = objList.Find (z => z.m_CharacterSprite.GetInstanceID () == CharacterInstance [_x, y].m_CharacterSprite.GetInstanceID ());
			if (down.m_CharacterSprite == null && CharacterInstance [_x, y].m_SpriteNum == num) {
				DestroyCount++;
				objList.Add (CharacterInstance [_x, y]);
				search (num,ref objList, _x, y);
			}
		}
		// 右探索
		_y = y != CharacterInstance.GetLength(1) - 1? y + direction : CharacterInstance.GetLength(1) - 1;
		if (CharacterInstance [x,_y].m_SpriteNum != CharaData.Length + 1) {
			CharacterData right = objList.Find (z => z.m_CharacterSprite.GetInstanceID () == CharacterInstance [x, _y].m_CharacterSprite.GetInstanceID ());
			if (right.m_CharacterSprite == null && CharacterInstance [x, _y].m_SpriteNum == num) {
				DestroyCount++;
				objList.Add (CharacterInstance [x, _y]);
				search (num,ref objList, x, _y);
			}
		}

	}

	private void CheckSpriteNum(int y,int x){
	}

	public void CreateCharaInstance_No(int Chara_Num,int x_Num,int y_Num,Vector2 vec2Pos)
	{
		if(!CharacterInstance [x_Num,y_Num].m_CharacterSprite)
			CharacterInstance [x_Num,y_Num].m_CharacterSprite = GameObject.Instantiate(CharaData [Chara_Num].m_CharacterSprite,new Vector3(vec2Pos.x,vec2Pos.y,0),Quaternion.identity) as GameObject;
		CharacterInstance [x_Num, y_Num].m_CharacterSprite.transform.SetParent (Box);
		CharacterInstance [x_Num,y_Num].m_SpriteNum = CharaData [Chara_Num].m_SpriteNum;
	}

	public void CreateCharaInstance_Name(string Chara_Name,int x_Num,int y_Num,Vector2 vec2Pos)
	{
		if(!CharacterInstance [x_Num,y_Num].m_CharacterSprite)
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

	internal void CombinationSearch(int x,int y){
		List<CharacterData> list = new List<CharacterData>();
		switch (CharacterInstance [x, y].m_SpriteNum) {
		case 0://牛 仲間がいたらまとまってジャーキーになる
			
			search (0,ref list, x, y);
			if (list.Count >= 4)
				RootDestoryInstance (ref list);//とりあえず削除
			break;
		default:
			
			break;
		}
	}

	internal void Combination(int num,List<CharacterData> list){
		switch (num) {
		case 0:
			foreach (CharacterData data in list)
				if (data.m_CharacterSprite)
					Destroy (data.m_CharacterSprite);
			break;
		case 1:
			foreach (CharacterData data in list)
				if (data.m_CharacterSprite)
					Destroy (data.m_CharacterSprite);
			break;
		case 2:
			break;
		case 3:
			break;
		}
	}

	internal void RootDestoryInstance(ref List<CharacterData> objList)
	{
		for (int i = 0; i < objList.Count; i++) {
			CharacterData newList = objList[i];
			Destroy (newList.m_CharacterSprite);
			newList.m_SpriteName = "a";
			newList.m_SpriteNum = CharaData.Length + 1;
			objList [i] = newList;
		}
	}

	public void DirectionMove(int i,int j,int M)
	{
		int l_x = 0, l_y = 0;
		if (M == (int)direction.Left) {
			l_x = -1;
		}else if (M == (int)direction.Right) {
			l_x = 1;
		}else if (M == (int)direction.Up) {
			l_y = -1;
		}else if(M == (int)direction.Down) {
			l_y = 1;
		}


		Vector3 after;
		after = CharacterInstance [i, j].m_CharacterSprite.GetComponent<RectTransform>().position;
		CharacterInstance [i, j].m_CharacterSprite.GetComponent<RectTransform>().position = CharacterInstance [i - 1, j].m_CharacterSprite.GetComponent<RectTransform>().position;
		CharacterInstance [i - 1, j].m_CharacterSprite.GetComponent<RectTransform>().position = after;

		string _afterS;
		_afterS = CharacterInstance [i, j].m_SpriteName;
		CharacterInstance [i, j].m_SpriteName = CharacterInstance [i + l_x, j + l_y].m_SpriteName;
		CharacterInstance [i + l_x, j + l_y].m_SpriteName = _afterS;

		int _afterI;
		_afterI = CharacterInstance [i, j].m_SpriteNum;
		CharacterInstance [i, j].m_SpriteNum = CharacterInstance [i + l_x, j + l_y].m_SpriteNum;
		CharacterInstance [i + l_x, j + l_y].m_SpriteNum = _afterI;
	}

	//internal void DirectionObjMove(CharacterData DropX,CharacterData DropY)
	internal void DirectionObjMove(int x1,int y1,int x2,int y2)
	{
		CharacterData data = CharacterInstance [x1, y1];
		CharacterInstance [x1, y1] = CharacterInstance [x2, y2];
		CharacterInstance [x2, y2] = data;
		CharacterData DropX = CharacterInstance [x1, y1];
		CharacterData DropY = CharacterInstance [x2, y2];

		Vector3 after;
		after = DropX.m_CharacterSprite.GetComponent<RectTransform>().position;
		DropX.m_CharacterSprite.GetComponent<RectTransform>().position = DropY.m_CharacterSprite.GetComponent<RectTransform>().position;
		DropY.m_CharacterSprite.GetComponent<RectTransform>().position = after;

		/*
		string afterName;
		afterName = DropX.m_SpriteName;
		DropX.m_SpriteName = DropY.m_SpriteName;
		DropY.m_SpriteName = afterName;

		int afterNum;
		afterNum = DropX.m_SpriteNum;
		DropX.m_SpriteNum = DropY.m_SpriteNum;
		DropY.m_SpriteNum = afterNum;
		*/
	}
}
