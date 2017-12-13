using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージのマネージャーClass
/// </summary>
public class StageManager : SingletonMonoBehaviour<StageManager> {
    public StageChip[,] Stage;//ステージ配列
    public int[] StageLength { get { return new int[] { x, y }; } }
    private StageChip[,] backGroundStage;//ステージの上に配置する落ちてくる用のChip置き場
    [SerializeField]
    private int x, y;//Stageの大きさ
    [SerializeField]//ステージ枠の大きさ ステージの中心位置
    private Vector2 StageSize,DefaultPosition;
    
    [SerializeField]
    private Sprite[] data;//使用するマス画像
    [SerializeField]
    private StageChip StageChipPrefab;
    [SerializeField]
    private Vector2 chipSze;//,offset;//余白は無し
    [SerializeField]
	public List<Character> characterPrefabs;
	[SerializeField]
	public Character beefjarkeyPrefab;

    private bool updateFlag;
    public bool stopFlag;

    //演出などの待機するべき処理がいくつ実行中か　０になった時、おじさんとジャーキーの組み合わせなどを確認する
    [SerializeField]
    private int _count;
    public void MoveStart() {
        _count++;
        isWaiting = true;
    }
    [SerializeField]
    private bool isMoveEnd = true;
    public void MoveEnd() {
        _count--;
        isMoveEnd = true;
    }
    [SerializeField]
    private bool isWaiting;
    public bool isMoveWaitEnd {
        get {
            if (isWaiting && _count == 0) {
                isWaiting = false;
                return true;
            }
            return false;
        }
    }


    // Use this for initialization
    void Start () {
        Stage = InitStage();
        backGroundStage = InitStage(true);
        CharacterInit(Stage);
        CharacterInit(backGroundStage,true);
        //CharacterManager2.Instance.CharactersDateInitialize(characterPrefabs);
        //foreach (Character c in characterPrefabs) {
        //    Debug.Log(c.data.m_DropType);
        //}
	}

    private void Update()
    {
        if (Time.timeScale == 0 || stopFlag) return;
        //キャラクターが削除された時など、必要な時のみ処理する
        if (updateFlag) {
            updateFlag = false;
            GravityUpdate();

            foreach (StageChip chip in backGroundStage)
            {
                if (chip.holdCharacter == null)
                    chip.AddCharacter(InitCharacter(true));
            }
        }
        if (isMoveEnd) {
            CombinationUpdate();
            isMoveEnd = false;
        }
        /*
        if (isMoveWaitEnd) {
            CombinationUpdate();
            Debug.Log("waitEnd");
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            //おじさんとジャーキーの隣合わせ確認用
            Debug.Log("enter");
            CombinationUpdate();
        }*/
    }

    //ステージに配置されているキャラクターの組み合わせ確認
    public void CombinationUpdate() {
        for (int i = 0; i < Stage.GetLength(0); i++) {
            for (int j = 0; j < Stage.GetLength(1); j++) {
                if (Stage[i,j].character && (Stage[i, j].character.data.m_DropType == DropType.ozisan || Stage[i,j].character.data.m_DropType == DropType.biru)) {
                    List<GameObject> characters = new List<GameObject>() { Stage[i, j].character.gameObject };
                    CharacterManager2.Instance.Combination(characters, Stage[i, j].character.data.m_DropType, i, j);
                }
            }
        }
    }

    /// <summary>
    /// ステージの初期化・生成
    /// </summary>
    private StageChip[,] InitStage(bool isBack = false)
    {
        Vector2 InitStageSize = StageSize;
        //StageSize.x += offset.x;//右端に余白
        StageChip[,] stage = new StageChip[x, y];
        ////ステージの左上の場所を取得
        //Vector2 leftUpSide = new Vector2(-StageSize.x / 2, StageSize.y / 2) + DefaultPosition;//ステージ左上のポジションを取得
        ////マスの大きさを取得
        //Vector2 scale = new Vector2((StageSize.x / ((chipSze.x) * x + offset.x)), StageSize.y / (chipSze.x * y + offset.y));
        //float res = StageSize.x / (chipSze.x * x + offset.x * (x - 1));
        //Debug.Log(res);
        //scale.x = res;

        Vector2 leftUpSide = new Vector2(-StageSize.x / 2, StageSize.y / 2) + DefaultPosition;
        //Vector2 scale = new Vector2((InitStageSize.x / ((chipSze.x) * x + offset.x)), (InitStageSize.y / ((chipSze.y) * y + offset.y)));
        Vector2 scale = new Vector2((InitStageSize.x / ((chipSze.x) * x)), (InitStageSize.y / ((chipSze.y) * y)));

        GameObject parent;
        if (isBack)
        {
            leftUpSide.y += 1000;
            parent = new GameObject("backGround");
        }
        else
            parent = new GameObject("stage");
        parent.transform.SetParent(transform);
        parent.transform.localPosition = Vector2.zero;
        Vector2 pos = leftUpSide;
        //マスを生成
        for (int i = 0; i < y; i++) {
            pos.x = leftUpSide.x + ((chipSze.x * scale.x) * 0.5f);
            for (int j = 0; j < x; j++) {
                stage[j, i] = Instantiate(StageChipPrefab, parent.transform);//StageChip.InitStageChip(data[j % data.Length]);
                Image image = stage[j, i].GetComponent<Image>();
                image.sprite = data[(i + j) % data.Length];
                stage[j, i].path = new int[] { j, i };
                stage[j, i].transform.localPosition = pos;
                //次に配置するマスの位置を計算
                pos.x += chipSze.x * scale.x;// + offset.x;
                stage[j, i].transform.localScale = scale;
                stage[j, i].gameObject.SetActive(!isBack);
            }
            pos.y -= ((chipSze.y * scale.y));// + offset.y;
        }

        return stage;
    }

    /// <summary>
    /// Characterをステージ一杯に生成する処理
    /// </summary>
    /// <param name="stage"></param>
    private void CharacterInit(StageChip[,] stage,bool isBack = false) {
        foreach (StageChip chips in stage) {
            Character character = InitCharacter(isBack);
            chips.AddCharacter(character,true);
        }
    }

    /// <summary>
    /// 一番下の行のCharacterを全て削除する
    /// </summary>
    public void DeleteDownLineCharacter() {
        //int line = Stage.GetLength(1) - 1;
        //int length = Stage.GetLength(0);
        //for (int i = 0; i < length; i++) {
        //    if (Stage[i, line].stayCharacter) {
        //        DeleteCharacter(Stage[i, line].stayCharacter);
        //        DeleteCharacter(Stage[i, line - 1].stayCharacter);
        //    }
        //}
        foreach (StageChip chip in Stage) {
            if (chip.holdCharacter) {
                DeleteCharacter(chip.holdCharacter);
            }
        }
    }

    /// <summary>
    /// Chaarcterを削除する処理
    /// </summary>
    /// <param name="target"></param>
    public void DeleteCharacter(Character target) {
        Destroy(target.gameObject);
        updateFlag = true;
    }

    private GameObject characterParent;
    public GameObject CharacterParent {
        get {
            if (characterParent != null) return characterParent;
            else {
                characterParent = new GameObject("characters");
                characterParent.transform.SetParent(transform);
                return characterParent;
            }
        } }
    private GameObject characterBackParent;
    public GameObject CharacterBackParent {
        get {
            if (characterBackParent != null) return characterBackParent;
            else {
                characterBackParent = new GameObject("backs");
                characterBackParent.SetActive(false);
                characterBackParent.transform.SetParent(transform);
                return characterBackParent;
            }
        }
    }
    /// <summary>
    /// Characterを生成する処理
    /// </summary>
    /// <returns></returns>
    private Character InitCharacter(bool isBack = false) {
        Character chara = Instantiate(characterPrefabs[Random.Range(0, characterPrefabs.Count)]);
        if (isBack) {
            chara.transform.SetParent(CharacterBackParent.transform);
        }
        else
            chara.transform.SetParent(CharacterParent.transform);
        return chara;
    }

    /// <summary>
    /// Characterを落下させる処理
    /// </summary>
    public void GravityUpdate() {
        GravityUpdate(Stage);
        GravityUpdate(backGroundStage,true);
    }

    /// <summary>
    /// Characterを落下させる処理（ステージ毎）
    /// </summary>
    /// <param name="stage"></param>
    /// <param name="isBack"></param>
    private void GravityUpdate(StageChip[,] stage,bool isBack = false) {
        //左下から右上へむけてループ
        for (int i = stage.GetLength(1) - 1; i >= 0; i--)
        {
            for (int j = 0; j < stage.GetLength(0); j++)
            {
                if (stage[j, i].holdCharacter == null)
                {
                    DropDown(j, i, isBack);
                }
            }
        }
    }

    /// <summary>
    /// 指定された位置の上にCharacterがいた場合、指定した位置にCharacterを落とす処理
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="isBack"></param>
    private void DropDown(int x,int y,bool isBack = false) {
        if (y == 0 && isBack) {
            return;
        }

        if (!isBack)
        {
            if (y == 0) {
                if (backGroundStage[x, backGroundStage.GetLength(1) - 1].holdCharacter)
                {
                    backGroundStage[x, backGroundStage.GetLength(1) - 1].holdCharacter.transform.SetParent(CharacterParent.transform);
                    Stage[x, y].MoveCharacter(backGroundStage[x, backGroundStage.GetLength(1) - 1]);
                }
                else {
                    DropDown(x, backGroundStage.GetLength(1) - 1, true);
                }
            }
            else if (Stage[x, y - 1].holdCharacter)
            {
                Stage[x, y].MoveCharacter(Stage[x, y - 1]);
                return;
            }
            else
            {
                DropDown(x, y - 1);
                return;
            }
        }

        if (isBack) {
            if (backGroundStage[x, y - 1].holdCharacter) {
                if (y == backGroundStage.GetLength(1)) {
                    backGroundStage[x, y].MoveCharacter(Stage[x, 0]);
                }
                else
                    backGroundStage[x, y].MoveCharacter(backGroundStage[x, y - 1]);
            }
            else {
                DropDown(x, y - 1, true);
            }
        }
    }

    /// <summary>
    /// 指定したマスの下にマスがあるか確認
    /// </summary>
    /// <returns></returns>
    private bool isDownSideChip(int x,int y) {
        return (y < Stage.GetLength(1) - 1);
    }

    /// <summary>
    /// 更新の予約
    /// </summary>
    public void SetUpdate() {
        updateFlag = true;
    }

    public void RandDelete(int num)
    {
        for (int i = 0; i < num; i++)
            DeleteCharacter(Stage[Random.Range(0, Stage.GetLength(0)), Random.Range(0, Stage.GetLength(1))].holdCharacter);
    }

	public StageChip GetStageChip(GameObject checkMapObj)
	{
		StageChip output = null;
		foreach (StageChip _output in Stage) {
			if (_output.holdCharacter.gameObject == checkMapObj) {
				output = _output;
				break;
			}
		}

		return output;
	}

	public void SetStageChip(StageChip _stageChip)
	{
		foreach (StageChip _output in Stage) {
			if (_output.gameObject == _stageChip.gameObject) {
				//_output = _stageChip;
				break;
			}
		}
	}

	public void CreateBeefjarkey(GameObject checkMapObj)
	{
		StageChip _stageChip;
		_stageChip = GetStageChip (checkMapObj);
		Debug.Log (_stageChip.holdCharacter);
		DeleteCharacter(_stageChip.holdCharacter);

        Character beef = Instantiate(beefjarkeyPrefab);//CharacterManager2.Instance.CreateCharacter ((int)DropType.jaki);
        _stageChip.AddCharacter(beef, true, 0.8f);
        //Debug.Log(_stageChip.character + ":" + _stageChip.character.gameObject.activeInHierarchy);
		beef.transform.SetParent (CharacterParent.transform);
		//SetStageChip (_stageChip);
	}
}
