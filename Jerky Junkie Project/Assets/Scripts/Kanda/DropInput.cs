using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInput : MonoBehaviour {

	bool InputFlg,ChangeFlg;

	CharacterManager CharaManager;
	List<GameObject> SaveObj;

	// Use this for initialization
	void Start () {
		InputFlg = ChangeFlg = false;
		SaveObj = new List<GameObject> ();
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
									SaveObj.Add (CharaManager.CharacterInstance [i, j].m_CharacterSprite);
									InputFlg = true;
									break;
								} 
							}
						}
					} else {
						if (SaveObj.Count != 0 && !ChangeFlg) {
							if (SaveObj [0].GetInstanceID () != l_hit.collider.gameObject.GetInstanceID ()) {
								for (int i = 0; i < CharaManager.CharacterInstance.GetLength (0); i++) {
									for (int j = 0; j < CharaManager.CharacterInstance.GetLength (1); j++) {
										if (CharaManager.CharacterInstance [i, j].m_CharacterSprite.GetInstanceID () == l_hit.collider.gameObject.GetInstanceID ()) {
											CharaManager.DirectionObjMove (SaveObj [0], CharaManager.CharacterInstance [i, j].m_CharacterSprite);
											ChangeFlg = true;
										}
									}
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
			SaveObj.Clear ();
		}
	}
}
