using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShortcutManagement;
using UnityEngine;

public class OLD_Target : MonoBehaviour
{
    public enum TargetType
    {
        DoHit,
        DontHit
    }

    public enum TargetMovementType
    {
        Stationary,
        Mobile
    }

    public TargetType targetType;
    public TargetMovementType movementType;
    public ScoreSystem scoreSystem;
    public OLD_Spawner spawner;
    public TargetRotate rotator;
    public float speed;
    public int score;
    public int minRate;
    public int maxRate;

    public float despawnTime;

    private void Start()
    {
        if (movementType == TargetMovementType.Stationary)
        {
            rotator = GetComponentInParent<TargetRotate>();
            scoreSystem = rotator.scoreSystem;
            rotator.rotationSpeed = speed;
            rotator.minDelayRate= minRate;
            rotator.maxDelayRate= maxRate;
        }

        if (movementType == TargetMovementType.Mobile)
        {
            spawner = GetComponentInParent<OLD_Spawner>();
            scoreSystem = spawner.ScoreSystem;
            spawner.minSpawnRate = minRate;
            spawner.maxSpawnRate = maxRate;
        }
    }

    private void Update()
    {
        if(movementType == TargetMovementType.Mobile)
        {
        Destroy(gameObject, despawnTime);
        }
    }

    void FixedUpdate()
    {
        if(movementType == TargetMovementType.Mobile)
        {
            Vector3 position = transform.position;

            if (spawner.Direction == OLD_Spawner.SpawnDirection.TowardsLeft)
            {
                position = new(transform.position.x - Vector3.left.x * speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else if (spawner.Direction == OLD_Spawner.SpawnDirection.TowardsRight)
            {
                position = new(transform.position.x - Vector3.right.x * speed * Time.deltaTime, transform.position.y, transform.position.z);
            }

            transform.position = position;
        }
    }

    public void TargetHit()
    {
        if (movementType == TargetMovementType.Mobile)
        {
            Destroy(gameObject);
        }

        if(movementType == TargetMovementType.Stationary)
        {
            rotator.isHit = !rotator.isHit;
        }

        scoreSystem.currentScore += score;
    }
}
