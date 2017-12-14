using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct CharacterData2
{
    public Sprite m_CharacterSprite;
    public string m_SpriteName;
    [SerializeField]
    internal DropType m_DropType;
    public int[] path;
}

public enum DropType {
    usi,//牛
    jaki,//ジャーキー
    ozisan,//おじさん
    biru,//ビール
}

public class CharacterManager2 : SingletonMonoBehaviour<CharacterManager2> {
    [SerializeField]
    private int xLength, yLength;
    private Vector2 dropSize;

    [SerializeField]
    private CharacterData2[] charactersData;
    [SerializeField]
    private Character characterPrefab;
    //private Dictionary<string, int> CharaNum = new Dictionary<string, int>();
    public string DestroyName;
    [SerializeField]
    private int comboCount;
    //ComboTextの透明度取得
    public float comboTextAlpha { get{
            if (comboTime > 0)
                return comboTime / ConstData.COMBO_LIMIT_TIME;
            else return 0;
        } }
    public float comboBonus { get { return 1 + comboCount / 10.0f; } }
    private float comboTime;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        ComboCheck();
    }

    /// <summary>
    /// コンボの時間経過を確認する
    /// </summary>
    private void ComboCheck() {
        if (comboCount == 0) return;
        comboTime -= Time.deltaTime;
        if (comboTime <= 0){
            comboCount = 0;
            comboTime = 0;
        }
    }

    /// <summary>
    /// コンボ数を増やす
    /// </summary>
    /// <param name="objects"></param>
    private void AddCombo(List<GameObject> objects) {
        comboCount++;
        comboTime = ConstData.COMBO_LIMIT_TIME;

        //combo表示位置を計算する
        Vector3 comboPos = Vector3.zero;
        if (objects.Count > 0) {
            foreach (GameObject obj in objects) {
                comboPos += obj.transform.position;
            }
            comboPos /= objects.Count - 1;
        }
        
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
        //chara.data.m_SpriteNum = charactersData[CharaNum[name]].m_SpriteNum;
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
        Debug.Log(charactersData[num].m_DropType);
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
            if (obj)
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
        Character c1 = StageManager.Instance.Stage[x1, y1].holdCharacter;
        Character c2 = StageManager.Instance.Stage[x2, y2].holdCharacter;

        StageManager.Instance.Stage[x1, y1].RemoveCharacter();
        StageManager.Instance.Stage[x2, y2].RemoveCharacter();

        StageManager.Instance.Stage[x1, y1].AddCharacter(c2);
        StageManager.Instance.Stage[x2, y2].AddCharacter(c1);
    }

    /// <summary>
    /// 周囲にいる同タイプのCharacterを探し、繋がっているもの全てを返す処理
    /// </summary>
    /// <param name="type">探索対象のキャラクタータイプ</param>
    /// <param name="objList">発見済みのCharacterリスト</param>
    /// <param name="x">探索基準点x</param>
    /// <param name="y">探索基準点y</param>
    /// <param name="isBomb">探索先が違うtypeでもListに含めるか　爆発の場合はtrueに</param>
    /// <param name="isCircle">斜めの位置も見るか　爆発の場合はtrueに</param>
    /// /// <param name="limit">探索する限界距離</param>
    internal void search(DropType type,List<GameObject> objList, int x, int y,bool isBomb = false,bool isCircle = false,int limit = 100)
    {
        if (limit <= 0) return;
        int _x, _y;
        
        int[,] dirs = isCircle ?
            //爆発の場合は斜めも見るので、合計９要素の配列
            new int[,] { { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, 1 }, { 0, 0 }, { 0, -1 }, { -1, 1 }, { -1, 0 }, { -1, -1 } } :
            //通常の場合なら上下左右の４要素
            new int[,] { { 1, 0 }, { 0, 1 }, { 0, -1 }, { -1, 0 } };
        for(int i = 0;i < dirs.GetLength(0);i++) {
            //探索先の座標が存在するか確認　存在しなければcontinue
            _x = x + dirs[i, 0];
            if (_x < 0 || _x >= StageManager.Instance.StageLength[0]) continue;
            //_x = (_x < 0 ? 0 : (_x >= StageManager.Instance.StageLength[0] ? StageManager.Instance.StageLength[0] - 1 : _x));
            _y = y + dirs[i, 1];
            if (_y < 0 || _y >= StageManager.Instance.StageLength[1]) continue;
            _y = (_y < 0 ? 0 : (_y >= StageManager.Instance.StageLength[1] ? StageManager.Instance.StageLength[1] - 1 : _y));

            if (StageManager.Instance.Stage[_x, _y].character == null) continue;
            GameObject chara = objList.Find(z => z == StageManager.Instance.Stage[_x, _y].holdCharacter.gameObject);
            //探索先が同タイプでリストに含まれていない場合、探索先をリストに含めた後さらにそこから探索を開始する
            if (chara == null && StageManager.Instance.Stage[_x, _y].holdCharacter.data.m_DropType == type) {
                StageManager.Instance.Stage[_x, _y].character.isChecked = true;
                objList.Add(StageManager.Instance.Stage[_x, _y].holdCharacter.gameObject);
                search(type,objList, _x, _y,isBomb,isCircle,--limit);
            }
            else if(isBomb){
                //爆発の場合、探索先をListに含めて処理を終了する
            }
        }
    }

    /// <summary>
    /// キャラクターの組み合わせ効果の発動
    /// </summary>
    /// <param name="characters"></param>
    /// <param name="type"></param>
    public void Combination(List<GameObject> characters, DropType type, int x = 0,int y = 0) {
        int score = 0;
        switch (type) {
            case DropType.usi:
                break;
            case DropType.ozisan:
                if (StageManager.Instance.Stage[x, y].character.isChecked) break;
                //ジャーキーと隣あったら、そのジャーキーと繋がっている全てのジャーキーを消滅させる
                search(DropType.jaki, characters, x, y);
                //同時に、消したキャラクターの周囲１マス内（斜め含む）にあるビールを消滅させる
                search(DropType.jaki, characters, x, y, true);
                List<GameObject> charas = new List<GameObject>(characters);
                //ジャーキーやビールの数
                int jakiCount = 0;
                foreach (GameObject c in charas) {
                    Character character = c.GetComponent<Character>();
                    if (character.data.m_DropType == DropType.jaki)
                    {
                        search(DropType.biru, characters, character.data.path[0], character.data.path[1], false, true, 1);
                        jakiCount++;
                    }
                }
                if (jakiCount == 0) break;//ジャーキーが無ければ消さない
                //int biruCount = 0;
                //foreach (GameObject c in characters) {
                //    if (c.GetComponent<Character>().data.m_DropType == DropType.biru) biruCount++;
                //}

                score = (5000 + (jakiCount - 1) * 1000);//* (1 + biruCount / 10);
                if (score > 0)
                {
                    AddScore(score, characters);
                }

                AddTime(2 + (jakiCount - 1) * 0.5f);

                RootDestoryInstance(characters);
                break;
            case DropType.biru:
                if (StageManager.Instance.Stage[x, y].character.isChecked) break;
                search(DropType.biru, characters, x, y);
                if (characters.Count >= 4) {
                    score = characters.Count * 500 - 1000;
                    if (score > 0)
                    {
                        AddScore(score, characters);
                    }
                    RootDestoryInstance(characters);
                }
                break;
            case DropType.jaki:
            default:
                break;
        }
    }

    /// <summary>
    /// 時間延長処理
    /// </summary>
    /// <param name="value"></param>
    private void AddTime(float value) {
        TimerController.Instance.AddTime(value * comboBonus);
    }

    /// <summary>
    /// スコアの獲得処理
    /// </summary>
    /// <param name="value"></param>
    private void AddScore(int value,List<GameObject> objects) {
        HiScore.Instance.AddPoint(Mathf.FloorToInt(100 * comboBonus));
        AddCombo(objects);
    }

    /**
	 * @brief				組になっているかどうかサーチする処理
	 * @param[in] x			x軸配列座標
	 * @param[in] y			y軸配列座標
	 */
    internal void CombinationSearch(int x, int y)
    {
        List<CharacterData> list = new List<CharacterData>();
        switch (StageManager.Instance.Stage[x, y].holdCharacter.data.m_DropType)
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

    public bool getObjFlg(int x, int y)
    {
        Debug.Log(StageManager.Instance.Stage[x, y].holdCharacter.data.m_DropType);
        return StageManager.Instance.Stage[x, y].holdCharacter.data.m_DropType == DropType.usi;
    }

    /*
    private void RootDestroy
        */
}
