using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Suggestion : ScriptableObject
{
    [SerializeField]
    protected string title;
    [SerializeField]
    protected string description;
    [SerializeField]
    protected bool isBlockingWaves;
    [SerializeField]
    protected bool visible;

    public abstract bool IsCompleted(GameObject player);

    public string GetTitle()
    {
        return title;
    }

    public string GetDescription()
    {
        return description.Replace("\\n", "\n");
    }

    public bool isBlocking() {
        return isBlockingWaves;
    }

    public bool isVisible() {
        return visible;
    }
}