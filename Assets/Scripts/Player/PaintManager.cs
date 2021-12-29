using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaintManager : Singleton<PaintManager>
{
    [SerializeField] private Renderer _ballRenderer;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;
    
    public void Paint(Road road, List<Road> paintedRoads)
    {
        road.meshRenderer.material
        .DOColor(_ballRenderer.material.color, _duration)
        .SetDelay(_delay);

        road.isPainted = true;
        paintedRoads.Add(road);

        if (GameManager.Instance.roadCounts == MovementController.Instance.paintedRoads.Count)
        {

            PlayerPrefs.SetInt("Level", (LevelManager.Instance.CurrentLevel + 1));
            StateManager.Instance.state = State.EndGame;
        }
    }
}
