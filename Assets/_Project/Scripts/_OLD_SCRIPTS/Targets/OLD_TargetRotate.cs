using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_TargetRotate : MonoBehaviour
{
    public GameObject standingTarget;
    public OLD_ScoreSystem scoreSystem;

    [HideInInspector] public bool isHit;
    [HideInInspector] public float minDelayRate;
    [HideInInspector] public float maxDelayRate;
    [HideInInspector] public float rotationSpeed;
    private float timer;
    private bool respawning;

    private Vector3 drop = new(90, 0, 0);
    private Vector3 rise = Vector3.zero;

    void Start()
    {
        timer = 0.0f;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (isHit || timer > Random.Range(minDelayRate, maxDelayRate)) 
        {
            Drop();
        }

        if(respawning == true && timer > Random.Range(minDelayRate, maxDelayRate))
        {
            Rise();
        }
    }

    void Drop()
    {
        //transform.LeanRotate(drop, rotationSpeed * Time.deltaTime);
        standingTarget.GetComponent<Collider>().enabled = false;

        timer = 0;
        respawning = true;
    }

    void Rise()
    {
        //transform.LeanRotate(rise, rotationSpeed * Time.deltaTime);
        standingTarget.GetComponent<Collider>().enabled = true;

        timer = 0;
        respawning = false;
    }
}
