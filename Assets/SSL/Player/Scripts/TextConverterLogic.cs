using UnityEngine;
using TMPro;
class TextConverterLogic 
{
    private GameObject _gameObject;

    public TextConverterLogic(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

    public void TextConverter(float speed, int score, TextMeshProUGUI angleText, TextMeshProUGUI speedText, TextMeshProUGUI coordinatesText, TextMeshProUGUI scoreText)
    {
        //speed
        if (Mathf.Floor(speed) >= 0)
            speedText.text = $"{Mathf.Floor(speed)}kkk km/h";

        //angle
        float angle = _gameObject.transform.eulerAngles.z;
        angleText.text = $"{Mathf.Floor(angle)}";

        //coordinates
        float x = Mathf.Floor(_gameObject.transform.position.x);
        float y = Mathf.Floor(_gameObject.transform.position.y);
        coordinatesText.text = $"X: {x} | Y: {y}";

        //score
        if (score != 0)
            scoreText.text = $"{score}";
    }
}
