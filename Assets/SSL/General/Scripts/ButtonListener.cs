using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonListener : MonoBehaviour
{
    [SerializeField]
    private Button _replayButton;

    private void OnEnable()
    {
        _replayButton.onClick.AddListener(OnButtonReplayPress);
    }

    private void OnDisable()
    {
        _replayButton.onClick.RemoveListener(OnButtonReplayPress);
    }

    private void OnButtonReplayPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
