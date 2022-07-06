using System;
using UnityEngine;

public static class EventBus
{
    public static Action onAsteroidDamageChecked;
    public static Action<int> onAsteroidScoreChecked;

    public static Func<GameObject> onObjectTransfed;

    public static Action onAlienDeathChecked;

    public static Action onPlayerIsAlived;
}

