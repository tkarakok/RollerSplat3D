using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMaanager : Singleton<GameMaanager>
{
    [Header("Level Texture")]
    [SerializeField] private Texture2D _levelTexture;
    [Header("Tiles Prefabs")]
    [SerializeField] private GameObject _prefabWall;
    [SerializeField] private GameObject _prefabRoad;
    [Header("Parent Object")]
    [SerializeField] private Transform _parent;

    public int roadCounts = 0;

    private Color _colorWall = Color.black;
    private Color _colorRoad = Color.white;

    private float _unitPerPixel;
    
    [HideInInspector] public List<Road> roadList = new List<Road>();
    [HideInInspector] public Road defaultBallRoad;


    private void Awake()
    {
        Generate();
        defaultBallRoad = roadList[0];
        PaintManager.Instance.Paint(defaultBallRoad,MovementController.Instance.paintedRoads);
    }

    void Generate()
    {
        _unitPerPixel = _prefabWall.transform.lossyScale.x;
        float _halfUnitPerPixel = _unitPerPixel / 2;

        float _width = _levelTexture.width;
        float _height = _levelTexture.height;

        Vector3 _offset = (new Vector3(_width / 2f, 0f, _height / 2f) * _unitPerPixel)
                       - new Vector3(_halfUnitPerPixel, 0f, _halfUnitPerPixel);

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                // get color per pixel
                Color _pixelColor = _levelTexture.GetPixel(i, j);

                Vector3 _spawnPos = ((new Vector3(i, 0, j) * _unitPerPixel) - _offset);

                // check color

                if (_pixelColor == _colorWall)
                {
                    Spawn(_prefabWall,_spawnPos);
                }
                else if (_pixelColor == _colorRoad)
                {
                    Spawn(_prefabRoad, _spawnPos);
                    roadCounts++;
                }


            }
        }

    }

    void Spawn(GameObject gameObject, Vector3 position)
    {
        position.y = gameObject.transform.position.y;

        GameObject spawnObject = Instantiate(gameObject, position, Quaternion.identity, _parent);

        if (gameObject == _prefabRoad)
        {
            roadList.Add(spawnObject.GetComponent<Road>());
        }
    }

}
