using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace FireFighter
{
    public static class Utilities
    {
        public static float RadianAngleSigned(float3 from, float3 to)
        {
            float angle = math.acos(math.dot(math.normalize(from), math.normalize(to)));
            float3 cross = math.cross(from, to);
            angle *= math.sign(math.dot(math.up(), cross));
            return angle;
        }
    }
}
