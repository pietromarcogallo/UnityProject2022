using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultColorBulletAssignment : MonoBehaviour
{
    public Material[] materialColor;
    public GameObject[] Walls;
    public Transform[] Spawn;
    void Start()
    {
        int[] indMaterial = new int[materialColor.Length];
        for (int i = 0; i < materialColor.Length; i++)
        {
            Walls[i].GetComponent <Renderer>().material = materialColor[i];
        }
    }
}
