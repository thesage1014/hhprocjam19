using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util {

    public static class Util
    {
        public static Vector2 xz (this Vector3 vec3) {
            return new Vector2(vec3.x, vec3.z);
        }
        public static Vector2 xy(this Vector3 vec3) {
            return new Vector2(vec3.x, vec3.y);
        }
        public static Vector2Int xz(this Vector3Int vec3) {
            return new Vector2Int(vec3.x, vec3.z);
        }
        public static Vector2Int xy(this Vector3Int vec3) {
            return new Vector2Int(vec3.x, vec3.y);
        }
    }
}
