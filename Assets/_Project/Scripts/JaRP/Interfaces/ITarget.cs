using System.Collections;

public interface ITarget
{
    void OnSpawn();
    IEnumerator OnHit();
    IEnumerator NaturalDespawn();
}
