using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class RemapToScreenOriginProcessor : InputProcessor<Vector2>
{
#if UNITY_EDITOR
    static RemapToScreenOriginProcessor()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        InputSystem.RegisterProcessor<RemapToScreenOriginProcessor>();
    }

    public override Vector2 Process(Vector2 value, InputControl control)
    {
        float x = value.x - (Camera.main.pixelWidth * 0.5f);
        float y = value.y - (Camera.main.pixelHeight * 0.5f);

        return new Vector2(x, y);
    }
}
