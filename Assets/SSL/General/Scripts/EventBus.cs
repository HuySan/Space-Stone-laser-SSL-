﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class EventBus
{
    public static Action onDamageChecked;

    public static Func<GameObject> onObjectTransfed;
}
