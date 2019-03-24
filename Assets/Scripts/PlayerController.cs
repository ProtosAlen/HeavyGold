using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private MainScript main;

    //Items
    [System.Serializable]
    public enum Item
    {
        Ring,
        Liroconite,
        Dagger,
        Necklace,
        Stick,
        Crown
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
        main = GetComponent<MainScript>();
        rb = GetComponent<Rigidbody>();

        items.Add(new Items(Item.Ring, "", 125, 0.27f, 0));
        items.Add(new Items(Item.Liroconite, "", 125, 0.27f, 0));
        items.Add(new Items(Item.Necklace, "", 125, 0.27f, 0));
        items.Add(new Items(Item.Ring, "", 125, 0.27f, 0));
        items.Add(new Items(Item.Stick, "", 125, 0.27f, 0));
        items.Add(new Items(Item.Crown, "", 125, 0.27f, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            items[other.GetComponent<ItemScript>().itemID].count++;
            other.gameObject.SetActive(false);
            main.UpdateText();
        }
        if (other.gameObject.CompareTag("SellPoint"))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].count != 0)
                {
                    main.moneyTotal += items[i].value * items[i].count;
                    main.weightTotal -= items[i].weight * items[i].count;
                    items[i].count = 0;
                    main.UpdateText();
                }
            }
        }
        if (other.gameObject.CompareTag("FinishPoint"))
        {
            main.GoToPoint(other.GetComponent<ItemScript>().itemID);
        }
        if (other.gameObject.CompareTag("Death"))
        {
           //TODO DEAD
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



        //Cam Follow
        Camera.main.transform.position = transform.position + new Vector3(0, 20);
    }

    bool isGrounded;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 2, 0) * jumpPwr, ForceMode.Impulse);
            isGrounded = false;
        }

    }
}
