﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージに配置されるマスClass
/// </summary>
public class StageChip : MonoBehaviour {
    public int[] path;
    public Vector2 position { get { return transform.position; } }
    //このマスにいる（移動中でも）キャラクター
    public Character holdCharacter { get { return _character; } }
    //このマスに留まっている（移動中はnull）キャラクター
    public Character character {
        get {
            if (_character && _character.move != null)
                return null;
            return _character;
        }
    }
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
    public void AddCharacter(Character target, bool isInit = false,float moveEndWait = 0) {
        _character = target;
        //_character.transform.SetParent(transform);
        _character.data.path = new int[] { path[0], path[1] };
        /*
        if (isInit)
            _character.transform.position = position;
        else*/
        _character.MoveStart(position, isInit, moveEndWait);
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
