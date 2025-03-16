using UnityEngine;

namespace Utils
{
    public static class ScreenUtils
    {
        /// <summary>
        /// Generates points that are inside the screen rect and translates them into world space
        /// </summary>
        /// <param name="depth">The z axis in world space</param>
        /// <returns></returns>
        public static Vector3 GetRandomPositionInsideCameraView(float depth)
        {
            var xAxisScreenPosition = Random.Range(50, Screen.width - 50);
            var yAxisScreenPosition = Random.Range(70, Screen.height - Screen.height / 6);
            var screenPosition = new Vector2(xAxisScreenPosition, yAxisScreenPosition);
            var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            worldPosition.z = depth;
            return worldPosition;
        }
    }
}