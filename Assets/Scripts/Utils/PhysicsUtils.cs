using System;
using UnityEngine;

namespace Utils
{
    public static class PhysicsUtils
    {
        public static bool Overlap(Vector2 circlePosition, float circleRadius, Vector2 rectPos,
            Vector2 rectSize)
        {
            return Overlap(circlePosition.x, circlePosition.y, circleRadius, rectPos.x - rectSize.x / 2,
                rectPos.y - rectSize.y / 2, rectSize.y, rectSize.x);
        }

        public static bool Overlap(float circleX, float circleY, float circleRadius, float rectX, float rectY,
            float rectHeight, float rectWidth)
        {
            var closestX = Math.Clamp(circleX, rectX, rectX + rectWidth);
            var closestY = Math.Clamp(circleY, rectY, rectY + rectHeight);
            var distanceX = circleX - closestX;
            var distanceY = circleY - closestY;
            var distanceSquared = distanceX * distanceX + distanceY * distanceY;
            return distanceSquared <= circleRadius * circleRadius;
        }
    }
}