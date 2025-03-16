using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemoView : MonoBehaviour
{
    [SerializeField] private Slider _shapeSidesCountSlider;
    [SerializeField] private TextMeshProUGUI _shapeSidesCountText;
    [SerializeField] private Shape _shape;
    [SerializeField] private Button _gameButton;
    
    private void Awake()
    {
        _shapeSidesCountSlider.minValue = ShapeHelper.MIN_SIDES_COUNT;
        _shapeSidesCountSlider.maxValue = ShapeHelper.MAX_SIDES_COUNT;
        _shapeSidesCountSlider.onValueChanged.AddListener(OnValueChangedShapeSidesCountSlider);
        _gameButton.onClick.AddListener(OnGameButtonClicked);
    }

    private void OnGameButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void Start()
    {
        _shapeSidesCountText.text = _shapeSidesCountSlider.value.ToString();
        _shape.DrawPolygon((int)_shapeSidesCountSlider.value);
    }

    private void OnValueChangedShapeSidesCountSlider(float value)
    {
        _shapeSidesCountText.text = value.ToString();
        _shape.DrawPolygon((int)value);
        _shape.Interact();
    }
}
