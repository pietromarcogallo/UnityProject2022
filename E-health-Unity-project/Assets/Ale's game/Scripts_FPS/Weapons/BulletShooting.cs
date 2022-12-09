using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    // bullet
    public GameObject bullet;
    
    // bullet force
    public float shootForce;
    
    // gun stats
    public float timeBetweenShooting, timeBetweenShots;
    
    // bools
    private bool shooting, readyToShoot;
    
    // reference
    public Camera fpsCam;
    public Transform attackPoint;
    
    // animation and sound
    // public GameObject plasmaPistol;
    public AudioSource plasmaShot;

    public Transform hologramPoint;
    
    public bool allowInvoke = true;
private void Awake()
    {
        readyToShoot = true;
    }

private void Start()
{
    Instantiate(bullet, hologramPoint.position, hologramPoint.rotation, gameObject.transform);
}

private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        // taking input
        shooting = Input.GetKeyDown(KeyCode.Mouse0);
        
        //shooting
        if (readyToShoot && shooting)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        
        // find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // ray through the middle of the screen
        RaycastHit hit;
        
        // check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); // default point if nothing is hit
        //Debug.Log(hit.transform.name);
        
        // calculate direction from attackPoint to targetPoint
        Vector3 direction = targetPoint - attackPoint.position;
        
        // instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, hologramPoint.rotation); //store instatiated bullet
        currentBullet.transform.forward = direction.normalized;
        
        // add force to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);
        
        gameObject.GetComponent<Animator>().Play("FireGun");
        plasmaShot.Play();
        
        // invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
        gameObject.GetComponent<Animator>().Play("New State");
        
    }
}
