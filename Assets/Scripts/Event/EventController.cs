using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    private Action baseEvent;

    public void AddEventlistener(Action listener) => baseEvent += listener;

    public void RemoveEventlistener(Action listener) => baseEvent -= listener;

    public void InvokeEvent(Action listener) => baseEvent?.Invoke();

}
