using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _smileAsteroidPrefab;
    [SerializeField]
    private int _asteroidsStartSpawnCount = 5;

    [SerializeField]
    private GameObject _alienPrefab;

    private bool _alienIsAlive;

    private int _inflictCount = 0;

    void Start()
    {
        _alienIsAlive = false;

        if(_smileAsteroidPrefab == null)
        {
            Debug.LogError("SmileAsteroidPrefab was not added");
            return;
        }

        if (_alienPrefab == null)
        {
            Debug.LogError("AlienPrefab was not added");
            return;
        }

        AsteroidSpawner(_asteroidsStartSpawnCount, _smileAsteroidPrefab);
    }

    private void Update()
    {
        if (_alienIsAlive)
            return;
            
        if (_inflictCount == (_asteroidsStartSpawnCount * 3) - 3)//Когда остаётся один астеройд
        {
            _alienIsAlive = true;
            Debug.Log("Спавним тарелку");
            AlienSpawner();
        }

        if (_inflictCount >= _asteroidsStartSpawnCount * 3)
        {
            _inflictCount = 0;
            _asteroidsStartSpawnCount += 1;
            StartCoroutine(Delay(_asteroidsStartSpawnCount));
            return;
        }
        
    }

    private void AsteroidSpawner(int startSpawnCount, GameObject prefab)
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
            Instantiate(prefab, randomnSpawnPosition, rotation);
        }
    }

    private void AlienSpawner()
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

        Instantiate(_alienPrefab, topRandomBorder, Quaternion.identity);
    }

    private void Waves(int startSpawnCount)
    {
        AsteroidSpawner(startSpawnCount, _smileAsteroidPrefab);
    }

    IEnumerator Delay(int startSpawnCount)
    {
        yield return new WaitForSeconds(1);
        Waves(startSpawnCount);
    }


    private void OnEnable()
    {
        EventBus.onAsteroidDamageChecked += AsteroidDamageCount;
        EventBus.onAlienDeathChecked += AlienDeath;
    }

    private void OnDisable()
    { 
        EventBus.onAsteroidDamageChecked -= AsteroidDamageCount;
        EventBus.onAsteroidDamageChecked -= AlienDeath;
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
