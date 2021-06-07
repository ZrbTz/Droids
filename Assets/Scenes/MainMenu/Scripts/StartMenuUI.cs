using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StartMenuUI : MonoBehaviour
{
    public void ShowPage(GameObject page)
    {
        page.SetActive(true);
        gameObject.SetActive(false);
    }
}
