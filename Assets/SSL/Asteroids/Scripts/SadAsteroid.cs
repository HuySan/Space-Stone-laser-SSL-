using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadAsteroid : Asteroid
{
    [SerializeField]
    private int _giveScrore = 100;
    public override void HitHandler()
    {
        Destroy(gameObject);
        EventBus.onAsteroidDamageChecked?.Invoke();
        EventBus.onAsteroidScoreChecked?.Invoke(_giveScrore);
        //Проигрываение звук смэрти
        //Спавн эффекта
    }
}
