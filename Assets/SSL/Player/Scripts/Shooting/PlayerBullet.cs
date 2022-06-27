using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class PlayerBullet : Bullets
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
        IHitable iHitable = collision.gameObject.GetComponent<IHitable>();
      /*  if (asteroid != null)
        {
            Destroy(this.gameObject);
            asteroid.ObjectDecay();
        }*/

        if (iHitable != null)
            iHitable.HitHandler();
    }
}

