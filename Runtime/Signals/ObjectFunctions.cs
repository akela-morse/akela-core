﻿using System.Collections.Generic;
using Akela.Bridges;
using UnityEngine;

namespace Akela.Signals
{
    [DisallowMultipleComponent]
    [Icon("Packages/com.akelamorse.akelacore/Editor/EditorResources/ObjectFunctions Icon.png")]
    [AddComponentMenu("Signals/Object Functions", 4)]
    public class ObjectFunctions : MonoBehaviour, ISerializationCallbackReceiver
    {
        #region Component Fields
        [SerializeField, HideInInspector] List<string> _keys = new();
        [SerializeField, HideInInspector] List<BridgedEvent> _values = new();
        #endregion

        private readonly Dictionary<string, BridgedEvent> _functions = new();

        public void Call(string name)
        {
            if (!_functions.TryGetValue(name, out var function))
                return;

            function.Invoke();
        }

        #region ISerializationCallbackReceiver
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();

            foreach (var entries in _functions)
            {
                _keys.Add(entries.Key);
                _values.Add(entries.Value);
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _functions.Clear();

            for (var i = 0; i < _keys.Count; ++i)
            {
#if UNITY_EDITOR
                if (!_functions.TryAdd(_keys[i], _values[i]))
                    Debug.LogError($"Issue while serializing ObjectFunctions. Function name '{_keys[i]}' already exists.", this);
#else
                _functions.Add(_keys[i], _values[i]);
#endif
            }
        }
        #endregion
    }
}