// Date   : 13.10.2018 12:42
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryManager : MonoBehaviour
{

    private List<CollectableKey> keys = new List<CollectableKey>();

    void Start()
    {

    }

    void Update()
    {

    }

    public void AddKey(CollectableKey key)
    {
        keys.Add(key);
    }

    public bool UseKey(KeyType keyType)
    {
        CollectableKey key = keys.Find(k => k.KeyType == keyType);
        if (key != null)
        {
            keys.Remove(key);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Key")
        {
            AddKey(collision.GetComponentInParent<CollectableKey>().Collect());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            Door door = collision.gameObject.GetComponent<Door>();
            if (UseKey(door.RequiredKeyType))
            {
                door.Open();
            }

        }
    }
}
