using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileAsteroid : Asteroid
{
    [SerializeField]
    private GameObject _sadAsteroid;
    [SerializeField]
    private int _sadAsteroidSpawnCount = 2;
    [SerializeField]
    private int _giveScrore = 200;

    public override void HitHandler()
    {
        Destroy(this.gameObject);

        EventBus.onAsteroidDamageChecked?.Invoke();
        EventBus.onAsteroidScoreChecked?.Invoke(_giveScrore);

        for (int i = 0; i < _sadAsteroidSpawnCount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            Instantiate(_sadAsteroid, transform.position, rotation);
        }

    }
}
