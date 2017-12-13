/**
 * @file   DropInput.cs
 * @brief  ドロップを触った時の制御
 * @author 神田　晃伸
 * @date   2017/12/13
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief  	  ドロップを触った時の制御
 * @details   パズルを交換するためにインプットを受け取る。パズルを交換する。
 */
public class DropInput : MonoBehaviour {

	int nowX,nowY;					//!<　ドロップを選択した時のドロップの二次元配列位置を保存する変数
	bool InputFlg,ChangeFlg;		//!<　一回しか処理をさせないストッパー変数

	CharacterManager CharaManager;	//!<　CharaManagerインスタンス
	List<CharacterData> SaveObj;	//!<　削除するドロップを格納するリスト

	// Use this for initialization
	void Start () {
		InputFlg = ChangeFlg = false;
		SaveObj = new List<CharacterData> ();
        CharaManager = GameObject.Find ("CharacterManager").GetComponent<CharacterManager> ();
        //CharaManager = CharacterManager2.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButton (0)) {

			float l_maxDistance = 10f;

			Ray l_ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D l_hit = Physics2D.Raycast ((Vector2)l_ray.origin, (Vector2)l_ray.direction, l_maxDistance);

			if (l_hit.collider) {
				if (l_hit.collider.CompareTag ("Drop")) {
					if (!InputFlg) {
						
						// ドロップの二次元配列の探索 
						for (int i = 0; i < CharaManager.CharacterInstance.GetLength (0); i++) {
							for (int j = 0; j < CharaManager.CharacterInstance.GetLength (1); j++) {
								// ヒットしたオブジェクトと一致していれば実行
								if (CharaManager.CharacterInstance [i, j].m_CharacterSprite.GetInstanceID () == l_hit.collider.gameObject.GetInstanceID ()) {
									// ヒットオブジェクトを格納
									SaveObj.Clear ();
									SaveObj.Add (CharaManager.CharacterInstance [i, j]);
									nowX = i;
									nowY = j;

									InputFlg = true;
									break;
								} 
							}
							if (InputFlg)
								break;
						}
					} else {
						if (SaveObj.Count != 0 && !ChangeFlg) {
							
							// 他のマスに移動していれば実行
							if (SaveObj [0].m_CharacterSprite.GetInstanceID () != l_hit.collider.gameObject.GetInstanceID ()) {
								// ドロップの二次元配列の探索 
								for (int i = 0; i < CharaManager.CharacterInstance.GetLength (0); i++) {
									for (int j = 0; j < CharaManager.CharacterInstance.GetLength (1); j++) {
										// ヒットしたオブジェクトと一致していれば実行
										if (CharaManager.CharacterInstance [i, j].m_CharacterSprite.GetInstanceID () == l_hit.collider.gameObject.GetInstanceID ()) {
											CharaManager.DirectionObjMove (nowX, nowY, i, j);

											Debug_CharacterDataLog ();
											CharaManager.CombinationSearch (i, j);
											//CharaManager.search (SaveObj [0].m_SpriteNum, ref SaveObj, i, j);
											//CharaManager.search (CharaManager.CharacterInstance [numX, numY].m_SpriteNum, ref SaveObj, numX, numY);
											CharaManager.CombinationSearch (nowX, nowY);
											Debug.Log (true);

											ChangeFlg = true;
											break;
										}
									}
									if (ChangeFlg)
										break;
								}
							}
						}
					}
				}

				Debug.Log (l_hit.collider.gameObject.name);
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			InputFlg = ChangeFlg = false;
			//SaveObj.Clear ();
		}
		if (Input.GetMouseButtonUp (1)) {
			CharaManager.RootDestoryInstance (ref SaveObj);
			SaveObj.Clear ();
			nowX =nowY = 0;
		}

		Debug.Log (SaveObj.Count);
	}

	public void Debug_CharacterDataLog(){
		for (int i = 0; i < CharaManager.CharacterInstance.GetLength (0); i++) {
			string log = "";
			for (int j = 0; j < CharaManager.CharacterInstance.GetLength (1); j++) {
				log += CharaManager.CharacterInstance[i,j].m_SpriteNum + "|";
			}
			Debug.Log (log);
		}
	}
}
