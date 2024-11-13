﻿using System;
using System.Collections;
using Akela.Behaviours;
using Akela.Bridges;
using UnityEngine;

namespace Akela.Triggers
{
    [AddComponentMenu("Triggers/Delay Trigger", 6)]
    [HideScriptField]
    public class DelayTrigger : MonoBehaviour, ITrigger
    {
        #region Component Fields
        [SerializeField] float _time;
        [SerializeField] bool _startTimerImmediately;
        [Header("Events")]
        [SerializeField] BridgedEvent _onTimer;
        #endregion

        private Coroutine coroutine;

        public bool IsActive { get; private set; }

        public void AddListener(Action callback, TriggerEventType eventType = TriggerEventType.OnBecomeActive)
        {
            if (eventType != TriggerEventType.OnBecomeActive)
                return;
            
            _onTimer.AddListener(() => callback());
        }

        public void StartTimer()
        {
            if (_time <= 0f)
            {
                IsActive = true;
                _onTimer.Invoke();
                return;
            }

            coroutine = StartCoroutine(Timer());
        }

        public void StopTimer()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }

        #region Component Messages
        private void OnEnable()
        {
            if (_startTimerImmediately)
                StartTimer();
        }

#if  UNITY_EDITOR
        private void OnValidate()
        {
            if (_time < 0f)
                _time = 0f;
        }
#endif
        #endregion

        #region Private Methods
        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_time);

            IsActive = true;
            _onTimer.Invoke();
        }
        #endregion
    }
}
