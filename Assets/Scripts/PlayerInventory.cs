using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public enum Item
    {
        Ring
    }

    [System.Serializable]
    public class Items
    {
        public Item item;
        public string description;
        public float value;
        public float weight;

        public Items(Item item, string description, float value, float weight)
        {
            this.item = item;
            this.description = description;
            this.value = value;
            this.weight = weight;
        }
    }

    [HideInInspector]
    public List<Items> items = new List<Items>();
    
    void Start()
    {
        items.Add(new Items(Item.Ring, "", 50, 0.5f));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
