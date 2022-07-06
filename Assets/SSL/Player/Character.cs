using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Character : MonoBehaviour,IMovable, IShooting, IDamageible
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
    private GameObject _deathUiPanel;
    [SerializeField]
    private TextMeshProUGUI _bestScoreText;
    [SerializeField]
    private TextMeshProUGUI _nowScoreText;

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

    private int _healthCount;

    private TextConverterLogic _logic;
    private MoverLogic _moverLogic;
    private DeathLogic _deathLogic;
    private SafeSystem _safeSystem;

    private bool _playerIsAlive;

    private void Awake()
    {
        _playerIsAlive = true;

        _logic = new TextConverterLogic(this.gameObject);
        _moverLogic = new MoverLogic(_speed, _maxSpeed, _speedRotate, _acceleration, _inertia, this.transform);

         _hearts = _gridHeart.GetComponentsInChildren<Image>();
        _healthCount = _hearts.Length;
        _deathLogic = new DeathLogic(_healthCount, this.gameObject, _deathUiPanel, _hearts);
        _safeSystem = new SafeSystem(_nowScoreText, _bestScoreText);
    }

    void Update()
    {

        if (!_playerIsAlive)
            return;
        _logic.TextConverter(_speed, _score, _angleText, _speedText, _coordinatesText, _scoreText);
    }

    public void MoveForward(Vector2 direction)
    {
        if (!_playerIsAlive)
            return;
        _moverLogic.MoveForward(direction, out float speed);
        _speed = speed;
    }

    //Вызывается, если нажимаем на кнопку движения
    public void MoveForward(bool isPressed = true)
    {
        if (!_playerIsAlive)
            return;
        _moverLogic.MoveForward(isPressed, out float speed);
        _speed = speed;
    }

    public void LookAtMouse(Vector2 position)
    {
        if (!_playerIsAlive)
            return;
        _moverLogic.LookAtMouse(position);
    }

    public void ShootBullet()
    {
        if (!_playerIsAlive)
            return;
        _firePoint.Bullet();
    }

    public void ShootLaser()
    {
        if (!_playerIsAlive)
            return;
        _firePoint.Laser();
    }

    public void InflictDamage()
    {
        if (!_playerIsAlive)
            return;
        _deathLogic.InflictDamage();
        _safeSystem.SaveAndShowScore(_score);
    }

    private void OnEnable()
    {
        EventBus.onObjectTransfed += Init;
        EventBus.onAsteroidScoreChecked += GetAsteroidScore;
        EventBus.onAlienDeathChecked += AlienGetScore;
        EventBus.onPlayerIsAlived += PlayerIsAlive;
    }

    private void OnDisable()
    {
        EventBus.onObjectTransfed -= Init;
        EventBus.onAsteroidScoreChecked += GetAsteroidScore;
        EventBus.onAlienDeathChecked -= AlienGetScore;
        EventBus.onPlayerIsAlived -= PlayerIsAlive;
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

    private void PlayerIsAlive()
    {
        _playerIsAlive = false;
    }
}
