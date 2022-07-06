using System;
using UnityEngine;


class PlayerBullet : Bullets
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHitable iHitable = collision.gameObject.GetComponent<IHitable>();

        if (iHitable != null)
        {
            iHitable.HitHandler();
            Destroy(this.gameObject);
        }
    }
}

