﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    public Image ImgHealthBar;
    public Text TxtHealth;
    public int Min;
    public int Max;
    private int mCurrentValue;

    private float mCurrentPercent;

    private void Awake()
    {
        mCurrentValue = GetComponent<Player>().playerCurrentHealth;
    }

    public void SetHealth(int health)
    {
        if (health != mCurrentValue)
        {
            if (Max - Min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = health;
                mCurrentPercent = mCurrentValue / (Max - Min);
            }

            TxtHealth.text = string.Format("{0} %", Mathf.RoundToInt(mCurrentPercent * 100));
            ImgHealthBar.fillAmount = mCurrentPercent;
        }
    }

    public float CurrentPercent
    {
        get { return mCurrentPercent; }
    }

    public int CurrentValue
    {
        get { return mCurrentValue; }
    }
	// Use this for initialization
	void Start ()
    {
        SetHealth(Max);	
	}

    void Update()
    {
        //SetHealth(
    }
}
