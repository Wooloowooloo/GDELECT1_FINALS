using System.Collections;

public interface ITarget
{
    void OnSpawn(ESpawnerType spawnerType);
    IEnumerator OnHit();
    IEnumerator NaturalDespawn();
}
