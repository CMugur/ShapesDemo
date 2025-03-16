using System;
using System.Collections.Generic;
using UnityEngine;

public static class ShapeHelper
{
    public const int MIN_SIDES_COUNT = 3;
    public const int MAX_SIDES_COUNT = 10;

    /// <summary>
    /// Generates a sprite of a shape
    /// </summary>
    /// <param name="sidesCount">The number of sides that the shape has</param>
    /// <param name="radius">The radius of the circle that surrounds the shape</param>
    /// <param name="textureSize">The size of the texture</param>
    /// <returns></returns>
    public static Sprite GenerateSprite(int sidesCount, float radius, Vector2Int textureSize)
    {
        var vertices = GetVertices(sidesCount, radius);
        vertices = ShiftToTextureCenter(textureSize, vertices);
        var triangles = GetTrianglesIndices(sidesCount);
        
        var texture = new Texture2D(textureSize.x, textureSize.y);

        var colors = new Color[texture.width * texture.height];
        for (var i = 0; i < colors.Length; i++) colors[i] = Color.white;
        texture.SetPixels(colors);
        texture.Apply();

        var sprite = Sprite.Create(texture,
            new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f,
            textureSize.x);
        sprite.OverrideGeometry(vertices, triangles);
        
        return sprite;
    }
    
    /// <summary>
    /// Generates points found on a circle circumference 
    /// </summary>
    /// <param name="sidesCount">The number of sides that the shape has</param>
    /// <param name="radius">The radius of the circle that we want to generate points on</param>
    /// <returns></returns>
    public static Vector2[] GetVertices(int sidesCount, float radius)
    {
        sidesCount = ClampSidesCount(sidesCount);
        var angleDelta = 360 / sidesCount;
        var radiansDelta = angleDelta * Mathf.Deg2Rad;
        var radians = 0f;

        var vertices = new Vector2[sidesCount];
        while (sidesCount > 0)
        {
            sidesCount--;
            var xPos = radius * Mathf.Cos(radians);
            var yPos = radius * Mathf.Sin(radians);
            vertices[sidesCount] = new Vector2(xPos, yPos);
            radians += radiansDelta;
        }

        return vertices;
    }

    /// <summary>
    /// Generates the triangle indices for the shape we want to render
    /// </summary>
    /// <param name="sidesCount">The number of sides that the shape has</param>
    /// <returns></returns>
    private static ushort[] GetTrianglesIndices(int sidesCount)
    {
        sidesCount = ClampSidesCount(sidesCount);
        var indices = new List<ushort>();
        for (var i = 0; i < sidesCount - 2; i++)
        {
            indices.Add(0);
            indices.Add((ushort)(i + 1));
            indices.Add((ushort)(i + 2));
        }

        return indices.ToArray();
    }

    public static int ClampSidesCount(int sidesCount)
    {
        return Math.Clamp(sidesCount, MIN_SIDES_COUNT, MAX_SIDES_COUNT);
    }

    private static Vector2[] ShiftToTextureCenter(Vector2Int textureSize, Vector2[] vertices)
    {
        var xShift = textureSize.x / 2f;
        var yShift = textureSize.y / 2f;

        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] += new Vector2(xShift, yShift);
        }

        return vertices;
    }

    public static string GetShapeName(int verticesCount)
    {
        switch (verticesCount)
        {
            case 10: return "Decagon";
            case 9: return "Nonagon";
            case 8: return "Octagon";
            case 7: return "Heptagon";
            case 6: return "Hexagon";
            case 5: return "Pentagon";
            case 4: return "Square";
            case 3: return "Triangle";
            default: return "OTHER";
        }
    }
}