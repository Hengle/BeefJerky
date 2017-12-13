using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterData2
{
    public Sprite m_CharacterSprite;
    public string m_SpriteName;
    internal int m_SpriteNum;
}

public class CharacterManager2 : SingletonMonoBehaviour<CharacterManager2> {
    [SerializeField]
    private int xLength, yLength;
    private Vector2 dropSize;

    [SerializeField]
    private CharacterData2[] charactersData;
    [SerializeField]
    private Character characterPrefab;
    private Dictionary<string, int> CharaNum = new Dictionary<string, int>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();

        
        for (int i = 0; i < charactersData.Length; i++) {
            CharaNum[charactersData[i].m_SpriteName] = i;
            charactersData[i].m_SpriteNum = CharaNum[charactersData[i].m_SpriteName];
        }
    }

    private void Init()
    {
        //StageにCharacterを配置　Stageから呼び出すので不要
    }

    private void Start()
    {
        /*
        for (int i = 0; i < CharacterInstance.GetLength(0); i++)
        {
            for (int j = 0; j < CharacterInstance.GetLength(1); j++)
            {

                int l_random = Random.Range(0, charactersData.Length);

                // 全マス均一な大きさにするため、最初のマスの大きさを使う
                dropSize.x = charactersData[0].m_CharacterSprite.GetComponent<RectTransform>().sizeDelta.x;
                dropSize.y = charactersData[0].m_CharacterSprite.GetComponent<RectTransform>().sizeDelta.y;

                //生成地点を取得する
                //Vector2 vec2Pos = new Vector2(InitPos.position.x + Width * j, InitPos.position.y - Height * i);
                //RandomCreate(i, j, vec2Pos, 0, charactersData.Length);

            }
        }*/
    }

    /// <summary>
    /// 指定した番号の種類のCharacterを生成する
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public Character CreateCharacter(int num) {
        Character chara = Instantiate(characterPrefab);
        chara.data = CreateCharacterData(num);

        return chara;
    }

    /// <summary>
    /// 指定された名前のCharacterDataを取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Character CreateCharacter(string name) {
        Character chara = Instantiate(characterPrefab);
        chara.data = CreateCharacterData(name);

        return chara;
    }

    /// <summary>
    /// ランダムな種類のCharacterを生成する
    /// </summary>
    /// <returns></returns>
    public Character RandomCreate() {
        return CreateCharacter(Random.Range(0, charactersData.Length));
    }

    /// <summary>
    /// 指定された番号のキャラクターデータを取得する
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public CharacterData2 CreateCharacterData(int num) {
        return charactersData[num];
    }

    /// <summary>
    /// 指定した名前のキャラクターデータを取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public CharacterData2 CreateCharacterData(string name) {
        foreach (CharacterData2 data in charactersData)
        {
            if (data.m_SpriteName == name)
                return data;
        }
        return charactersData[0];
    }

    internal void RootDestoryInstance(List<GameObject> objList)
    {
        foreach (GameObject obj in objList) {
            //一つずつ削除してもらう
            StageManager.Instance.DeleteCharacter(obj.GetComponent<Character>());
        }
        /*
        for (int i = 0; i < objList.Count; i++)
        {
            CharacterData newList = objList[i];
            Destroy(newList.m_CharacterSprite);
            newList.m_SpriteName = "NULL";
            newList.m_SpriteNum = CharaData.Length + 1;
            objList[i] = newList;
        }*/
    }

    /// <summary>
    /// 指定したCharacterを削除する処理
    /// </summary>
    /// <param name="target"></param>
    private void Destroyinstance(Character target) {
        //Stageの方から消す形に
        StageManager.Instance.DeleteCharacter(target);
    }

    /// <summary>
    /// 指定した二つの地点のキャラクターを入れ替える
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    internal void DirectionObjMove(int x1, int y1, int x2, int y2) {
        //Stageで入れ替える形に
        Character c1 = StageManager.Instance.Stage[x1, y1].stayCharacter;
        Character c2 = StageManager.Instance.Stage[x2, y2].stayCharacter;

        StageManager.Instance.Stage[x1, y1].RemoveCharacter();
        StageManager.Instance.Stage[x2, y2].RemoveCharacter();

        StageManager.Instance.Stage[x1, y1].AddCharacter(c2);
        StageManager.Instance.Stage[x2, y2].AddCharacter(c1);
    }

    /// <summary>
    /// 
    /// </summary>
    internal void search(int num, ref List<Character> objList, int x, int y)
    {
        /*
        int direction = -1;
        int _x, _y;

        int[,] dirs = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
        for(int i = 0;i < dirs.GetLength(0);i++) {
            _x = x + dirs[i, 0];
            _x = (_x < 0 ? 0 : (_x >= StageManager.Instance.StageLength[0] ? StageManager.Instance.StageLength[0] - 1 : _x));
            _y = y + dirs[i, 1];
            _y = (_y < 0 ? 0 : (_y >= StageManager.Instance.StageLength[1] ? StageManager.Instance.StageLength[1] - 1 : _y));


            GameObject up = objList.Find(z => z.gameObject == StageManager.Instance.Stage[_x, _y].stayCharacter.gameObject);
            if (up.m_CharacterSprite == null && CharacterInstance[_x, _y].m_SpriteNum == num) {
                DestroyCount++;
                objList.Add(CharacterInstance[_x, _y]);
                search(num, ref objList, _x, _y);
            }
        }

        // 上探索
        _x = x != 0 ? x + direction : 0;
        if (CharacterInstance[_x, y].m_SpriteNum != CharaData.Length + 1)
        {
            CharacterData up = objList.Find(z => z.m_CharacterSprite.GetInstanceID() == CharacterInstance[_x, y].m_CharacterSprite.GetInstanceID());
            if (up.m_CharacterSprite == null && CharacterInstance[_x, y].m_SpriteNum == num)
            {
                DestroyCount++;
                objList.Add(CharacterInstance[_x, y]);
                search(num, ref objList, _x, y);
            }
        }
        // 左探索
        _y = y != 0 ? y + direction : 0;
        if (CharacterInstance[x, _y].m_SpriteNum != CharaData.Length + 1)
        {
            CharacterData left = objList.Find(z => z.m_CharacterSprite.GetInstanceID() == CharacterInstance[x, _y].m_CharacterSprite.GetInstanceID());
            if (left.m_CharacterSprite == null && CharacterInstance[x, _y].m_SpriteNum == num)
            {
                DestroyCount++;
                objList.Add(CharacterInstance[x, _y]);
                search(num, ref objList, x, _y);
            }
        }

        direction *= -1;
        // 下探索
        _x = x != CharacterInstance.GetLength(0) - 1 ? x + direction : CharacterInstance.GetLength(0) - 1;
        if (CharacterInstance[_x, y].m_SpriteNum != CharaData.Length + 1)
        {
            CharacterData down = objList.Find(z => z.m_CharacterSprite.GetInstanceID() == CharacterInstance[_x, y].m_CharacterSprite.GetInstanceID());
            if (down.m_CharacterSprite == null && CharacterInstance[_x, y].m_SpriteNum == num)
            {
                DestroyCount++;
                objList.Add(CharacterInstance[_x, y]);
                search(num, ref objList, _x, y);
            }
        }
        // 右探索
        _y = y != CharacterInstance.GetLength(1) - 1 ? y + direction : CharacterInstance.GetLength(1) - 1;
        if (CharacterInstance[x, _y].m_SpriteNum != CharaData.Length + 1)
        {
            CharacterData right = objList.Find(z => z.m_CharacterSprite.GetInstanceID() == CharacterInstance[x, _y].m_CharacterSprite.GetInstanceID());
            if (right.m_CharacterSprite == null && CharacterInstance[x, _y].m_SpriteNum == num)
            {
                DestroyCount++;
                objList.Add(CharacterInstance[x, _y]);
                search(num, ref objList, x, _y);
            }
        }
        */
    }

    /**
	 * @brief				組になっているかどうかサーチする処理
	 * @param[in] x			x軸配列座標
	 * @param[in] y			y軸配列座標
	 */
    internal void CombinationSearch(int x, int y)
    {
        List<CharacterData> list = new List<CharacterData>();
        switch (StageManager.Instance.Stage[x, y].stayCharacter.data.m_SpriteNum)
        {
            case 0://牛 仲間がいたらまとまってジャーキーになる
                //search(CharaNum["Gyu"], ref list, x, y);
                //search();
                if (list.Count >= 4)
                    RootDestoryInstance(/*list*/new List<GameObject>());//とりあえず削除
                break;
            default:

                break;
        }
    }
    /*
    private void RootDestroy
        */
}
