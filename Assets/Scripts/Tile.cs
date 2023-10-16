using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    public int value { get; set; }
    public List<Tile> AdyacentList= new List<Tile>();

    public void Color(Color color)
    {
        _renderer.color = color;
    }
}
