using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージに配置されるマスClass
/// </summary>
public class StageChip : MonoBehaviour {
    public Vector2 position { get { return transform.position; } }
    public Character stayCharacter { get { return _character; } }
    [SerializeField]
    private Character _character;

    private void Update()
    {
        if (_character) {
            StageManager.Instance.SetUpdate();
        }
    }

    /// <summary>
    /// このマスからCharacterを出す処理
    /// </summary>
    public Character RemoveCharacter() {
        Character character = _character;
        _character = null;
        return character;
    }

    /// <summary>
    /// このマスにCharacterを追加する処理
    /// </summary>
    public void AddCharacter(Character target, bool isInit = false) {
        _character = target;
        _character.transform.SetParent(transform);
        if (isInit)
            _character.transform.localPosition = Vector2.zero;
        else
            _character.MoveStart(position);
    }

    /// <summary>
    /// このマスから他のマスにCharacterを動かす処理
    /// </summary>
    /// <param name="targetChip"></param>
    public void MoveCharacter(StageChip targetChip) {
        AddCharacter(targetChip.RemoveCharacter());
        //Debug.Log(Vector3.Lerp(_character.transform.position, targetChip.position, 0.5f));
    }

    


}
