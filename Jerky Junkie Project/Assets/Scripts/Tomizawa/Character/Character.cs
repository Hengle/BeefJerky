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
    public bool isChecked;
    

    public void Init(CharacterData2 data) {
        this.data = data;
        //this.data.m_CharacterSprite = gameObject;
        GetComponent<Image>().sprite = data.m_CharacterSprite;
    }

    public void MoveStart(Vector2 pos,bool isInit = false, float moveEndWait = 0) {
        if (gameObject.activeInHierarchy) {
            if (isInit) transform.position = pos;

            if (move != null) StopCoroutine(move);
            move = StartCoroutine(MoveTo(pos, moveEndWait)); //StartCoroutine(MoveTo(_character.gameObject, position));
            Debug.Log(gameObject + ":" + (move == null));
        }
        else transform.position = pos;
    }

    [SerializeField]
    private float moveEndWaitTime;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="moveEndWait">移動が終わるまでの最低時間 生成してすぐに消さない用</param>
    /// <returns></returns>
    private IEnumerator MoveTo(Vector2 pos,float moveEndWait = 0)
    {
        moveEndWaitTime += moveEndWait;
        StageManager.Instance.MoveStart();
        if (move != null) {
            StopCoroutine(move);
            StageManager.Instance.MoveEnd();
        }

        while (Vector2.Distance(transform.position, pos) > 0.5f)
        {
            if (Time.timeScale == 0 || StageManager.Instance.stopFlag) {
                yield return null;
                moveEndWaitTime -= Time.deltaTime;
                continue;
            }
            transform.position = Vector2.Lerp(transform.position, pos, 0.1f);
            yield return null;
            moveEndWaitTime -= Time.deltaTime;
        }

        while (moveEndWaitTime >= 0) {
            yield return null;
            moveEndWaitTime -= Time.deltaTime;
        }

        StageManager.Instance.MoveEnd();
        move = null;
        moveEndWaitTime = 0;
    }

    public void HitImage() {
        DropInput2.Instance.ObjectPointerEnter(this);
    }
}
