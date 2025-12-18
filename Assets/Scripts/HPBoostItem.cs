using System;
using UnityEngine;

public class HPBoostItem : MonoBehaviour, ICollectable
{
    public static event Action<int> OnCollect;
    [SerializeField] public int healAmount = 2; 
    public void Collect()
    {
        if (OnCollect != null) { OnCollect.Invoke(healAmount); }
        Destroy(gameObject);
    }
}
