using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyBullet : Bullets
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageible iDamageible = collision.gameObject.GetComponent<IDamageible>();
        if (iDamageible == null)
            return;

        iDamageible.InflictDamage();
    }
}
