using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{
    [SerializeField] List<LootItem> LootTable = new();
    // Start is called before the first frame update
    public void TakeDamage(float damage)
    {
        DropLoot();
        Destroy(gameObject);
    }
    private void DropLoot()
    {
        foreach (var item in LootTable)
        {
            if (Random.Range(0, 100) < item.chance)
            {
                InstantiateLoot(item.itemPrefab);
                break;
            }
        }        }
    private void InstantiateLoot(GameObject go)
    {
        if (go)
        {
            GameObject droppedLoot = Instantiate(go, transform.position, Quaternion.identity);

        }
    }
}
