using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Map_Generator: MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Vector2Int _gap;
    [SerializeField, Range(0,0.8f)] private float _skipAmount = 0.1f;
    [SerializeField, Range(0, 1f)] private float _forestAmount = 0.3f;
    [SerializeField] private GridType _gridType;
    [SerializeField] private ScriptableGridConfig[] _configs;


    private bool _requiresGeneration = true;
    private Camera _camera;
    private Grid _grid;

    private Vector3 _cameraPositionTarget;
    private Vector3 _moveVel;
    private float _cameraSizeVel;
    private float _cameraSizeTarget;

    private Vector2 _currentGap;
    private Vector2 _gapVel;




    private void Awake()
    {
        _grid = GetComponent<Grid>();
        _camera = Camera.main;
        _currentGap = _gap;        
    }


    private void OnValidate() => _requiresGeneration = true;


    private void LateUpdate()
    {
        if(Vector2.Distance(_currentGap, _gap) > 0.01f)
        {
            _currentGap = Vector2.SmoothDamp(_currentGap, _gap, ref _gapVel, 0.01f);
            _requiresGeneration = true;
        }

        if (_requiresGeneration) Generate();

        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, _cameraPositionTarget, ref _moveVel, 0.05f);
        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _cameraSizeTarget, ref _cameraSizeVel, 0.05f);
    }

    private void Generate()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var config = _configs.First(c => c.Type == _gridType);


        //_grid.cellLayout = config.Layout;
        _grid.cellSize = config.CellSize;

        if (_grid.cellLayout != GridLayout.CellLayout.Hexagon) _grid.cellGap = _currentGap;
        _grid.cellSwizzle = config.GridSwizzle;

        var coordinates = new List<Vector3Int>();

        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                coordinates.Add(new Vector3Int(x, y));
            }
        }


        var bounds = new Bounds();
        var skipCount = Mathf.FloorToInt(coordinates.Count * _skipAmount);
        var forestCount = Mathf.FloorToInt(coordinates.Count * _forestAmount);
        var index = 0;
        var rand = new Random(420);


        foreach (var coordinate in coordinates.OrderBy(t => rand.Next()).Take(coordinates.Count - skipCount))
        {
            var isForest = index++ < forestCount;
            var prefab = isForest ? config.ForestPrefab : config.ForestPrefab;
            var position = _grid.GetCellCenterWorld(coordinate);
            var spawned = Instantiate(prefab, position, Quaternion.identity, transform);
            //spawned.Init(coordinate);
            bounds.Encapsulate(position);
        }

        SetCamera(bounds);
        _requiresGeneration = false;

    }


    private void SetCamera(Bounds bounds)
    {
        bounds.Expand(2);

        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * _camera.pixelHeight / _camera.pixelWidth;

        _cameraPositionTarget = bounds.center + Vector3.back;
        _cameraSizeTarget = Mathf.Max(horizontal, vertical) * 0.5f;
    }

}



[CreateAssetMenu]
public class ScriptableGridConfig : ScriptableObject
{
    public GridType Type;
    [Space(10)]
    public GridLayout.CellSwizzle Layout;
    public GridTileBase GrassPrefab, ForestPrefab;
    public Vector3 CellSize;
    public GridLayout.CellSwizzle GridSwizzle;
}


[Serializable]
public enum GridType
{
    Rectangle,
    Isometric,
    HexagonPointy,
    HexagonFlat
}


public abstract class GridTileBase : MonoBehaviour
{
 
}
