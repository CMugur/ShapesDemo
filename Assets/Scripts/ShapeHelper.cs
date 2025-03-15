using System;
using UnityEngine;

public static class ShapeHelper
{
    private const int MIN_SIDES_COUNT = 3;
    private const int MAX_SIDES_COUNT = 100;
        
    
    #region Hexagon
    
    private static float GetHexApothem(float hexSideLength)
    {
        return hexSideLength * (float)Math.Sqrt(3) / 2;
    }

    public static Vector2[] GetHexVertices(float hexSideLength)
    {
        var halvedSideLength = hexSideLength / 2f;
        var apothem = GetHexApothem(hexSideLength);
        
        var vertices = new Vector2[]
        {
            new(-halvedSideLength, apothem), new(halvedSideLength, apothem), new(hexSideLength, 0),
            new(halvedSideLength, -apothem), new(-halvedSideLength, -apothem), new(-hexSideLength, 0)
        };

        return vertices;
    }

    public static ushort[] GetHexTriangles()
    {
        return new ushort[] { 0, 1, 5, 5, 1, 2, 2, 5, 4, 4, 2, 3 };
    }
    
    public static Vector2Int GetHexTextureSize(float hexSideLength)
    {
        var width = (int)Math.Ceiling((hexSideLength) * 2);
        //This will make sure that the texture is the same height as the hexagon render
        //Not used since it does not provide any meaningful benefit for this project,
        //and it makes centering the collider a bit harder
        //var height = (int)Math.Ceiling(GetHexApothem(hexSideLength) * 2);
        return new Vector2Int(width, width);
    }

    #endregion

    #region General

    public static Vector2[] ShiftToTextureCenter(Vector2Int textureSize, Vector2[] vertices)
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
            case 6: return "Hexagon";
            case 5: return "Pentagon";
            case 4: return "Square";
            case 3: return "Triangle";
            default: return "OTHER";
        }
    }

    public static Vector2[] GetVertices(int sidesCount, float radius)
    {
        sidesCount = Math.Clamp(MIN_SIDES_COUNT, MAX_SIDES_COUNT, sidesCount);
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

    #endregion
}