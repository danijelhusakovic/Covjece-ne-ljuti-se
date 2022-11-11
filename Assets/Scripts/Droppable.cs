using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    [HideInInspector] bool CanBeDropped;
    public Transform Transform;
    public GameObjectEvent OnDropped;

    public void Awake()
    {
        CanBeDropped = true;
        Transform = transform;
        OnDropped = new GameObjectEvent();
    }
}
