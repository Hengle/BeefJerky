using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージのマネージャーClass
/// </summary>
public abstract class StageManager : SingletonMonoBehaviour {
    public StageChip[][] Stage;//ステージ配列
    [SerializeField]
    private int x, y;//Stageの大きさ
    [SerializeField]
    private Vector2 StageSize;//ステージ枠の大きさ

    [System.Serializable]
    struct SpriteData
    {
        public Sprite sprite;
        private Vector2 _size;
        public Vector2 size
        {
            get
            {
                if (_size == Vector2.zero)
                {

                }
                return _size;
            }
        }
    }

    [SerializeField]
    private SpriteData[] data;

    // Use this for initialization
    void Start () {
        InitStage();
	}

    /// <summary>
    /// ステージの初期化・生成
    /// </summary>
    private void InitStage()
    {
        for (int i = 0; i < y; y++) {
            for (int j = 0; j < x; j++) {
                Stage[x][y] = StageChip.InitStageChip(x);
            }
        }
    }
}
