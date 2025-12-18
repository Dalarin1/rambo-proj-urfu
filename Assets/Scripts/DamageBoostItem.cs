using System;
using UnityEngine;

public class DamageBoostItem: MonoBehaviour, ICollectable
{
    public static event Action<float> OnCollect;
    private float dmgMultiplyer = 10f;
    public void Collect()
    {
        if (OnCollect != null) { OnCollect.Invoke(dmgMultiplyer); }
        Destroy(gameObject);
    }
}