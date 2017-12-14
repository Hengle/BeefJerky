using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeefjerkyAnimation : MonoBehaviour {
    bool startAnimation=false;

    public float startTimeMin =3f;
    public float startTimeMax =10f;
    float startTime = 11;
    public float endTime = 0.2f;
    private float startCTime = 0f;
    private float endCTime = 0;

    private Vector3 beefEulerAngles = new Vector3(0, 0,20);
    private RectTransform rTransform;

    private void Start()
    {
        rTransform = GetComponent<RectTransform>();
            RandomTime();
    }


    private void Update()
    {


        if (startAnimation)
        {
            rTransform.eulerAngles = beefEulerAngles;
            if (endCTime >= endTime)
            {
                startAnimation = false;
                endCTime = 0;
                rTransform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                endCTime += Time.deltaTime;
            }
        }
        else
        {
            if (startCTime >= startTime)
            {
                startCTime = 0;
                BeefjerkyAnimationStart();
            }
            else
            {
                startCTime += Time.deltaTime;
            }
        }
    }

    private void BeefjerkyAnimationStart()
    {
        startAnimation = true;
    }
    
    private void RandomTime()
    {
        startTime = Random.Range(startTimeMin, startTimeMax);
    }

}
