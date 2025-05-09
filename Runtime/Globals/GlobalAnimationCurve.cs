﻿using Akela.Behaviours;
using UnityEngine;

namespace Akela.Globals
{
    [HideScriptField]
    [Icon("Packages/com.akelamorse.akelacore/Editor/EditorResources/GlobalCurveAsset Icon.png")]
    [CreateAssetMenu(fileName = "New AnimationCurve", menuName = "Globals/AnimationCurve", order = -92)]
    public sealed class GlobalAnimationCurve : GlobalBase<AnimationCurve>
    {

    }
}