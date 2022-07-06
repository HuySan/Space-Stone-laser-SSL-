using UnityEngine;
using UnityEngine.UI;

class DeathLogic
{
    private int _healthCount;
    private GameObject _gameObject;
    private Image[] _hearts;
    private GameObject _deathUi;


    public DeathLogic(int healthCount, GameObject gameObject, GameObject deathUiPanel, Image[] hearts)
    {
        _healthCount = healthCount;
        _gameObject = gameObject;
        _hearts = hearts;
        _deathUi = deathUiPanel;
    }

    public void InflictDamage()
    {
        _healthCount--;
        _hearts[_healthCount].gameObject.SetActive(false);
        //Отнимаем от ui сердце
        if (_healthCount == 0)
        {           
            EventBus.onPlayerIsAlived?.Invoke();
            EnableDisableComponents();
            _healthCount = 3;
        }
    }

    private void EnableDisableComponents()
    {
        _gameObject.GetComponent<SpriteRenderer>().enabled = false;
        _gameObject.GetComponent<BoxCollider2D>().enabled = false;

        _deathUi.SetActive(true);
    }

}
