using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInput2 : MonoBehaviour {
    /*
    int nowX, nowY;//現在選択中のマス位置
    bool InputFlg, ChangeFlg;

    List<GameObject> saveList = new List<GameObject>();

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {

            float l_maxDistance = 10f;

            Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D l_hit = Physics2D.Raycast((Vector2)l_ray.origin, (Vector2)l_ray.direction, l_maxDistance);

            if (l_hit.collider && l_hit.collider.CompareTag("Drop"))
            {
                if (!InputFlg)
                {
                    int[] num = GetCharacterNumber(l_hit.collider.gameObject);
                    if (num != null) {
                        // ヒットオブジェクトを格納
                        saveList.Clear();
                        saveList.Add(StageManager.Instance.Stage[num[0], num[1]].stayCharacter.gameObject);
                        nowX = num[0];
                        nowY = num[1];

                        InputFlg = true;
                    }
                }
                else
                {
                    if (saveList.Count != 0 && !ChangeFlg)
                    {

                        // 他のマスに移動していれば(最初の入力で受け取ったオブジェクトと違うオブジェクトを選択していたら)実行 //ドロップの入れ替え処理
                        if (saveList[0].GetInstanceID() != l_hit.collider.gameObject.GetInstanceID())
                        {
                            int[] num = GetCharacterNumber(l_hit.collider.gameObject);
                            if (num != null) {
                                // ヒットしたオブジェクトと一致していれば実行
                                if (CharaManager.CharacterInstance[i, j].m_CharacterSprite.GetInstanceID() == l_hit.collider.gameObject.GetInstanceID())
                                {
                                    CharaManager.DirectionObjMove(nowX, nowY, i, j);

                                    Debug_CharacterDataLog();
                                    CharacterManager2.Instance..CombinationSearch(i, j);
                                    //CharaManager.search (SaveObj [0].m_SpriteNum, ref SaveObj, i, j);
                                    //CharaManager.search (CharaManager.CharacterInstance [numX, numY].m_SpriteNum, ref SaveObj, numX, numY);
                                    CharaManager.CombinationSearch(nowX, nowY);

                                    ChangeFlg = true;
                                }
                            }
                        }
                    }
                }

                Debug.Log(l_hit.collider.gameObject.name);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            InputFlg = ChangeFlg = false;
            //SaveObj.Clear ();
        }
        if (Input.GetMouseButtonUp(1))
        {
            CharaManager.RootDestoryInstance(ref SaveObj);
            SaveObj.Clear();
            nowX = nowY = 0;
        }

        Debug.Log(SaveObj.Count);
    }

    /// <summary>
    /// 指定されたオブジェクトがマスに配置されているか確認する処理
    /// </summary>
    /// <param name="target"></param>
    /// <returns>マス番号　存在しなければnull</returns>
    public int[] GetCharacterNumber(GameObject target)
    {
        for (int i = 0; i < StageManager.Instance.Stage.GetLength(0); i++)
        {
            for (int j = 0; j < StageManager.Instance.Stage.GetLength(1); j++)
            {
                // ヒットしたオブジェクトと一致していれば実行
                if (StageManager.Instance.Stage[i, j].stayCharacter.GetInstanceID() == target.GetInstanceID())
                {
                    return new int[] { i, j };
                }
            }
            if (ChangeFlg)
                break;
        }
        return null;
    }
    */
}
