using UnityEngine;
using TMPro;

class SafeSystem
{

    private TextMeshProUGUI _nowScoreText;
    private TextMeshProUGUI _bestScoreText;

    private int _nowScore;
    private const string MAX_SCORE = "maxScore";

    public SafeSystem(TextMeshProUGUI nowScoreText, TextMeshProUGUI bestScoreText)
    {
        _nowScoreText = nowScoreText;
        _bestScoreText = bestScoreText;
    }

    public void SaveAndShowScore(int nowScore)
    {
        _nowScore = nowScore;
        _nowScoreText.text = $"NOW SCORE: {_nowScore}";
        SetScore();
    }

    private void SetScore()
    {
        if (_nowScore > LoadMaxScore())
        {
            SafeMaxScore();
            _bestScoreText.text = $"BEST SCORE: {_nowScore}";
            return;
        }


        _bestScoreText.text = $"BEST SCORE: {LoadMaxScore()}";

    }
    private void SafeMaxScore()
    {
        PlayerPrefs.SetInt(MAX_SCORE, _nowScore);

    }

    private int LoadMaxScore()
    {
        int result = PlayerPrefs.GetInt(MAX_SCORE, 0);
        return result;
    }
}
