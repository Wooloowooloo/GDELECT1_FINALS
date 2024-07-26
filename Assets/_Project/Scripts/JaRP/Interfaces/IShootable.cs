using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void OnSpawn(ESpawnerType spawnerType);
    IEnumerator OnHit();
    void Despawn();
}
