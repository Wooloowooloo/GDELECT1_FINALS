using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_Spawner : MonoBehaviour
{
    public enum SpawnDirection
    {
        TowardsLeft,
        TowardsRight,
    }

    public SpawnDirection Direction;
    public OLD_ScoreSystem ScoreSystem;
    public GameObject[] targets;
    [HideInInspector] public float minSpawnRate;
    [HideInInspector] public float maxSpawnRate;

    private bool canSpawn;

    void Start()
    {
        canSpawn = true;
    }

    void Update()
    {
        if(canSpawn == true)
        {
            StartCoroutine(SpawnTarget());
        }
    }

    IEnumerator SpawnTarget()
    {
        canSpawn = false;
        int targetLayer = LayerMask.NameToLayer("Targets");
        var spawned = Instantiate(targets[Random.Range(0, targets.Length)], transform.position, Quaternion.identity);
        spawned.transform.parent = transform;
        spawned.layer = targetLayer;
        yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
        canSpawn = true;
    }
}