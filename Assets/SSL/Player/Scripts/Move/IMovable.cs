using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

interface IMovable
{
    void MoveForward(Vector2 direction);
    void MoveForward(bool isPressed = true);

    void LookAtMouse(Vector2 position);
}

