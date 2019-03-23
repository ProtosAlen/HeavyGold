using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    private PlayerController playerController;

    [Header("Stats")]
    public float time;
    float moneyTotal;
    float weightTotal;

    [Header("Texts")]
    public Text moneyText;
    public Text weightText;
    public Text timeText;

    [System.Serializable]
    public class SpawnPoints
    {
        public string name;
        public string description;
        public Transform finishPoint;

        public SpawnPoints(string name, string description, Transform finishPoint)
        {
            this.name = name;
            this.description = description;
            this.finishPoint = finishPoint;
        }
    }

    public List<SpawnPoints> spawnPoints = new List<SpawnPoints>();

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        UpdateText();
    }

    public void GoToPoint(int i)
    {
        transform.position = spawnPoints[i].finishPoint.position;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            //TODO add end
        }
        timeText.text = TimeFormat(time, "F0");
    }

    public void UpdateText()
    {
        float tempMoney = 0;
        float tempWeight = 0;

        for (int i = 0; i < playerController.items.Count; i++)
        {
            if (playerController.items[i].count != 0)
            {
                tempMoney += playerController.items[i].value * playerController.items[i].count;
                tempWeight += playerController.items[i].weight * playerController.items[i].count;
            }
        }

        moneyTotal = tempMoney;
        weightTotal = tempWeight;

        moneyText.text = "$" + moneyTotal.ToString("F2");
        weightText.text = weightTotal.ToString("F2") + "kg";
    }

    public static string TimeFormat(double num, string dec)
    {
        double numStr;

        string suffix;
        if (num < 60)
        {
            numStr = num;
            suffix = " s";
        }
        else if (num < 3600)
        {
            numStr = num / 60;
            suffix = " min";
        }
        else if (num < 86400)
        {
            numStr = num / 3600;
            suffix = " h";
        }
        else if (num < 604800)
        {
            numStr = num / 86400;
            suffix = " days";
        }
        else
        {
            numStr = num;
            suffix = " s";
        }
        return numStr.ToString(dec) + suffix;
    }
}
