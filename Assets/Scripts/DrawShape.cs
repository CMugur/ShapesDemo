using System.Text;
using UnityEngine;

public class DrawShape : MonoBehaviour
{
    [SerializeField] private float _shapeSideLength = 1;
    [SerializeField] private Shape _shapePrefab;
    
    private void Start()
    {
        DrawHexagon(_shapeSideLength);
    }

    private void DrawHexagon(float shapeSideLength)
    {
        var textureSize = ShapeHelper.GetHexTextureSize(shapeSideLength);
        var vertices = ShapeHelper.GetVertices(6, shapeSideLength);
        vertices = ShapeHelper.ShiftToTextureCenter(textureSize, vertices);
        var triangles = ShapeHelper.GetHexTriangles();
        DrawPolygon(vertices, triangles, textureSize);
    }

    private void DrawPolygon(Vector2[] vertices, ushort[] triangles, Vector2Int textureSize)
    {
        var shape = Instantiate(_shapePrefab);
        shape.Initialize(ShapeHelper.GetHexVertices(0.5f));
        var texture = new Texture2D(textureSize.x, textureSize.y);

        var colors = new Color[texture.width * texture.height];
        for (var i = 0; i < colors.Length; i++) colors[i] = Color.white;
        texture.SetPixels(colors);
        texture.Apply();

        shape.SpriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero,
            textureSize.x);
        shape.SpriteRenderer.sprite.OverrideGeometry(vertices, triangles); 
    }

    public static void LogArray(Vector2[] array)
    {
        var sb = new StringBuilder();
        foreach (var entry in array)
        {
            sb.Append(entry);
        }

        Debug.Log(sb);
    }
}