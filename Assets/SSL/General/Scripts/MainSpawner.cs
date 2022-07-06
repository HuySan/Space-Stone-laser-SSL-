using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawner : MonoBehaviour, ICoroutineStartable
{
    [SerializeField]
    private GameObject _smileAsteroidPrefab;
    [SerializeField]
    private int _asteroidsStartSpawnCount = 5;

    [SerializeField]
    private GameObject _alienPrefab;

    private SpawnerLogic _spawnerLogic;
    private ICoroutineStartable _coroutineStartable;

    void Awake()
    {
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

        _coroutineStartable = this.gameObject.GetComponent<ICoroutineStartable>();
        _spawnerLogic = new SpawnerLogic(_alienPrefab, _smileAsteroidPrefab, _coroutineStartable, _asteroidsStartSpawnCount);

        _spawnerLogic.Asteroid(_asteroidsStartSpawnCount);
    }

    private void Update()
    {
        _spawnerLogic.Spawn();       
    }


    private void OnEnable()
    {
        EventBus.onAsteroidDamageChecked += _spawnerLogic.AsteroidDamageCount;
        EventBus.onAlienDeathChecked += _spawnerLogic.AlienDeath;
    }

    private void OnDisable()
    { 
        EventBus.onAsteroidDamageChecked -= _spawnerLogic.AsteroidDamageCount;
        EventBus.onAlienDeathChecked -= _spawnerLogic.AlienDeath;
    }


    void ICoroutineStartable.StartChildCoroutine(IEnumerator corutineMethod)
    {
        StartCoroutine(corutineMethod);
    }
}
