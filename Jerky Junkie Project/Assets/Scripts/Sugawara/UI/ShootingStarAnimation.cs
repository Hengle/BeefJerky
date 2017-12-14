using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarAnimation : MonoBehaviour {

	[SerializeField] private float animSpeed = 1.0f;

	[SerializeField] private GameObject[] starsObj = null;

	[SerializeField] private float intervalMax = 0.5f;

	[SerializeField] private float minX = 2.2f, maxX = 3.0f, minY = 1.0f, maxY = 4.5f;

	private float interval = 0.0f;
	private int currentNum = 0;

	private void Start()
	{
		for (int i = 0; i < starsObj.Length; i++)
		{
			starsObj[i].GetComponent<Animator>().speed = animSpeed;
		}
		starsObj[currentNum].transform.position = PositionReset();
		StartCoroutine("StarSet");
	}

	private IEnumerator StarSet()
	{
		for (int i = 0;i<starsObj.Length;i++) {
			yield return new WaitForSeconds(0.25f);
			starsObj[i].SetActive(true);
		}
	}

	private void Update()
	{
		interval += Time.deltaTime;
		if (interval > intervalMax)
		{
			interval = 0.0f;
			currentNum++;
			if (currentNum >= starsObj.Length)
				currentNum = 0;
			starsObj[currentNum].transform.position = PositionReset();
			starsObj[currentNum].SetActive(false);
			starsObj[currentNum].SetActive(true);
		}
	}

	private Vector3 PositionReset()
	{
		Vector3 pos;
		pos.x = Random.Range(minX,maxX);
		pos.y = Random.Range(minY,maxY);
		pos.z = -1.0f;

		return pos;
	}
}
