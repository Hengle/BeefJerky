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
    public CharactersData CharaData;
    [SerializeField]
    public List<Character> characterPrefabs { get{ return CharaData.CharacetrPrefab; } }
	[SerializeField]
    public Character beefjarkeyPrefab { get{ return CharaData.Jakis[0]; } }
    [SerializeField]
    private Character OzisanPrefab { get{ return CharaData.Ozisan; } }
    [SerializeField]
    private int OzisanCount;
    [SerializeField]
    private int score;//おじさんを消した数

    private bool updateFlag;
    public bool stopFlag;

	private float GravityWaitTime;

    
    public void MoveStart() {
        //isWaiting = true;
    }
    [SerializeField]
    private bool isMoveEnd = true;
    public void MoveEnd() {
        isMoveEnd = true;
    }


    // Use this for initialization
    void Start () {
        Stage = InitStage();
        backGroundStage = InitStage(true);
        CharacterInit(Stage);
        CharacterInit(backGroundStage,true);
        OzisanInit();
	}

    private void Update()
    {
        if (Time.timeScale == 0 || stopFlag) return;
        //キャラクターが削除された時など、必要な時のみ処理する
        if (updateFlag) {
			if(GravityWaitTime > 0)
			{
				GravityWaitTime -= Time.deltaTime;
				return;
			}
            updateFlag = false;
            GravityUpdate();
            
            foreach (StageChip chip in backGroundStage)
            {
                if (chip.holdCharacter == null)
                    chip.AddCharacter(InitRandCharacter(true));
            }
        }
        if (isMoveEnd || Input.GetMouseButtonDown(0)) {
            CombinationUpdate();
            isMoveEnd = false;
        }
    }

    //ステージに配置されているキャラクターの組み合わせ確認
    public void CombinationUpdate() {
        foreach (StageChip chip in Stage) {
            if (chip.character) chip.character.isChecked = false;
        }
        for (int i = 0; i < Stage.GetLength(0); i++) {
            for (int j = 0; j < Stage.GetLength(1); j++) {
                if (Stage[i,j].character && (Stage[i, j].character.data.m_DropType == DropType.ozisan || Stage[i, j].character.data.m_DropType == DropType.biru)) {
                    List<Character> characters = new List<Character>() { Stage[i, j].character };
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

    private void OzisanInit() {
        while (OzisanCount < ConstData.OZISAN_INSTANCE_MAX) {
            bool isBack = Random.Range(0, 2) == 0;
            int x = Random.Range(0, Stage.GetLength(0)), y = Random.Range(0, Stage.GetLength(1));
            StageChip targetChip = (isBack ? backGroundStage : Stage)[x, y];
            if (targetChip.holdCharacter.data.m_DropType != DropType.ozisan) {
                DeleteCharacter(targetChip.holdCharacter);
                targetChip.AddCharacter(InitCharacter(OzisanPrefab, isBack),true);
            }
        }
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
        switch (target.data.m_DropType)
        {
            case DropType.ozisan:
                OzisanCount--;
                StageManager.Instance.score++;
				HiScore.Instance.AddPoint(1);
                break;
        }
        Destroy(target.gameObject);
		GravityWaitTime = ConstData.Gravity_WaitTIme;
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
	private GameObject effectParent;
	public GameObject EffectParent
	{
		get
		{
			if (effectParent != null) return effectParent;
			else
			{
				effectParent = new GameObject("effectBox");
				effectParent.transform.SetParent(transform);
				return effectParent;
			}
		}
	}

	/// <summary>
	/// Characterを生成する処理
	/// </summary>
	/// <param name="isBack"></param>
	/// <param name="excepts">生成したくないキャラクターのタイプ</param>
	/// <returns></returns>
	private Character InitCharacter(bool isBack = false,params DropType[] excepts) {
        List<Character> list = new List<Character>(characterPrefabs);
        Character prefab;
        int i = 0;
        bool ReplaceFlag;
        do
        {
            prefab = list[Random.Range(0, list.Count)];
            ReplaceFlag = false;
            foreach (DropType t in excepts)
            {
                if (prefab.data.m_DropType == t)
                {
                    ReplaceFlag = true;
                    list.Remove(prefab);
                    break;
                }
            }
        }
        while (ReplaceFlag && i++ < 100);
        Debug.Log(i);
        
        return InitCharacter(prefab, isBack);
    }

    public Character InitCharacter(Character prefab,bool isBack = false) {
        Character chara = Instantiate(prefab);
        if (isBack){
            chara.transform.SetParent(CharacterBackParent.transform);
        }
        else
            chara.transform.SetParent(CharacterParent.transform);
        
        switch (chara.data.m_DropType) {
            case DropType.ozisan:
                OzisanCount++;
                CharacterManager2.Instance.AddTime(1);
                break;
        }
        return chara;
    }

    public Character InitRandCharacter(bool isBack = false) {
        //おじさんが少なければ、おじさんの数に応じた割合でおじさんを生成する
        if (Random.Range(0, ConstData.OZISAN_INSTANCE_MAX) > OzisanCount) {
            return InitCharacter(OzisanPrefab, isBack);
        }
        return InitCharacter(characterPrefabs[Random.Range(0, characterPrefabs.Count)], isBack);
    }

    public Character ChangeCharacterType(Character origin) {
        
        return origin;
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

	public StageChip GetStageChip(Character checkMapObj)
	{
		StageChip output = null;
		foreach (StageChip _output in Stage) {
			if (_output.holdCharacter && _output.holdCharacter == checkMapObj) {
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

	public void CreateBeefjarkey(Character character,int deleteJakiCount)
	{
        //StageChip _stageChip;
        StageChip stageChip = Stage[character.data.path[0], character.data.path[1]];// GetStageChip (character);
		DeleteCharacter(character);

        Character beef = null;
        if (deleteJakiCount >= 15) {
            beef = Instantiate(CharaData.Jakis[3]);
        }
        if (deleteJakiCount >= 11)
        {
            beef = Instantiate(CharaData.Jakis[2]);
        }
        else if (deleteJakiCount >= 7)
        {
            beef = Instantiate(CharaData.Jakis[1]);
        }
        else {
            beef = Instantiate(CharaData.Jakis[0]);
        }

        if (beef == null) return;
        stageChip.AddCharacter(beef, true, 1.0f);

        beef.transform.SetParent (CharacterParent.transform);
	}
}
