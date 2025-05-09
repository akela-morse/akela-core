﻿using UnityEngine;

namespace Akela.Globals
{
    [Icon("Packages/com.akelamorse.akelacore/Editor/EditorResources/CameraSetter Icon.png")]
    [AddComponentMenu("Globals/Camera Reference Setter", 2)]
    public sealed class CameraReferenceSetter : ReferenceSetterBase<GlobalCameraReference, Camera>
    {
#if UNITY_EDITOR
        private void Reset()
        {
            _value = GetComponent<Camera>();
        }
#endif
    }
}