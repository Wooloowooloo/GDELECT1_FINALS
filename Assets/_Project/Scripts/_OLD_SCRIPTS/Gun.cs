using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider boxColl;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public static bool equipped;
    public static bool slotfull;

    public Transform firePoint;
    public float range;

    public int maxTotalBullets;
    public int maxClipBullets;

    public LayerMask targetLayer = new();

    [HideInInspector] public int currentTotalBullets;
    [HideInInspector] public int currentClipBullets;

    void Start()
    {
        //Setup
        if (!equipped)
        {
            rb.isKinematic = false;
            boxColl.isTrigger = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            boxColl.isTrigger = true;
            slotfull = true;
        }

        currentClipBullets = maxClipBullets;
        currentTotalBullets = maxTotalBullets;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToP = player.position - transform.position;

        if (!equipped && distanceToP.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotfull)
            PickUp();

        // Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q))
            Drop();

        if (Input.GetKeyDown(KeyCode.Mouse0) && equipped == true)
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.R) && equipped == true)
        {
            Reload();
        }

        if (currentTotalBullets < 0)
        {
            currentTotalBullets = 0;
        }
    }
    private void PickUp()
    {
        equipped = true;
        slotfull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody Kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        boxColl.isTrigger = true;
    }

    private void Drop()
    {
        equipped = false;
        slotfull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        boxColl.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
    }

    private void Shoot()
    {
        Vector2 screenCentrePoint = new(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentrePoint);

        if (currentClipBullets > 0)
        {
            currentClipBullets--;
            currentTotalBullets--;

            if (Physics.Raycast(ray, out RaycastHit hitInfo, range, targetLayer))
            {
                /*
                Target target = hitInfo.transform.gameObject.GetComponent<Target>();

                Debug.Log(target.transform.name);
                target.TargetHit();*/

                if (hitInfo.transform.gameObject != null)
                {
                    hitInfo.transform.gameObject.GetComponent<OLD_Target>().TargetHit();
                }
                else
                {
                    Debug.Log("nothing");
                }
            }
        }
        else
        {
            currentClipBullets = 0;
        }
        Debug.Log(currentTotalBullets);
    }

    private void Reload()
    {
        int reloadAmount = maxClipBullets - currentClipBullets;
        reloadAmount = (currentTotalBullets - reloadAmount) >= 0 ? reloadAmount : currentTotalBullets;
        currentClipBullets += reloadAmount;
        currentTotalBullets -= reloadAmount;
    }
}