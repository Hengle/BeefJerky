using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// パズルで消されるキャラクターClass
/// </summary>
public class Character : MonoBehaviour {
    public Coroutine move;
    public bool isMove { get { return move == null; } }

    public CharacterData2 data;
    

    public void Init(CharacterData2 data) {
        this.data = data;
        //this.data.m_CharacterSprite = gameObject;
        GetComponent<Image>().sprite = data.m_CharacterSprite;
    }

    public void MoveStart(Vector2 pos) {
        if (move != null) StopCoroutine(move);
        if (gameObject.activeInHierarchy)
            move = StartCoroutine(MoveTo(pos)); //StartCoroutine(MoveTo(_character.gameObject, position));
        else transform.position = pos;
    }

    private IEnumerator MoveTo(Vector2 pos)
    {
        StageManager.Instance.MoveStart();
        if (move != null) {
            StopCoroutine(move);
            StageManager.Instance.MoveEnd();
        }

        while (Vector2.Distance(transform.position, pos) > 0.5f)
        {
            if (Time.timeScale == 0 || StageManager.Instance.stopFlag) {
                yield return null;
                continue;
            }
            transform.position = Vector2.Lerp(transform.position, pos, 0.1f);
            yield return null;
        }
        StageManager.Instance.MoveEnd();
        move = null;
    }

    public void HitImage() {
        DropInput2.Instance.ObjectPointerEnter(this);
    }
}
