using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D), typeof(SpriteRenderer))]
public class Shape : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PolygonCollider2D _polygonCollider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _particleSystem;
    public ParticleSystem ParticleSystem => _particleSystem;
    
    [Header("Settings")]
    [SerializeField] private int _radius;
    private Vector2Int _textureSize => Vector2Int.one * _radius * 2;

    public int SidesCount { get; private set; }

    public void DrawPolygon(int sidesCount)
    {
        SidesCount = ShapeHelper.ClampSidesCount(sidesCount);
        if (_spriteRenderer.sprite != null) Destroy(_spriteRenderer.sprite);
        _spriteRenderer.sprite = ShapeHelper.GenerateSprite(sidesCount, _radius, _textureSize);
        _polygonCollider.points = ShapeHelper.GetVertices(sidesCount, 0.5f);
        transform.name = ShapeHelper.GetShapeName(sidesCount);
    }

    public void Interact()
    {
        SetRandomColor();
        _animator.Play("Wobble");
        _audioSource.Play();
        _particleSystem.Play();
    }

    public void SetRandomColor()
    {
        _spriteRenderer.color = Random.ColorHSV();        
    }
}