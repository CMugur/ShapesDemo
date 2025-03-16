using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private const int _timerStepMS = 10;
    
    public TimerState State { get; private set; }

    public event Action<int> OnTimerUpdated;
    public event Action<float> OnTimerUpdatedNormalized;
    public event Action OnTimerFinished;
    
    public int TotalMS { get; private set; }
    public int RemainingMS { get; private set; }

    private Coroutine _tick;
    
    public void StartTimer(int totalMS, int remainingMS = -1)
    {
        if (_tick != null) StopCoroutine(_tick);
        State = TimerState.Running;
        TotalMS = math.clamp(totalMS, 1, int.MaxValue);
        RemainingMS = remainingMS == -1 ? TotalMS : Math.Clamp(remainingMS, 0, TotalMS);
        _tick = StartCoroutine(Tick());
    }

    public void Pause()
    {
        if(State != TimerState.Running) return;
        if (_tick != null) StopCoroutine(_tick);
        State = TimerState.Paused;
    }

    public void Resume()
    {
        if(State != TimerState.Paused) return;
        State = TimerState.Running;
        _tick = StartCoroutine(Tick());
    }

    public void Finish()
    {
        if (_tick != null) StopCoroutine(_tick);
        State = TimerState.Finished;
        OnTimerFinished?.Invoke();
    }

    private IEnumerator Tick()
    {
        while (RemainingMS > 0)
        {
            yield return new WaitForSeconds(_timerStepMS / 1000f);
            UpdateRemainingMS(-_timerStepMS);
            if (RemainingMS <= 0) Finish();
        }
    }

    public void UpdateRemainingMS(int amount)
    {
        RemainingMS += amount;
        RemainingMS = Math.Clamp(RemainingMS, 0, TotalMS);
        
        OnTimerUpdated?.Invoke(RemainingMS);
        OnTimerUpdatedNormalized?.Invoke(RemainingMS / (1f * TotalMS));
    }
}

public enum TimerState
{
    Ready,
    Running,
    Paused,
    Finished
}
