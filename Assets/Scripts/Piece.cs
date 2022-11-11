using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class Piece : MonoBehaviour
{
    private Draggable _draggable;

    public void Awake()
    {
        _draggable = GetComponent<Draggable>();
    }

    public void Start()
    {
        _draggable.OnStartDrag.AddListener(HandleStartDrag);
        _draggable.OnStopDrag.AddListener(HandleStopDrag);
    }

    private void HandleStartDrag(GameObject go)
    {
        //bude pozvan
    }

    private void HandleStopDrag(GameObject go)
    {
        //bude pozvan
    }
}


public enum PlayerColor
{
    Red,
    Blue,
    Yellow,
    Green
}

public enum CellType
{
    Regular,
    Immune,
    Home
}