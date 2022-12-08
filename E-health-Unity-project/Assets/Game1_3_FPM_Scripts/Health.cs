using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int numHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int GetHealth()
    {
        return health;
    }

    public void AffectHealth()
    {
        health--;
        Update();
    }

    private void Update()
    {
        if (health > numHearts)
            health = numHearts;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < health ? fullHeart : emptyHeart;
            hearts[i].enabled = i < numHearts;
        }
    }
}
