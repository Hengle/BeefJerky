using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectAnimation : MonoBehaviour {

	int num;

	[SerializeField]
	float ImageMoveSpeed;

	Image effectImage;
	[SerializeField]
	Sprite[] effectSprites;

	Animation anim;

	// Use this for initialization
	void Start () {
		effectImage = GetComponent<Image> ();
		effectImage.sprite = effectSprites[0];
		num = 0;
		//StartCoroutine (SmoothTextureAnimation());

		anim = GetComponent<Animation> ();
		anim.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator SmoothTextureAnimation()
	{
		for (int i = 0; i < effectSprites.Length; i++) {
			yield return new WaitForSeconds (ImageMoveSpeed);

			effectImage.sprite = effectSprites [i];
		}

	}

}
