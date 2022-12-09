using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFireAnimationSound : MonoBehaviour
{
    public GameObject plasmaPistol;
    public bool isFiring = false;
    public AudioSource plasmaShot;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isFiring == false)
            {
                StartCoroutine(FireThePistol());
            }
        }

        IEnumerator FireThePistol()
        {
            isFiring = true;
            plasmaPistol.GetComponent<Animator>().Play("FireGun");
            plasmaShot.Play();
            yield return new WaitForSeconds(0.25f);
            plasmaPistol.GetComponent<Animator>().Play("New State");
            isFiring = false;
        }
    }
}
