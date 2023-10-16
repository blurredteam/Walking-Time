using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private float _spacing;
    [SerializeField] private int _numberOfPaths;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private LineRenderer _lineRendererPrefab;

    [SerializeField] private Camera _cam;

    //private Tile[,] _map;
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
            for(int x = 0; x < _map.Length/_height;x++)
            {
                for (int y = 0; y < _map.Length/_width; y++) _map[x, y].Color(Color.white);
            }

            for(int i = 0; i < _lines.Count; i++)
            {
                Destroy(_lines[i]);
            }
            
            for(int j = 0; j < _numberOfPaths; j++)
            {
                BuildPath(_map, Color.black);
            }
        }
    }

    void GenerateGrid()
    {
        _map = new Tile[_width, _height];

        int xGridPos = 0;
        int yGridPos = 0;

        int xInit = 200;
        int yInit = 200;

        for(float x = 0; x < _width*_spacing; x+=_spacing)
        {
            for(float y = 0; y < _height*_spacing; y+=_spacing)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x+xInit,y+yInit), Quaternion.identity, this.transform);
                spawnedTile.name = $"Tile {x/ _spacing} {y/ _spacing}";
                spawnedTile.Color(Color.white);
                spawnedTile.value = (int)(y/_spacing);

                _map[xGridPos, yGridPos] = spawnedTile;
                yGridPos++;
            }

            yGridPos = 0;
            xGridPos++;
        }

        for(int caminoIdx = 0; caminoIdx < _numberOfPaths; caminoIdx++) BuildPath(_map, Color.black);

        //_cam.transform.position = new Vector3(((float)_width/2 - 0.5f)*_spacing, ((float)_height / 2 -0.5f)*_spacing, -10);
        //_cam.orthographicSize = 5 * _spacing;

    }

    public void BuildPath(Tile[,] map, Color color)
    {
        List<Tile> path = new List<Tile>();
        int seed = Random.Range(0, _height);
        int selectedTile = seed;

        for (int x = 0; x < _width; x++)
        {
            int offset = Random.Range(-1, 2);   // El offset define si ira hacia arriba (1) recto (0) o abajo (-1)

            selectedTile = SelectTile(x, selectedTile, offset, map);

            map[x, selectedTile].Color(color);
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

    private void DrawPath(List<Tile> path, List<GameObject> lines)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            path[i].AdyacentList.Add(path[i + 1]);
            var line = Instantiate(_lineRendererPrefab, this.transform.position, Quaternion.identity, this.transform);
            line.SetPosition(0, path[i].transform.position);
            line.SetPosition(1, path[i + 1].transform.position);
            _lines.Add(line.gameObject);
        }
    }
}
