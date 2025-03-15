using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D), typeof(SpriteRenderer))]
public class Shape : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PolygonCollider2D _polygonCollider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    
    public void Initialize(Vector2[] vertices)
    {
        transform.name = ShapeHelper.GetShapeName(vertices.Length);
        _polygonCollider.points = vertices;
        _polygonCollider.offset = Vector2.one * 0.5f;        
    }

    public void SetRandomColor()
    {
        SpriteRenderer.color = Random.ColorHSV();
    }
}