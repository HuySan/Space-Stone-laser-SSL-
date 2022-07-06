using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ICoroutineStartable
{
    void StartChildCoroutine(IEnumerator corutineMethod);
}
