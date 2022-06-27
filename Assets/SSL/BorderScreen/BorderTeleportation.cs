using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTeleportation : MonoBehaviour
{
    private Vector2 _screenBounds;

    private void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        float offset = 1;//нужен для смещения объекта когда он телепортируется

        if (viewPos.x > _screenBounds.x)
            transform.position = new Vector2(-viewPos.x + offset, viewPos.y);

        if (viewPos.x < _screenBounds.x * -1)
            transform.position = new Vector2(-viewPos.x - offset, viewPos.y);

        if (viewPos.y > _screenBounds.y)
            transform.position = new Vector2(viewPos.x, -viewPos.y + offset);

        if (viewPos.y < _screenBounds.y * -1)
            transform.position = new Vector2(viewPos.x, -viewPos.y - offset);
    }
}
