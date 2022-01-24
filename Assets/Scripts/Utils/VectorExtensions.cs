using UnityEngine;

namespace Utils
{
    public static class VectorExtensions
    {
        public static Vector3 ToX0Z(this Vector3 v, float y = 0) => new Vector3(v.x, y, v.z);
    }
}