using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadAsteroid : Asteroid
{
    public override void HitHandler()
    {
        Destroy(gameObject);
        EventBus.onDamageChecked?.Invoke();
        //Проигрываение звук смэрти
        //Спавн эффекта
    }
}
