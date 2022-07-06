using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

class SpawnerLogic
{

    private GameObject _alienPrefab;
    private GameObject _asteroidPrefab;
    private ICoroutineStartable _coroutineStartable;
    private bool _alienIsAlive = false;

    private int _asteroidsStartSpawnCount;

    private int _inflictCount = 0;

    public SpawnerLogic(GameObject alienPrefab, GameObject asteroidPrefab, ICoroutineStartable coroutineStartable, int asteroidsStartSpawnCount)
    {
        _alienPrefab = alienPrefab;
        _asteroidPrefab = asteroidPrefab;
        _coroutineStartable = coroutineStartable;
        _asteroidsStartSpawnCount = asteroidsStartSpawnCount;
    }

    public void Spawn()
    {
        if (_alienIsAlive)
            return;

        if (_inflictCount == (_asteroidsStartSpawnCount * 3) - 3)//Когда остаётся один астеройд
        {
            _alienIsAlive = true;
            Debug.Log("Спавним тарелку");
            Alien();
        }

        if (_inflictCount >= _asteroidsStartSpawnCount * 3)
        {
            _inflictCount = 0;
            _asteroidsStartSpawnCount += 1;
            _coroutineStartable.StartChildCoroutine(Delay(_asteroidsStartSpawnCount));
        }
    }

    private void Waves(int startCount)
    {
        Asteroid(startCount);
    }

    IEnumerator Delay(int startSpawnCount)
    {
        yield return new WaitForSeconds(1);
        Waves(startSpawnCount);
    }

    private void Alien()
    {
        Vector3 bottomLeftWorld = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRightWorld = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        Vector2 topRandomBorder = new Vector2();
        bool checkCollider = true;

        while (checkCollider)
        {
            topRandomBorder = new Vector3(Random.Range(bottomLeftWorld.x, topRightWorld.x), topRightWorld.y);
            checkCollider = Physics2D.OverlapCircle(topRandomBorder, _alienPrefab.transform.lossyScale.y + 0.5f);
        }

        GameObject.Instantiate(_alienPrefab, topRandomBorder, Quaternion.identity);
    }

    public void Asteroid(int startSpawnCount)
    {
        Vector3 bottomLeftWorld = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRightWorld = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Vector3 topRandomBorder = new Vector3(Random.Range(bottomLeftWorld.x, topRightWorld.x), topRightWorld.y, 0);
        Vector3 bottomRandomBorder = new Vector3(Random.Range(bottomLeftWorld.x, topRightWorld.x), bottomLeftWorld.y, 0);
        Vector3 rightRandomBorder = new Vector3(topRightWorld.x, Random.Range(bottomLeftWorld.y, topRightWorld.y), 0);
        Vector3 leftRandomBorder = new Vector3(bottomLeftWorld.x, Random.Range(bottomLeftWorld.y, topRightWorld.y), 0);

        List<Vector3> randomBorders = new List<Vector3> { topRandomBorder, bottomRandomBorder, rightRandomBorder, leftRandomBorder };

        for (int i = 0; i < startSpawnCount; i++)
        {

            Vector3 randomnSpawnPosition = randomBorders[Random.Range(0, randomBorders.Count)];
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            GameObject.Instantiate(_asteroidPrefab, randomnSpawnPosition, rotation);
        }
    }

    public void AlienDeath()
    {
        _alienIsAlive = false;
        _inflictCount += 1;
    }

    public void AsteroidDamageCount()
    {
        _inflictCount += 1;
    }
}
