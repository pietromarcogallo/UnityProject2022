using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDfps : MonoBehaviour
{
    public static PlayerHUDfps istance = null;

    private void Awake()
    {
        if (istance == null)
            istance = this;
    }

    [SerializeField] private Scorefps scoreUI;

    public void UpdateScoreAmount()
    {
        scoreUI.AddToScore();
    }

    public void LessdateScoreAmount()
    {
        scoreUI.SubtractFromScore();
    }
}
