using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    //Acccess the CanvasGroup
    public CanvasGroup fadeGroup;

    //Bools to control action
    public bool fadeIn = false;
    public bool fadeOut = false;
    //float to control speed
    [Range(0.0f, 2.0f)]
    public float fadeSpeedRange; 

    public void ShowUI()
    {
        fadeIn = true;
    }

    public void HideUI()
    {
        fadeOut = true;
    }

    private void Update()
    {
        if (fadeIn)
        {
            if(fadeGroup.alpha < 1)
            {
                fadeGroup.alpha += Time.deltaTime * fadeSpeedRange;
                if(fadeGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (fadeGroup.alpha >= 0)
            {
                fadeGroup.alpha -= Time.deltaTime * fadeSpeedRange;
                if (fadeGroup.alpha <= 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

}
