using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パズルで消されるキャラクターClass
/// </summary>
public class Character : MonoBehaviour {
    public Coroutine move;
    public bool isMove { get { return move == null; } }

    public void MoveStart(Vector2 pos) {
        if (move != null) StopCoroutine(move);
        if (gameObject.activeInHierarchy)
            move = StartCoroutine(MoveTo(this, pos)); //StartCoroutine(MoveTo(_character.gameObject, position));
        else transform.position = pos;
    }

    private IEnumerator MoveTo(Character target, Vector2 pos)
    {
        if (target.move != null) StopCoroutine(target.move);

        while (target != null && Vector2.Distance(target.transform.position, pos) > 0.1f)
        {
            if (Time.timeScale == 0 || StageManager.Instance.stopFlag) {
                yield return null;
                continue;
            }
            target.transform.position = Vector2.Lerp(target.transform.position, pos, 0.1f);
            yield return null;
        }
    }
}
