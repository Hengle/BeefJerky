using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInput2 : SingletonMonoBehaviour<DropInput2> {
    int nowX, nowY;//現在選択中のマス位置
    bool InputFlg, ChangeFlg;

    List<GameObject> saveList = new List<GameObject>();

    private GameObject obj;
    public void ObjectPointerEnter(Character character) {
        obj = character.gameObject;
    }

    private GameObject GetMousePosObj() {
        return obj;
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (GetMousePosObj() != null) {
                //float l_maxDistance = 10f;
                //Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit2D l_hit = Physics2D.Raycast((Vector2)l_ray.origin, (Vector2)l_ray.direction, l_maxDistance);
                GameObject obj = GetMousePosObj();
                if (obj && obj.tag == ("Drop"))
                {
                    if (!InputFlg)
                    {
                        int[] num = GetCharacterNumber(obj);
                        if (num != null)
                        {
                            // ヒットオブジェクトを格納
                            saveList.Clear();
                            saveList.Add(StageManager.Instance.Stage[num[0], num[1]].stayCharacter.gameObject);
                            nowX = num[0];
                            nowY = num[1];

                            InputFlg = true;

                            Debug.Log("hit object is" + obj);
                        }
                    }
                    else
                    {
                        if (saveList.Count != 0 && !ChangeFlg)
                        {

                            // 他のマスに移動していれば(最初の入力で受け取ったオブジェクトと違うオブジェクトを選択していたら)実行 //ドロップの入れ替え処理
                            if (saveList[0] != obj)
                            {
                                int[] num = GetCharacterNumber(obj);
                                if (num != null)
                                {
                                    
                                    // ヒットしたオブジェクトと一致していれば実行
                                    if (StageManager.Instance.Stage[num[0], num[1]].stayCharacter.gameObject == obj)
                                    {
                                        Debug.Log(saveList[0] + " to " + obj);
                                        //StageManager.Instance.Stage[num[0], num[1]].stayCharacter.data;
                                        CharacterManager2.Instance.DirectionObjMove(nowX, nowY, num[0], num[1]);
                                        //CharaManager.DirectionObjMove(nowX, nowY, i, j);

                                        //Debug_CharacterDataLog();
                                        CharacterManager2.Instance.CombinationSearch(num[0], num[1]);
                                        //CharaManager.search (SaveObj [0].m_SpriteNum, ref SaveObj, i, j);
                                        //CharaManager.search (CharaManager.CharacterInstance [numX, numY].m_SpriteNum, ref SaveObj, numX, numY);
                                        //CharaManager.CombinationSearch(nowX, nowY);
                                        CharacterManager2.Instance.CombinationSearch(nowX, nowY);

                                        ChangeFlg = true;
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
            

        }
        if (Input.GetMouseButtonUp(0))
        {
            InputFlg = ChangeFlg = false;
            //SaveObj.Clear ();
        }
        if (Input.GetMouseButtonUp(1))
        {

            CharacterManager2.Instance.RootDestoryInstance(saveList);
            saveList.Clear();
            nowX = nowY = 0;
        }
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
                if (StageManager.Instance.Stage[i, j].stayCharacter.gameObject == target)
                {
                    return new int[] { i, j };
                }
            }
            if (ChangeFlg)
                break;
        }
        Debug.Log("none object");
        return null;
    }
    /**/
}
