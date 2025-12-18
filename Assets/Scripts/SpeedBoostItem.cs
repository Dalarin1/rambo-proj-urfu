using System;
using UnityEngine;

public class SpeedBoostItem : MonoBehaviour, ICollectable
{
    public static event Action<float> OnCollect;
    private float speedMultiplyer = 1.5f;
    public void Collect()
    {
        if (OnCollect != null) { OnCollect.Invoke(speedMultiplyer); }
        Destroy(gameObject);
    }
}