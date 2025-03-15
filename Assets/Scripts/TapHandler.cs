using System;
using UnityEngine;
using UnityEngine.Events;

public class TapHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private uint _doubleTapDeltaMS = 250;
    
    [Header("Events")]
    public UnityEvent OnSingleTap;
    public UnityEvent OnDoubleTap;

    private DateTime _lastTapTime = DateTime.MinValue;
    
    private void OnMouseDown()
    {
        OnSingleTap?.Invoke();
        var timeSinceLastTapMS = (DateTime.Now - _lastTapTime).TotalMilliseconds;
        if (timeSinceLastTapMS <= _doubleTapDeltaMS)
        {
            OnDoubleTap?.Invoke();
            _lastTapTime = DateTime.MinValue;
            return;
        }

        _lastTapTime = DateTime.Now;
    }
}
