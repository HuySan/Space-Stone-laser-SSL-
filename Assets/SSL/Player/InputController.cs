using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class InputController : MonoBehaviour
{
    [SerializeField]
    private GameObject _controllerObject;

    private IMovable _iMovable;
    private IShooting _iShooting;

    private PlayerInput _input;
    private bool _isPressed;
    private Vector2 _direction;
    private void Awake()
    {
        _input = new PlayerInput();
    }

    void Start()
    {
        _iMovable = _controllerObject.GetComponent<IMovable>();
        if (_iMovable == null)
            throw new NullReferenceException("IMovable not found");

        _iShooting = _controllerObject.GetComponent<IShooting>();
        if (_iShooting == null)
            throw new NullReferenceException("IShooting not found");

    }

    private void Update()
    {
        if (_controllerObject == null)
            return;
        //Keyboard
        if (_input.Player.MoveForword.IsPressed())
        {
             _direction = _input.Player.MoveForword.ReadValue<Vector2>();
            _iMovable.MoveForward(_direction);
        }
        else
        {
            _isPressed = false;
            _iMovable.MoveForward(_isPressed);
        }

        //Look at Mouse
        Vector2 mousePosition = _input.Player.MousePosition.ReadValue<Vector2>();
        _iMovable.LookAtMouse(mousePosition);

        //bullet
        _input.Player.MouseLeft.started += context => _iShooting.ShootBullet();


        //Laser
        _input.Player.MouseRight.started += context => _iShooting.ShootLaser();
       // if (!_input.Player.MouseRight.IsPressed())
       //     _iShooting.shootLaser(false);

        //   if (_input.Player.MouseRight.IsPressed())
        //      _iShooting.shootLaser(true);
        //   else
        //    _iShooting.shootLaser(false);
    }



    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
   

}

