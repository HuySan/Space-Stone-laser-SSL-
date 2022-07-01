using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Character : MonoBehaviour, IMovable, IShooting, IDamageible
{
    [SerializeField]
    private float _speed = 0;
    [SerializeField]
    private float _maxSpeed = 7;
    [SerializeField]
    private float _acceleration = 1.6f;
    [SerializeField]
    private float _inertia = 2.5f;
    [SerializeField]
    private float _speedRotate = 10f;
    [SerializeField]
    private FirePoint _firePoint;

    [SerializeField]
    private TextMeshProUGUI _speedText;
    [SerializeField]
    private TextMeshProUGUI _angleText;
    [SerializeField]
    private TextMeshProUGUI _coordinatesText;

    [SerializeField]
    private TextMeshProUGUI _scoreText;
    private int _score = 0;

    [SerializeField]
    private int _alienGiveScore = 500;

    [SerializeField]
    private GameObject _gridHeart;
    private Image[] _hearts;

    private Vector2 _targetDirection;
    private Vector2 _directionMove;
    private int _healthCount;

    private void Awake()
    {
         _hearts = _gridHeart.GetComponentsInChildren<Image>();
        _healthCount = _hearts.Length;
       // _hearts[_healthCount].gameObject.SetActive(false);
    }

    void Update()
    {
        TextConverter();
    }

    private void TextConverter()
    {
        //speed
        if (Mathf.Floor(_speed) > 0)
            _speedText.text = $"{Mathf.Floor(_speed)}kkk km/h";

        //angle
        float angle = this.gameObject.transform.eulerAngles.z;
        _angleText.text = $"{Mathf.Floor(angle)}";

        //coordinates
        float x = Mathf.Floor(this.gameObject.transform.position.x);
        float y = Mathf.Floor(this.gameObject.transform.position.y);
        _coordinatesText.text = $"X: {x} | Y: {y}";

        //score
        if (_score != 0)
            _scoreText.text = $"{_score}";
    }

    public void LookAtMouse(Vector2 positionMouse)
    {
        _targetDirection = Camera.main.ScreenToWorldPoint(positionMouse) - transform.position;
        float angle = Mathf.Atan2(_targetDirection.x, _targetDirection.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speedRotate * Time.deltaTime);
    }

    public void MoveForward(Vector2 direction)
    {
          _directionMove = direction;

          if (_speed < _maxSpeed)
              _speed += _acceleration * Time.deltaTime;
          transform.Translate(direction *  _speed * Time.deltaTime);
    }

    public void MoveForward(bool isPressed)
    {
        if (isPressed)
            return;

        if (_speed <= 0)
            return;

        _speed -= _inertia * Time.deltaTime;
        transform.Translate(_directionMove * _speed * Time.deltaTime);
    }

    public void ShootBullet()
    {
        _firePoint.Bullet();
    }

    public void ShootLaser()
    {
        _firePoint.Laser();
    }

    public void InflictDamage()
    {
        _healthCount--;
        _hearts[_healthCount].gameObject.SetActive(false);
        //Отнимаем от ui сердце
        if (_healthCount == 0)
        {
            Destroy(this.gameObject);
            //экран смерти
            _healthCount = 3;
        }
        //спавним эффект
        //Вызываем скрипт с проигрыванием аудио(Думаю сделать всю работу аудио в отдельном скрипте и из него вызывать методы)
    }

    private void OnEnable()
    {
        EventBus.onObjectTransfed += Init;
        EventBus.onAsteroidScoreChecked += GetAsteroidScore;
        EventBus.onAlienDeathChecked += AlienGetScore;
    }

    private void OnDisable()
    {
        EventBus.onObjectTransfed -= Init;
        EventBus.onAsteroidScoreChecked += GetAsteroidScore;
        EventBus.onAlienDeathChecked -= AlienGetScore;
    }

    private void AlienGetScore()
    {
        _score += _alienGiveScore;
    }

    private void GetAsteroidScore(int score)
    {
        _score += score;
    }

    private GameObject Init()
    {
        return this.gameObject;
    }

}
