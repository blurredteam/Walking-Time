using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] public int _width, _height;
    [SerializeField] private float _X_spacing;
    [SerializeField] private float _Y_spacing;
    [SerializeField] private int _numberOfPaths;

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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int x = 0; x < _map.Length / _height; x++)
            {
                for (int y = 0; y < _map.Length / _width; y++) _map[x, y].ColorTile(Color.clear);
            }

            for (int i = 0; i < _lines.Count; i++)
            {
                Destroy(_lines[i]);
            }

            for (int j = 0; j < _numberOfPaths; j++)
            {
                BuildPath(_map, Color.white);
            }
        }
    }

    //Genera el grafo _width*_height, genera los caminos aleatorio y elimina las casillas sobrantes
    void GenerateGrid()
    {
        _map = new Tile[_width, _height];

        int xGridPos = 0;
        int yGridPos = 0;

        // 1. Se genera el grid
        for (float x = 0; x < _width * _X_spacing; x += _X_spacing)
        {
            for (float y = 0; y < _height * _Y_spacing; y += _Y_spacing)
            {
                float offset = Random.Range(-0.35f, 0.35f);
                float offsetX = 7 + offset;
                float offsetY = 4 + offset;

                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x - offsetX, y - offsetY), Quaternion.identity, this.transform);
                spawnedTile.name = $"Tile {x / (_X_spacing)} {y / _Y_spacing}";
                spawnedTile.value = (int)(y / _Y_spacing);

                spawnedTile.AssignType(xGridPos);

                _map[xGridPos, yGridPos] = spawnedTile;
                yGridPos++;
            }

            yGridPos = 0;
            xGridPos++;
        }

        // 2. Se crean caminos aleatorios
        for (int caminoIdx = 0; caminoIdx < _numberOfPaths; caminoIdx++) BuildPath(_map, Color.white);

        // 3. Se borran las casillas que sobran
        for (int x = 0; x < _map.Length / _height; x++)
        {
            for (int y = 0; y < _map.Length / _width; y++) {
                if (!_map[x, y].selected) {
                    Destroy(_map[x, y].gameObject); //_map[x, y].ColorTile(Color.clear)
                }  
            } 
        }

        // 4. Se instancia la casilla final
        var lastTile = Instantiate(_tilePrefab, new Vector3(_width * (_X_spacing) - 7, 0), Quaternion.identity, this.transform);
        lastTile.name = $"TileFinal";
        lastTile._clickEvent.enabled = false;

        for (int y = 0; y < _map.Length / _width; y++)
        {
            if (_map[_width-1, y].selected)
            {
                _map[_width - 1, y].AdyacentList.Add(lastTile);
                var line = Instantiate(_lineRendererPrefab, new Vector3(0, 0), Quaternion.identity, this.transform);
                line.SetPosition(0, _map[_width - 1, y].transform.position);
                line.SetPosition(1,lastTile.transform.position);
                _lines.Add(line.gameObject);
            }
        }
        
        // 5. Se asigna el mapa al nivel
        LevelManager.instance.SetMap(_map, _width, _height);

    }

    //Genera un camino aleatorio en funcion de seed y offset
    public void BuildPath(Tile[,] map, Color color)
    {
        List<Tile> path = new List<Tile>();
        int seed = Random.Range(0, _height);
        int selectedTile = seed;

        for (int x = 0; x < _width; x++)
        {
            int offset = Random.Range(-1, 2);   // El offset define si ira hacia arriba (1) recto (0) o abajo (-1)

            selectedTile = SelectTile(x, selectedTile, offset, map);

            map[x, selectedTile].ColorTile(color);
            map[x, selectedTile].selected = true;
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
            var line = Instantiate(_lineRendererPrefab, new Vector3(0,0), Quaternion.identity, this.transform);
            line.SetPosition(0, path[i].transform.position);
            line.SetPosition(1, path[i + 1].transform.position);
            _lines.Add(line.gameObject);
        }
    }
}
