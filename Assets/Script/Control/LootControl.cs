using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootControl : MonoBehaviour
{
    private string lootName;
    private string lootType;
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, ItemManager.Instance.itemNames.Count);
        Debug.Log(ItemManager.Instance.itemNames.Count);
        lootName = ItemManager.Instance.itemNames[index];
        lootType = ItemManager.Instance.itemTypes[index];
        Debug.Log(index);
        // gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>((string)ItemManager.Instance.propValue[lootName]["path"]);
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>((string)ItemManager.Instance.GetPropertyByName(lootName, lootType, "path"));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("in");
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player");
            GameManager.Instance.Add(lootName, lootType);
            Destroy(gameObject);
        }
    }
}
