using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInput : MonoBehaviour {

	int numX,numY;
	bool InputFlg,ChangeFlg;

	CharacterManager CharaManager;
	List<CharacterData> SaveObj;

	// Use this for initialization
	void Start () {
		InputFlg = ChangeFlg = false;
		SaveObj = new List<CharacterData> ();
		CharaManager = GameObject.Find ("CharacterManager").GetComponent<CharacterManager> ();
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
						for (int i = 0; i < CharaManager.CharacterInstance.GetLength (0); i++) {
							for (int j = 0; j < CharaManager.CharacterInstance.GetLength (1); j++) {
								if (CharaManager.CharacterInstance [i, j].m_CharacterSprite.GetInstanceID () == l_hit.collider.gameObject.GetInstanceID ()) {
									SaveObj.Clear ();
									SaveObj.Add (CharaManager.CharacterInstance [i, j]);
									numX = i;
									numY = j;

									InputFlg = true;
									break;
								} 
							}
							if (InputFlg)
								break;
						}
					} else {
						if (SaveObj.Count != 0 && !ChangeFlg) {
							if (SaveObj [0].m_CharacterSprite.GetInstanceID () != l_hit.collider.gameObject.GetInstanceID ()) {
								for (int i = 0; i < CharaManager.CharacterInstance.GetLength (0); i++) {
									for (int j = 0; j < CharaManager.CharacterInstance.GetLength (1); j++) {
										if (CharaManager.CharacterInstance [i, j].m_CharacterSprite.GetInstanceID () == l_hit.collider.gameObject.GetInstanceID ()) {
											CharaManager.DirectionObjMove (numX, numY, i, j);

											Debug_CharacterDataLog ();
											CharaManager.CombinationSearch (i, j);
											//CharaManager.search (SaveObj [0].m_SpriteNum, ref SaveObj, i, j);
											//CharaManager.search (CharaManager.CharacterInstance [numX, numY].m_SpriteNum, ref SaveObj, numX, numY);
											CharaManager.CombinationSearch (numX, numY);
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
			numX =numY = 0;
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
