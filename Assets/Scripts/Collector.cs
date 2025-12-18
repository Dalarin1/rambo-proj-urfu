using UnityEngine;

public class Collector: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable item = collision.GetComponent<ICollectable>();
        if (item != null)
        {
            item.Collect();
        }
    }
}