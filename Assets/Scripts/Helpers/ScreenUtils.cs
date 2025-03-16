using UnityEngine;

namespace Utils
{
    public static class ScreenUtils
    {
        public static Vector3 GetRandomPositionInsideCameraView(float depth)
        {
            var xAxisScreenPosition = Random.Range(50, Screen.width - 50);
            var yAxisScreenPosition = Random.Range(50, Screen.height - Screen.height / 9);
            var screenPosition = new Vector2(xAxisScreenPosition, yAxisScreenPosition);
            var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            worldPosition.z = depth;
            return worldPosition;
        }
    }
}