using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージに配置されるマスClass
/// </summary>
public class StageChip : MonoBehaviour {
    public Vector2 position { get { return transform.position; } }
    public Character stayCharacter { get { return _character; } }
    private Character _character;

    private void Update()
    {
        if (_character) {

        }
    }

    /// <summary>
    /// このマスからCharacterを出す処理
    /// </summary>
    private void RemoveCharacter() {

    }

    /// <summary>
    /// このマスにCharacterを追加する処理
    /// </summary>
    private void AddCharacter() {

    }

    /// <summary>
    /// このマスから他のマスにCharacterを動かす処理
    /// </summary>
    /// <param name="target"></param>
    public void MoveCharacter(StageChip target) {

    }
}
