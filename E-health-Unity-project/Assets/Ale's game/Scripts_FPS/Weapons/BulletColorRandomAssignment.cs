using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColorRandomAssignment : MonoBehaviour
{
    public Material[] randomMaterialColor;
    public GameObject[] coloredWalls;
    void Start()
    {
        int[] indMaterial = new int[randomMaterialColor.Length];
        for (int i = 0; i < randomMaterialColor.Length; i++)
        {
            indMaterial[i] = 0; // not assigned material
        }

        foreach (GameObject wall in coloredWalls)
        {
            int num = Random.Range(0, randomMaterialColor.Length);
            while (indMaterial[num] != 0)
            {
                num++;
                if (num >= randomMaterialColor.Length)
                    num = 0;
            }

            indMaterial[num] = 1;
            wall.GetComponent <Renderer>().material = randomMaterialColor[num];
        }

    }
}
