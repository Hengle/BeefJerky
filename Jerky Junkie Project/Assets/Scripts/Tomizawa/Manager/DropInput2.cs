using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInput2 : SingletonMonoBehaviour<DropInput2> {
    int nowX, nowY;//現在選択中のマス位置
    [SerializeField]
    bool InputFlg, ChangeFlg;
    bool comboFlg, stopper;

    [SerializeField]
    List<GameObject> saveList = new List<GameObject>();

    private GameObject obj;
    public void ObjectPointerEnter(Character character) {
        obj = character.gameObject;
    }

    private GameObject GetMousePosObj() {
        if (Input.GetMouseButton(0))
            return obj;
        else return null;
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RayhitDrop();
        }
        if (Input.GetMouseButtonUp(0))
        {
            InputFlg = ChangeFlg = comboFlg = stopper = false;
            nowX = nowY = 0;
            //SaveObj.Clear ();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //CharacterManager2.Instance.RootDestoryInstance(saveList);
            nowX = nowY = 0;

            if (saveList.Count >= 4)
            {
                CharacterManager2.Instance.RootDestoryInstance(saveList);
            }
            saveList.Clear();
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

    public void RayhitDrop() {
        if (GetMousePosObj() == null) return;
        
        GameObject obj = GetMousePosObj();
        if (obj.tag != ("Drop")) return;

        if (!InputFlg)
        {
            int[] num = GetCharacterNumber(obj);
            if (num != null)
            {
                // ヒットオブジェクトを格納
                //saveList.Clear();
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
                if (saveList[0] != obj)
                {

                    if (saveList.Count == 0 || ChangeFlg) return;
                    int[] num = GetCharacterNumber(obj);

                    // 他のマスに移動していれば実行
                    GameObject findSaveObj = saveList.Find(x => x == obj);
                    if (findSaveObj != null)
                    {
                        CheckSaveObj(findSaveObj);
                        return;
                    }

                    if (num != null)
                    {

                        // ヒットしたオブジェクトと一致していれば実行
                        if (StageManager.Instance.Stage[num[0], num[1]].stayCharacter.gameObject == obj)
                        {

                            GameObject hitObj = GetMousePosObj();
                            ///////
                            // コンボ中に削除対象から外れてしまったときの処理
                            if (stopper)
                            {
                                //最後に選択されたキャラクターを再び選択したら、処理を再開する
                                if (hitObj == saveList[saveList.Count - 1])
                                {
                                    stopper = false;
                                }
                            }
                            else
                            {

                                // 削除対象オブジェクトを選択していれば実行
                                if (CharacterManager2.Instance.getObjFlg(num[0], num[1]))
                                {
                                    saveList.Add(StageManager.Instance.Stage[num[0], num[1]].stayCharacter.gameObject);
                                    Debug.Log(obj);
                                    comboFlg = true;
                                }
                                // コンボ中に削除対象から外れてしまったら実行
                                else if (!CharacterManager2.Instance.getObjFlg(num[0], num[1]) && comboFlg)
                                {
                                    if (saveList.Count >= 4)
                                    {
                                        CharacterManager2.Instance.RootDestoryInstance(saveList);
                                    }

                                    saveList.Clear();

                                    stopper = true;
                                }
                                // 削除対象以外を選択時実行
                                else if (!CharacterManager2.Instance.getObjFlg(num[0], num[1]) && !comboFlg)
                                {

                                    CharacterManager2.Instance.DirectionObjMove(nowX, nowY, num[0], num[1]);

                                    //Debug_CharacterDataLog ();
                                    //CharaManager.CombinationSearch (i, j);
                                    //CharaManager.CombinationSearch (nowX, nowY);

                                    //CharaManager.search (SaveObj [0].m_SpriteNum, ref SaveObj, i, j);
                                    //CharaManager.search (CharaManager.CharacterInstance [numX, numY].m_SpriteNum, ref SaveObj, numX, numY);

                                    ChangeFlg = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void CheckSaveObj(GameObject target)
    {
        // 保存されているオブジェクトの数が2つあれば実行
        if (saveList.Count >= 2) {
            // 最後のほうから探索
            int num = saveList.FindIndex(x => x == target);
            saveList.RemoveRange(num + 1, saveList.Count - (num + 1));
        }
    }

    /**/
}
