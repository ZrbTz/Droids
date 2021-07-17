using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTable {
    private static Dictionary<string, UnityEvent> events = new Dictionary<string, UnityEvent>();

    /*private static EventTable instance;
    public static EventTable Instance { get => instance; }

    private void OnEnable() {
        instance = this;
    }*/

    public static void AddEvent(string eventName, UnityEvent eventObject) {
        events[eventName] = eventObject;
    }

    public static void AddListener(string eventName, UnityAction listener) {
        if (events.TryGetValue(eventName, out UnityEvent eventObject))
            eventObject.AddListener(listener);
        else
            Debug.LogError("Event " + eventName + " not found");
    }

    public static void RemoveListener(string eventName, UnityAction listener) {
        if (events.TryGetValue(eventName, out UnityEvent eventObject))
            eventObject.RemoveListener(listener);
        else
            Debug.LogError("Event " + eventName + " not found");
    }
}
