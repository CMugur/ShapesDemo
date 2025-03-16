using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameView _gameView;
    [SerializeField] private Shape _shapePrefab;
    private readonly List<Shape> _shapeInstances = new();
    [SerializeField] private Timer _timer;
    
    [SerializeField] private int _shapesToSpawn = 10;
    [SerializeField] private int _roundTimeMS = 10000;
    [SerializeField] private int _delayBetweenRoundsMS = 2000;
    [SerializeField] private int _mistakeTimerPenaltyMS = 500;
    [SerializeField] private int _instructionsMS = 4000;
    private int _shapeWithSidesCountToTap = -1;
    
    private void Start()
    {
        _timer.OnTimerUpdatedNormalized += _gameView.OnRoundTimerUpdated;
        _timer.OnTimerFinished += OnTimerFinished;
        
        _gameView.ShowInstructions();
        _timer.StartTimer(_delayBetweenRoundsMS);        
    }

    private void OnTimerFinished()
    {
        if (_timer.TotalMS == _roundTimeMS)
        {
            EndRound(true);
            return;
        }
        
        if (_timer.TotalMS == _delayBetweenRoundsMS || _timer.TotalMS == _instructionsMS)
        {
            StartNewRound();
            return;
        }
    }

    private void StartNewRound()
    {
        SpawnShapes(_shapesToSpawn);
        _shapeWithSidesCountToTap = _shapeInstances[Random.Range(0, _shapeInstances.Count)].SidesCount;
        _gameView.OnRoundStart(ShapeHelper.GetShapeName(_shapeWithSidesCountToTap));
        _gameView.OnRoundTimerUpdated(1);
        _timer.StartTimer(_roundTimeMS);
    }

    private void EndRound(bool isGameOver)
    {
        _timer.Pause();
        
        foreach (var shape in _shapeInstances)
        {
            if (shape.TryGetComponent<TapHandler>(out var tapHandler))
            {
                tapHandler.Interactable = false;
            }
        }

        _gameView.OnRoundEnd(isGameOver);
        _timer.StartTimer(_delayBetweenRoundsMS);
    }

    private void SpawnShapes(int count)
    {
        ClearShapes();
        while (count > 0)
        {
            var shape = Instantiate(_shapePrefab, transform);
            shape.DrawPolygon(Random.Range(ShapeHelper.MIN_SIDES_COUNT, ShapeHelper.MAX_SIDES_COUNT + 1));
            shape.transform.position = ScreenUtils.GetRandomPositionInsideCameraView(0);
            shape.GetComponent<TapHandler>().OnDoubleTap.AddListener(OnDoubleTapShape);
            shape.SetRandomColor();
            _shapeInstances.Add(shape);
            count--;
        }
    }

    private void ClearShapes()
    {
        foreach (var shape in _shapeInstances)
        {
            Destroy(shape.gameObject);
        }
        
        _shapeInstances.Clear();
    }

    private void OnDoubleTapShape(GameObject go)
    {
        if (!go.TryGetComponent<Shape>(out var shape) || shape.SidesCount != _shapeWithSidesCountToTap)
        {
            _timer.UpdateRemainingMS(-_mistakeTimerPenaltyMS);
            _gameView.OnPenaltyApplied();
            return;
        }
        EndRound(false);
    }
}


