using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    //Attack
    public float chargeStrength = 10000f, chargeSpeed = 0f, chargeSpeedMax = 15000f;
    public float impulseSpeed = 5000f;
    
    public Transform gun;
    public Rigidbody bullet;

    public float maxBulletsNum = 200f, bulletNums = 200f, pickUpBulletsSpeed = 20f;
    public bool isPickUpBullets;

    public bool isImpulseGun, isChargeGun, isContinueGun, isMachineGun;

    public float machineGunCount, machineGunPeriod = 0.1f, continueGunBulletConsumeSpeed = 10f ;

    public Rigidbody playerRb;
    public float pushbackForce = 100f, pushbackForceChangeVelocity = 1500f, pushbackForceAddForce = 100f;

    public int killNumber = 0;
    

    public void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        machineGunCount = 0;
    }

    private void Update()
    {
        if (isPickUpBullets)
        {
            PickUpBullets();
        }
    }

    public void PrepareAttack()
    {
        if (isChargeGun)
        {
            if (chargeSpeed < chargeSpeedMax)
            {
                chargeSpeed += Time.deltaTime * chargeStrength;
            }
        }
    }

    public void Attack()
    {

        if (isChargeGun)
        {
            if (bulletNums > 0)
            {
                var shootObj = Instantiate(bullet, gun.position, gun.rotation);
                shootObj.AddForce (gun.forward * chargeSpeed);
                PushSelfBack();
                LoseBullet();
                chargeSpeed = 0f;
                print(bulletNums + " bullets remain");
            }
            else
            {
                print("No bullet, need reload!!!");
            }
        }

        if (isMachineGun)
        {
            if (bulletNums > 0)
            {
                machineGunCount -= Time.deltaTime;
                if (machineGunCount < 0)
                {
                    var shootObj = Instantiate(bullet, gun.position, gun.rotation); 
                    shootObj.AddForce (gun.forward * impulseSpeed * 3f);
                    PushSelfBack(); 
                    LoseBullet();
                    machineGunCount = machineGunPeriod;
                    print(bulletNums + " bullets remain");
                }
            }
            else
            {
                print("No bullet, need reload!!!");
            }
            
        }

        if (isImpulseGun)
        {
            if (bulletNums > 0)
            {   var shootObj = Instantiate(bullet, gun.position, gun.rotation);
                shootObj.AddForce (gun.forward * impulseSpeed);
                PushSelfBack(); 
                LoseBullet();
                print(bulletNums + " bullets remain");
            }
            else
            {
                print("No bullet, need reload!!!");
            }
        }

        if (isContinueGun)
        {
            if (bulletNums > 0)
            {   var shootObj = Instantiate(bullet, gun.position, gun.rotation);
                shootObj.AddForce (gun.forward * impulseSpeed);
                
                //open the trigger box
                //trigger box length increase, can be increase to the max
                //addforce to the rigidbody in the trigger box
                
                PushSelfBack(); 
                LoseBullet();
                print(bulletNums + " bullets remain");
            }
            else
            {
                print("No bullet, need reload!!!");
            }
        }

    }
    
    
    private void LoseBullet()
    {
        if (bulletNums > 0)
        {
            if (isContinueGun)
            {
                bulletNums -= (Time.deltaTime * continueGunBulletConsumeSpeed);
            }
            else
            {
                bulletNums--;
            }
        }
    }
    
    private void PushSelfBack()
    {
        //change pushback force based on physical movement mode
        if (gameObject.GetComponent<PlayerController>().isChangeVelocityMove)
        {
            pushbackForce = pushbackForceChangeVelocity;
        }
        else if (gameObject.GetComponent<PlayerController>().isAddForceMove)
        {
            pushbackForce = pushbackForceAddForce;
        }
        
        //add pushback force modifier num based on gun type
        if (isChargeGun)
        {
            playerRb.AddForce(-gun.forward * pushbackForce * chargeSpeed * 0.00015f, ForceMode.Impulse);
        }
        if (isMachineGun)
        {
            playerRb.AddForce(-gun.forward * pushbackForce * 0.7f, ForceMode.Impulse);
        }
        else
        {
            playerRb.AddForce(-gun.forward * pushbackForce, ForceMode.Impulse);
        }

        print("push back self");
    }

    private void PickUpBullets()
    {
        if (bulletNums < maxBulletsNum)
        {
            bulletNums += Time.deltaTime * pickUpBulletsSpeed; 
        }
        
    }
}
