using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Droppable))]
public class Cell : MonoBehaviour
{
    [HideInInspector] public int Order;


    private Droppable _droppable;
    private List<Piece> _pieces;

    public void Awake()
    {
        Order = transform.GetSiblingIndex();
        _droppable = GetComponent<Droppable>();
    }

    public void Start()
    {
        _droppable.OnDropped.AddListener(HandleDrop);
    }

    private void HandleDrop(GameObject go)
    {
        Debug.Log("Dropped on me");
    }

    public void ReceivePiece(Piece piece)
    {
        _pieces.Add(piece);
    }

    public void RemovePiece(Piece piece)
    {
        if (_pieces.Contains(piece)) _pieces.Remove(piece);
        else Debug.LogError("Tried to remove piece, but we don't have it", gameObject);
    }
}