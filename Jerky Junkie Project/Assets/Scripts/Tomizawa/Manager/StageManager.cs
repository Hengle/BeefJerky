using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージのマネージャーClass
/// </summary>
public class StageManager : SingletonMonoBehaviour<StageManager> {
    public StageChip[,] Stage;//ステージ配列
    private StageChip[,] backGroundStage;//ステージの上に配置する落ちてくる用のChip置き場
    [SerializeField]
    private int x, y;//Stageの大きさ
    [SerializeField]//ステージ枠の大きさ ステージの中心位置
    private Vector2 StageSize,DefaultPosition;


    //[SerializeField]
    //private Sprite[] data;//使用するマス画像
    //[SerializeField]
    //private Vector2 spriteSize;//画像の基本サイズ
    [SerializeField]
    private StageChip StageChipPrefab;
    [SerializeField]
    private Vector2 chipSze,offset;
    [SerializeField]
    private List<Character> characterPrefabs;

    private bool updateFlag;

    // Use this for initialization
    void Start () {
        Stage = InitStage();
        Debug.Log(Stage.GetLength(0));
        backGroundStage = InitStage(true);
        CharacterInit(Stage);
        CharacterInit(backGroundStage);
        Debug.Log(backGroundStage[0,0].stayCharacter);
	}

    private void Update()
    {
        foreach (StageChip chip in backGroundStage) {
            if (chip.stayCharacter == null)
                chip.AddCharacter(InitCharacter());
        }
    }

    /// <summary>
    /// ステージの初期化・生成
    /// </summary>
    private StageChip[,] InitStage(bool isBack = false)
    {
        Vector2 InitStageSize = StageSize;
        StageSize.x += offset.x;//右端に余白
        StageChip[,] stage = new StageChip[x, y];
        ////ステージの左上の場所を取得
        //Vector2 leftUpSide = new Vector2(-StageSize.x / 2, StageSize.y / 2) + DefaultPosition;//ステージ左上のポジションを取得
        ////マスの大きさを取得
        //Vector2 scale = new Vector2((StageSize.x / ((chipSze.x) * x + offset.x)), StageSize.y / (chipSze.x * y + offset.y));
        //float res = StageSize.x / (chipSze.x * x + offset.x * (x - 1));
        //Debug.Log(res);
        //scale.x = res;

        Vector2 leftUpSide = new Vector2(-StageSize.x / 2, StageSize.y / 2) + DefaultPosition;
        Vector2 scale = new Vector2((InitStageSize.x / ((chipSze.x) * x + offset.x)), (InitStageSize.y / ((chipSze.y) * y + offset.y)));

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
                stage[j, i].transform.localPosition = pos;
                //次に配置するマスの位置を計算
                pos.x += chipSze.x * scale.x + offset.x;
                stage[j, i].transform.localScale = scale;
                stage[j, i].gameObject.SetActive(!isBack);
            }
            pos.y -= ((chipSze.x * scale.x)) + offset.y;
        }

        return stage;
    }

    /// <summary>
    /// Characterをステージ一杯に生成する処理
    /// </summary>
    /// <param name="stage"></param>
    private void CharacterInit(StageChip[,] stage) {
        foreach (StageChip chips in stage) {
            Character character = InitCharacter();
            chips.AddCharacter(character);
        }
    }

    /// <summary>
    /// 一番下の行のCharacterを全て削除する
    /// </summary>
    public void DeleteDownLineCharacter() {
        int line = Stage.GetLength(1) - 1;
        int length = Stage.GetLength(0);
        for (int i = 0; i < length; i++) {
            if (Stage[i, line].stayCharacter) {
                DeleteCharacter(Stage[i, line].stayCharacter);
            }
        }
    }

    /// <summary>
    /// Chaarcterを削除する処理
    /// </summary>
    /// <param name="target"></param>
    private void DeleteCharacter(Character target) {
        Destroy(target.gameObject);
    }

    /// <summary>
    /// Characterを生成する処理
    /// </summary>
    /// <returns></returns>
    private Character InitCharacter() {
        return Instantiate(characterPrefabs[0]);
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
                if (stage[j, i].stayCharacter == null)
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
            Debug.Log(y);
            if (y == 0) {
                if (backGroundStage[x, backGroundStage.GetLength(1) - 1].stayCharacter)
                {
                    Stage[x, y].MoveCharacter(backGroundStage[x, backGroundStage.GetLength(1) - 1]);
                }
                else {
                    DropDown(x, backGroundStage.GetLength(1) - 1, true);
                }
            }
            else if (Stage[x, y - 1].stayCharacter)
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
            Debug.Log(backGroundStage.GetLength(0) + ":" + backGroundStage.GetLength(1) + ";" + y);
            if (backGroundStage[x, y - 1].stayCharacter) {
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
}
