using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _statusText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Animator _scoreTextAnimator;
    [SerializeField] private Image _roundTimeImage;
    [SerializeField] private Animator _roundTimeImageAnimator;
    [SerializeField] private Button _demoButton;
    [SerializeField] private RectTransform _safeSpawnSpace;
    public RectTransform SafeSpawnSpace => _safeSpawnSpace;
    
    [Header("Settings")]
    [SerializeField] private string _roundStartTextFormat = "Double tap on the {0} shape!";
    [SerializeField] private string _roundEndTextFormat = "Great job!";
    [SerializeField] private string _gameOverTextFormat = "Game over! Try again!";
    [SerializeField] private string _instructionsTextFormat = "Find the right shape and double tap on it to gain points!";
    [SerializeField] private string _scoreFormat = "Points: {0}";
    [SerializeField] private int _scoreIncrements = 10;
    
    private int _score;
    private bool _resetScoreOnNextRoundStart;

    private void Awake()
    {
        _demoButton.onClick.AddListener(OnDemoButtonClicked);
    }

    private void OnDemoButtonClicked()
    {
        SceneManager.LoadScene("DemoScene");
    }

    public void ShowInstructions()
    {
        _statusText.text = _instructionsTextFormat;
    }

    public void OnRoundStart(string shapeName)
    {
        _statusText.text = string.Format(_roundStartTextFormat, shapeName);
        if (_resetScoreOnNextRoundStart)
        {
            UpdateScore(-_score);
        }
    }

    public void OnRoundEnd(bool isGameOver)
    {
        _statusText.text = isGameOver ?  _gameOverTextFormat : _roundEndTextFormat;
        _resetScoreOnNextRoundStart = isGameOver;
        if (isGameOver) return;
        UpdateScore(_scoreIncrements);
    }

    public void OnRoundTimerUpdated(float roundTimerNormalized)
    {
        _roundTimeImage.fillAmount = roundTimerNormalized;
    }

    public void OnPenaltyApplied()
    {
        _roundTimeImageAnimator.Play("Penalty");
    }

    private void UpdateScore(int amount)
    {
        _score += amount;
        _scoreText.text = string.Format(_scoreFormat, _score);
        _scoreTextAnimator.Play("Wobble");
    }
}
