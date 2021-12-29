using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using DG.Tweening;
using System.Linq;

public class MovementController : Singleton<MovementController>
{
    public List<Road> paintedRoads = new List<Road>();

    [SerializeField] private LayerMask wallsAndRoadsLayer;
    [SerializeField] private float _stepDuration = .1f;
    [SerializeField] Ease ease;

    private const float MAX_RAY_DISTANCE = 100f;
    
    private Vector3 _direction;
    private bool _canMove = true;


    private void Start()
    {
        //change default position
        transform.position = GameManager.Instance.defaultBallRoad.transform.position;
        
        SwipeListener.Instance.OnSwipe.AddListener(swipe =>
        {
            switch (swipe)
            {
                case "Right":
                    _direction = Vector3.right;
                    break;
                case "Left":
                    _direction = Vector3.left;
                    break;
                case "Up":
                    _direction = Vector3.forward;
                    break;
                case "Down":
                    _direction = Vector3.back;
                    break;
            }
            PlayerMove();
        });
    }

    void PlayerMove()
    {
        if (StateManager.Instance.state == State.InGame)
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, _direction, MAX_RAY_DISTANCE, wallsAndRoadsLayer.value)
                                    .OrderBy(hit => hit.distance).ToArray();


            for (int i = 0; i < hits.Length; i++)
            {
                if (!hits[i].transform.gameObject.CompareTag("Wall"))
                {
                    Road paintObject = hits[i].transform.GetComponent<Road>();
                    if (!paintObject.isPainted)
                    {
                        PaintManager.Instance.Paint(paintObject, paintedRoads);

                    }
                }
                else
                {
                    if (i == 0)
                    {
                        return;
                    }
                    else
                    {
                        int _steps = i;
                        Vector3 _targetPosition = hits[i - 1].transform.position;
                        float _moveDuration = _steps * _stepDuration;
                        transform
                          .DOMove(_targetPosition, _moveDuration)
                          .SetEase(ease);
                        break;
                    }
                }
            }


        }
    }
}
