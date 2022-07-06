using UnityEngine;

public class BorderTeleportation : MonoBehaviour
{
    private Vector2 _screenBounds;
    private BorderLogic _borderLogic;

    private void Awake()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _borderLogic = new BorderLogic(_screenBounds, this.gameObject);
    }

    private void LateUpdate()
    {
        _borderLogic.SetBorder();
    }

}
