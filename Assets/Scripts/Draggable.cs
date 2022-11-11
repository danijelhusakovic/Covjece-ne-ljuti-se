using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObjectEvent OnStartDrag;
    public GameObjectEvent OnStopDrag;
    [HideInInspector] public bool CanBeDragged;
    [HideInInspector] public Transform Transform;

    [SerializeField] private bool _snapToMouse;

    private Vector3 _mousePos;
    private bool _isDragged;
    private Vector3 _distanceFromCenter;
    private List<Droppable> _list;
    private Vector3 _startingPosition;

    public void Awake()
    {
        CanBeDragged = true;
        Transform = transform;
        _startingPosition = Transform.position;
        _list = new List<Droppable>();
        _isDragged = false;
        OnStartDrag = new GameObjectEvent();
        OnStopDrag = new GameObjectEvent();
    }

    public void Update()
    {
        _mousePos = Input.mousePosition;
        _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);


        if (!_isDragged) return;
        if (!CanBeDragged) return;
        if (_snapToMouse)
        {
            Transform.position = new Vector3(_mousePos.x, _mousePos.y, 0f);
        }
        else
        {
            Transform.position = new Vector3(_mousePos.x + _distanceFromCenter.x, _mousePos.y + _distanceFromCenter.y, 0f);
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _startingPosition = Transform.position;
        _distanceFromCenter = Transform.position - _mousePos;
        _isDragged = true;
        OnStartDrag.Invoke(gameObject);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (_list.Count <= 0) 
        {
            Transform.position = _startingPosition;
        }
        else if(_list.Count == 1)
        {
            Transform.position = _list[0].Transform.position;
            _list[0].OnDropped.Invoke(gameObject);
        }
        else
        {
            Transform.position = FindClosest();
        }
        _isDragged = false;
        OnStopDrag.Invoke(gameObject);
    }

    private Vector3 FindClosest()
    {
        // Had to initialize it. Vector3.zero bears no value in computing.
        Vector3 closestPosition = Vector3.zero;

        // Setting the first minDistance to the maximum possible.
        // So when we first compare it with another, another will be closer to us than starting.
        float minDistance = Mathf.Infinity;
        float currentDistance;

        foreach (Droppable droppable in _list)
        {
            currentDistance = Vector3.Distance(droppable.Transform.position, Transform.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                closestPosition = droppable.Transform.position;
            }
        }

        return closestPosition;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Droppable droppable = other.GetComponent<Droppable>();
        if (droppable == null) return;
        _list.Add(droppable);
        //Debug.Log(_list.Count);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Droppable droppable = other.GetComponent<Droppable>();
        if (droppable == null) return;
        if (_list.Contains(droppable)) _list.Remove(droppable);
        //Debug.Log(_list.Count);
    }

}

public class GameObjectEvent: UnityEvent<GameObject> { }