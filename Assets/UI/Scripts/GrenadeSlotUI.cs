using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSlotUI : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI grenadeAmount;

    public void SetGrenadeAmount(int amount)
    {
        grenadeAmount.SetText(amount.ToString());
    }
}
