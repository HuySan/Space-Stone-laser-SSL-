using System;
using UnityEngine;

class BorderLogic
{
    private Vector2 _screenBounds;
    private GameObject _controlledGameObject;
    public BorderLogic(Vector2 screenBounds, GameObject controlledGameObject)
    {
        _screenBounds = screenBounds;
        _controlledGameObject = controlledGameObject;
    }

    public void SetBorder()
    {
        Vector3 transformPosition = _controlledGameObject.transform.position;
        float offset = 1;//нужен для смещения объекта когда он телепортируется

        if (transformPosition.x > _screenBounds.x)
            _controlledGameObject.transform.position = new Vector2(-transformPosition.x + offset, transformPosition.y);

        if (transformPosition.x < _screenBounds.x * -1)
            _controlledGameObject.transform.position = new Vector2(-transformPosition.x - offset, transformPosition.y);

        if (transformPosition.y > _screenBounds.y)
            _controlledGameObject.transform.position = new Vector2(transformPosition.x, -transformPosition.y + offset);

        if (transformPosition.y < _screenBounds.y * -1)
            _controlledGameObject.transform.position = new Vector2(transformPosition.x, -transformPosition.y - offset);
    }
}
