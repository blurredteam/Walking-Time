using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] public int _width, _height;    // Numero de casillas x y numero de casillas y (x*y)
    [SerializeField] private int _numberOfPaths;    // Numero de caminos generados
    [SerializeField] private float _xSpacing;       // Cuanta separacion en x hay entre cada casilla
    [SerializeField] private float _ySpacing;       // Cuanta separacion en y hay entre cada casilla
    [SerializeField] private float _lineOffset;     // ajusta valor y de las lineas (para centrarlas en las casillas)
    [SerializeField] private float _minOffset;      // offset de las casillas para generacion mas organica
    [SerializeField] private float _maxOffset;
    [SerializeField] private float _xOrigin;        // Donde empiezan a generarse casillas
    [SerializeField] private float _yOrigin;        // si yOrigin = 1 empiezan a generarse en y = 1

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private LineRenderer _lineRendererPrefab;

    [SerializeField] private Camera _cam;

    private Tile[,] _map;

    private List<GameObject> _lines = new List<GameObject>();

    private void Start()
    {
        _map = new Tile[_width, _height];

        GenerateGrid();
    }

    // nuevos generacion al pulsar espacio (no funciona con todo)
    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        for (int x = 0; x < _map.Length / _height; x++)
    //            for (int y = 0; y < _map.Length / _width; y++)
    //                _map[x, y].ColorTile(Color.clear);
    //        for (int i = 0; i < _lines.Count; i++) Destroy(_lines[i]);
    //        for (int j = 0; j < _numberOfPaths; j++) BuildPath(_map, Color.white);
    //    }
    //}

    //Genera el grafo _width*_height, genera los caminos aleatorio y elimina las casillas sobrantes
    void GenerateGrid()
    {
        _map = new Tile[_width, _height];

        int xGridPos = 0;
        int yGridPos = 0;

        // 1. Se genera el grid
        for (float x = 0; x < _width * _xSpacing; x += _xSpacing)
        {
            for (float y = 0; y < _height * _ySpacing; y += _ySpacing)
            {
                float offset = Random.Range(_minOffset, _maxOffset);
                float xAdjust = -(_xOrigin + offset); 
                float yAdjust = -(_yOrigin + offset);

                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x + xAdjust, y + yAdjust), Quaternion.identity, this.transform);
                spawnedTile.name = $"Tile {xGridPos} {yGridPos}";
                spawnedTile.value = yGridPos;

                spawnedTile.AssignType(xGridPos);

                _map[xGridPos, yGridPos] = spawnedTile;
                yGridPos++;
            }

            yGridPos = 0;
            xGridPos++;
        }

        // 2. Se crean caminos aleatorios
        for (int caminoIdx = 0; caminoIdx < _numberOfPaths; caminoIdx++) BuildPath(_map);

        // 3. Se borran las casillas que sobran
        for (int x = 0; x < _map.Length / _height; x++)
            for (int y = 0; y < _map.Length / _width; y++)
                if (!_map[x, y].selected) Destroy(_map[x, y].gameObject); 

        // 4. Se instancia la casilla final
        var lastTile = Instantiate(_tilePrefab, new Vector3(_width * _xSpacing - 6, -1.7f), Quaternion.identity, this.transform);
        lastTile.name = $"TileFinal";
        lastTile.type = 100;
        lastTile.SetTileInfo();
   
        for (int y = 0; y < _map.Length / _width; y++)
        {
            if (_map[_width - 1, y].selected)
            {
                _map[_width - 1, y].AdyacentList.Add(lastTile);
                var line = Instantiate(_lineRendererPrefab, new Vector3(0, 0), Quaternion.identity, this.transform);
                line.SetPosition(0, _map[_width - 1, y].transform.position + new Vector3(0, _lineOffset));
                line.SetPosition(1, lastTile.transform.position + new Vector3(0, _lineOffset));
                _lines.Add(line.gameObject);
            }
        }

        // 5. Se asigna el mapa al nivel
        LevelManager.instance.SetMap(_map, _width, _height);
    }

    //Genera un camino aleatorio en funcion de seed y offset
    public void BuildPath(Tile[,] map)
    {
        List<Tile> path = new List<Tile>();
        int seed = Random.Range(0, _height);
        int selectedTile = seed;

        for (int x = 0; x < _width; x++)
        {
            int offset = Random.Range(-1, 2);   // El offset define si ira hacia arriba (1) recto (0) o abajo (-1)

            selectedTile = SelectTile(x, selectedTile, offset, map);

            map[x, selectedTile].selected = true;
            map[x, selectedTile].GetComponent<SpriteRenderer>().sortingOrder = (10 - selectedTile);
            path.Add(map[x, selectedTile]);
        }

        DrawPath(path, _lines);
    }

    private int SelectTile(int x, int y, int offset, Tile[,] map)
    {
        // Si es la primera casilla (x=0) || Si la casilla (y + offset) se sale del mapa || Si va recto (offset=0)
        if (x == 0 || y + offset > _height - 1 || y + offset < 0 || offset == 0) return y;

        Tile tileToCheck = map[x - 1, y + offset]; //Casilla a testear con la lista de adyacencias

        // Este for esta para que no se cruzen dos lineas
        // p.e si hay una linea del nodo [x,y] al nodo [x+1,y+1] no puede haber una linea del [x,y+1] al [x+1,y]  
        // Si la lista de adyacencias de la casilla a testear esta vacia no entra en el for
        for (int i = 0; i < tileToCheck.AdyacentList.Count; i++)
        {
            // Si alguna de las adyacencias de [x-1, y+offset] coincide con la casilla [x, y] devuelve y (offset = 0)
            if (tileToCheck.AdyacentList[i].value == y) return y;
        }

        return y + offset;
    }

    //Dibuja el camino con el prefab de linea y genera la lista de adyacencias
    //Evita que se repitan adyacencias con el primer if contains
    private void DrawPath(List<Tile> path, List<GameObject> lines)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            if (!path[i].AdyacentList.Contains(path[i + 1])) path[i].AdyacentList.Add(path[i + 1]);
            var line = Instantiate(_lineRendererPrefab, new Vector3(0, 0), Quaternion.identity, this.transform);
            line.SetPosition(0, new Vector3(path[i].transform.position.x, path[i].transform.position.y + _lineOffset));
            line.SetPosition(1, new Vector3(path[i+1].transform.position.x, path[i+1].transform.position.y + _lineOffset));
            _lines.Add(line.gameObject);

            // Lineas que van a las primeras casillas
            if (path[i].position == 0)
            {
                var originLine = Instantiate(_lineRendererPrefab, new Vector3(0, 0), Quaternion.identity, this.transform);
                originLine.SetPosition(0, new Vector3(path[i].transform.position.x - 5, path[i].transform.position.y + _lineOffset));
                originLine.SetPosition(1, new Vector3(path[i].transform.position.x, path[i].transform.position.y + _lineOffset));
                _lines.Add(originLine.gameObject);
            }
        }
    }
}
