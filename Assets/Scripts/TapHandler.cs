using System;
using UnityEngine;
using UnityEngine.Events;

public class TapHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private uint _doubleTapDeltaMS = 250;
    public bool Interactable;
    
    [Header("Events")]
    public UnityEvent<GameObject> OnSingleTap;
    public UnityEvent<GameObject> OnDoubleTap;

    private DateTime _lastTapTime = DateTime.MinValue;
    
    private void OnMouseDown()
    {
        if (!Interactable) return;
        
        OnSingleTap?.Invoke(gameObject);
        var timeSinceLastTapMS = (DateTime.Now - _lastTapTime).TotalMilliseconds;
        if (timeSinceLastTapMS <= _doubleTapDeltaMS)
        {
            OnDoubleTap?.Invoke(gameObject);
            _lastTapTime = DateTime.MinValue;
            return;
        }

        _lastTapTime = DateTime.Now;
    }
}
