using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class EventBus
{
    public static Action onAsteroidDamageChecked;
    public static Action<int> onAsteroidScoreChecked;

    public static Func<GameObject> onObjectTransfed;

    public static Action onAlienDeathChecked;
}

