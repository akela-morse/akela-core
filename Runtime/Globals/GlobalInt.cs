﻿using Akela.Behaviours;
using UnityEngine;

namespace Akela.Globals
{
    [HideScriptField]
    [Icon("Packages/com.akelamorse.akelacore/Editor/EditorResources/GlobalIntAsset Icon.png")]
    [CreateAssetMenu(fileName = "New Integer", menuName = "Globals/Integer", order = -100)]
    public sealed class GlobalInt : GlobalBase<int>
    {

    }
}