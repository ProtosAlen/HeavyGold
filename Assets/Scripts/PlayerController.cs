using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float moneyTotal;
    float weightTotal;

    public Text moneyText;
    public Text weightText;

    //Items
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
        public int count;

        public Items(Item item, string description, float value, float weight, int count)
        {
            this.item = item;
            this.description = description;
            this.value = value;
            this.weight = weight;
            this.count = count;
        }
    }

    [HideInInspector]
    public List<Items> items = new List<Items>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        items.Add(new Items(Item.Ring, "", 125, 0.27f, 0));

        InvokeRepeating("UpdateText", 0, 1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            items[other.GetComponent<ItemScript>().itemID].count++;
            other.gameObject.SetActive(false);
            UpdateText();
        }
    }

    //Player Controlling
    public float speed;
    public float sprintSpeed;
    public float rotationSpeed;
    public float jumpPwr;
    private Rigidbody rb;

    private bool isSprinting;

    private Transform target;

    void FixedUpdate()
    {
        //Rotation
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = targetRotation;
        }
        
        //Moving
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Sprint
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) // Stop Sprint
        {
            isSprinting = false;
        }

        if (Input.GetKey(KeyCode.W)) // Forward
        {
            if (isSprinting)
            {
                rb.MovePosition(transform.position + transform.forward * Time.deltaTime * sprintSpeed);
            }
            else
            {
                rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Jump
        {
            rb.AddForce(Vector3.up * jumpPwr);
        }

        //Cam Follow
        Camera.main.transform.position = transform.position + new Vector3(0, 20);
    }
    private void UpdateText()
    {
        float tempMoney = 0;
        float tempWeight = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].count != 0)
            {
                tempMoney += items[i].value * items[i].count;
                tempWeight += items[i].weight * items[i].count;
            }
        }

        moneyTotal = tempMoney;
        weightTotal = tempWeight;

        moneyText.text = "$" + moneyTotal.ToString("F2");
        weightText.text = weightTotal.ToString("F2") + "kg";
    }
}
