using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {
    private ReactiveProperty<int> m_Timer = new ReactiveProperty<int>(5);
    [SerializeField]
    private Text m_TimerText;

    public ReadOnlyReactiveProperty<int> pTimer => m_Timer.ToReadOnlyReactiveProperty();

    private void Start() {
        m_Timer.SubscribeToText(m_TimerText, t => m_TimerText.text = t.ToString()).AddTo(this);
        CountStart();
    }

    private void CountStart() {
        var secound = TimeSpan.FromSeconds(1);
        var counter = Observable.Interval(secound).Subscribe(_ => m_Timer.Value--).AddTo(this);
        m_Timer.ObserveEveryValueChanged(x => x.Value).Where(x => x < 1).Subscribe(_ => counter.Dispose());
    }

}
