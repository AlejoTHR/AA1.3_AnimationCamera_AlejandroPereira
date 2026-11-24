using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericGun : MonoBehaviour
{
    [Header("Weapon")]
    public int clipMax = 30;
    public int clipCurrent = 30;
    public bool automatic = true;
    [Min(1f/60f)]
    public float fireRate = 0.1f;
    public float reloadTime = 0.5f;
    [Header("Firing")]
    public UnityEvent onFire;
    public Transform firePoint;
    public GameObject bullet;
    [Header("Animation")]
    public float positionRecover;
    public float rotationRecover;
    public Vector3 knockbackPosition;
    public Vector3 knockbackRotation;
    Vector3 originalPosition;
    Quaternion originalRotation;


    // ADDED //
    InputSystem_Actions input;
    public float TimerShoot;
    public float TimerReload;


    private void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        // INPUT
        input = new InputSystem_Actions();
        input.Enable();
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, positionRecover * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, rotationRecover * Time.deltaTime);



        // IS AUTOMATIC
        if (!automatic)
        {
            // SHOOT ONE BY ONE
            if (input.Player.Shoot.WasPerformedThisFrame())
            {
                Fire();
            }
        }

        // IS NOT AUTOMATIC
        else
        {
            if (input.Player.Shoot.IsPressed())
            {
                // WHILE IS PRESSED, ADD TO TIMER
                TimerShoot += Time.deltaTime;

                // IF TIMER MEETS FIRE RATE, SHOOT AND RESET TIMER
                if (TimerShoot >= fireRate)
                {
                    Fire();
                    TimerShoot = 0;
                }
            }
            // IF IS RELEASED, RESET TIMER
            else if (input.Player.Shoot.WasCompletedThisFrame()) 
            {
                TimerShoot = 0;
            }
        }

        // IF NO AMMO LEFT
        if(clipCurrent == 0)
        {
            // RELOAD TIME
            TimerReload += Time.deltaTime;
            // WHEN RELOAD TIME IS DONE
            if(TimerReload >= reloadTime)
            {
                clipCurrent = clipMax; // REFILL CLIP
                TimerReload = 0; // RESET TIMER
            }

        }

    }
    public void Fire()
    {
        // NEEDS AMMO TO SHOOT
        if(clipCurrent > 0)
        {
            clipCurrent--;
            Destroy(Instantiate(bullet, firePoint.position, firePoint.rotation), 10);
            StartCoroutine(Knockback_Corutine());
        }

    }
    IEnumerator Knockback_Corutine()
    {
        yield return null;
        transform.localPosition -= new Vector3(Random.Range(-knockbackPosition.x, knockbackPosition.x), Random.Range(0, knockbackPosition.y), Random.Range(-knockbackPosition.z, -knockbackPosition.z * .5f));
        transform.localEulerAngles -= new Vector3(Random.Range(knockbackRotation.x * 0.5f, knockbackRotation.x), Random.Range(-knockbackRotation.y, knockbackRotation.y), Random.Range(-knockbackRotation.z, knockbackRotation.z));
    }
}
