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

	int nowX,nowY;						//!<　ドロップを選択した時のドロップの二次元配列位置を保存する変数
	bool InputFlg,ChangeFlg;			//!<　一回しか処理をさせないストッパー変数
	bool comboFlg,stopper;

	CharacterData comboSaveObj;			//!<　削除するドロップを格納するリスト
	CharacterManager CharaManager;		//!<　CharaManagerインスタンス
	[SerializeField]
	List<CharacterData> SaveObj;		//!<　削除するドロップを格納するリスト

	// Use this for initialization
	void Start () {
		InputFlg = ChangeFlg = false;
		SaveObj = new List<CharacterData> ();
		CharaManager = GameObject.Find ("CharacterManager").GetComponent<CharacterManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButton (0)) {
			RayhitDrop ();

		}

		if (Input.GetMouseButtonUp (0)) {
			InputFlg = ChangeFlg = false;
			comboFlg = stopper = false;
			nowX =nowY = 0;
			if (SaveObj.Count >= 4) {
				CharaManager.RootDestoryInstance (ref SaveObj);
			}

			SaveObj.Clear ();
		}
//		if (Input.GetMouseButtonUp (1)) {
//			CharaManager.RootDestoryInstance (ref SaveObj);
//			SaveObj.Clear ();
//			nowX =nowY = 0;
//		}

		//Debug.Log (SaveObj.Count);
	}

	public void RayhitDrop()
	{
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
							if (l_hit.collider.gameObject.GetInstanceID () == CharaManager.CharacterInstance [i, j].m_CharacterSprite.GetInstanceID ()) {
								// ヒットオブジェクトを格納
								//SaveObj.Clear ();

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
						CharacterData findSaveObj = SaveObj.Find (x => x.m_CharacterSprite.GetInstanceID () == l_hit.collider.gameObject.GetInstanceID ());
						if (findSaveObj.m_CharacterSprite != null) {
							CheckSaveObj (l_hit);
							return;
						}

						// ドロップの二次元配列の探索 
						for (int i = 0; i < CharaManager.CharacterInstance.GetLength (0); i++) {
							for (int j = 0; j < CharaManager.CharacterInstance.GetLength (1); j++) {

								// ヒットしたオブジェクトと一致していれば実行
								if (l_hit.collider.gameObject.GetInstanceID () == CharaManager.CharacterInstance [i, j].m_CharacterSprite.GetInstanceID ()) {
									// コンボ中に削除対象から外れてしまったときの処理
									if (stopper) {
										if (l_hit.collider.gameObject.GetInstanceID () == SaveObj [SaveObj.Count - 1].m_CharacterSprite.GetInstanceID ()) {
											stopper = false;
										}

									} else {
										
										// 削除対象オブジェクトを選択して入れば実行
										if (CharaManager.getObjFlg (i, j)) {
											
											SaveObj.Add (CharaManager.CharacterInstance [i, j]);
											comboFlg = true;

										} 
										// コンボ中に削除対象から外れてしまったら実行
										else if (!CharaManager.getObjFlg (i, j) && comboFlg) {
											if (SaveObj.Count >= 4) {
												CharaManager.RootDestoryInstance (ref SaveObj);
											}

											SaveObj.Clear ();

											stopper = true;
										} 
										// 削除対象以外を選択時実行
										else if (!CharaManager.getObjFlg (i, j) && !comboFlg) {

											CharaManager.DirectionObjMove (nowX, nowY, i, j);

											//Debug_CharacterDataLog ();
											//CharaManager.CombinationSearch (i, j);
											//CharaManager.CombinationSearch (nowX, nowY);

											//CharaManager.search (SaveObj [0].m_SpriteNum, ref SaveObj, i, j);
											//CharaManager.search (CharaManager.CharacterInstance [numX, numY].m_SpriteNum, ref SaveObj, numX, numY);

											ChangeFlg = true;
										}


										//CheckSaveObj (l_hit);
										break;
									}

								}
								if (ChangeFlg)
									break;
							}
						}

					}
				}

				//Debug.Log (l_hit.collider.gameObject.name);
			}
		}
	}

	void CheckSaveObj(RaycastHit2D l_hit)
	{
		Debug.Log ("CheckSaveObj");
//		if (l_hit.collider.gameObject.GetInstanceID () == SaveObj [SaveObj.Count - 1].m_CharacterSprite.GetInstanceID ()) {
//			stopper = false;
//		}

		// 保存されているオブジェクトの数が2つあれば実行
		if (SaveObj.Count >= 2) {
			// 最後のほうから探索

			int num = SaveObj.FindIndex (x => x.m_CharacterSprite == l_hit.collider.gameObject);
			SaveObj.RemoveRange (num + 1, SaveObj.Count - (num + 1));

					/*for (int j = i ; i < SaveObj.Count; i++) {
						SaveObj.RemoveAt (j);
					}*/

					Debug.Log ("hid");
				

		}
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
