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

            if(float.IsNaN(angle))
            {
                angle = 0.0f;
            }

            return angle;
        }

        public static float RadianClampAngle(float angle)
        {
            do
            {
                if (angle > 6.28319f)
                {
                    angle -= 6.28319f;
                }
                else if (angle < -6.28319f)
                {
                    angle += 6.28319f;
                }
            } while (angle > 6.28319f || angle < -6.28319f);

            if (angle > 3.14159f)
            {
                angle -= 6.28319f;
            }
            else if (angle < -3.14159f)
            {
                angle += 6.28319f;
            }

            return angle;
        }

        public static float DegreeClampAngle(float angle)
        {
            do
            {
                if (angle > 360.0f)
                {
                    angle -= 360.0f;
                }
                else if (angle < -360.0f)
                {
                    angle += 360.0f;
                }
            } while (angle > 360.0f || angle < -360.0f);

            if (angle > 180.0f)
            {
                angle -= 360.0f;
            }
            else if (angle < -180.0f)
            {
                angle += 360.0f;
            }

            return angle;
        }
    }
}
