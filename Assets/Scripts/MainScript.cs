using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    private PlayerController playerController;

    [Header("Time")]
    float time = 600;
    float time1Open = 50;
    float time2Open = 120;
    float time3Open = 420;

    [Header("Stats")]
    public float moneyTotal;
    public float weightTotal;

    [Header("Texts")]
    public Text moneyText;
    public Text weightText;
    public Text timeText;

    public Transform[] spawnpoints;
    public GameObject[] stairs;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        UpdateText();
    }

    public void GoToPoint(int i)
    {
        transform.position = spawnpoints[i].position;
    }

    private void RaiseStairs(int s)
    {
        if (stairs[s].gameObject.activeSelf)
            stairs[s].gameObject.SetActive(true);
    }

    private void LowerStairs(int l)
    {
        if (!stairs[l].gameObject.activeSelf)
            stairs[l].gameObject.SetActive(false);
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time < time1Open && time > time2Open)
        {
            RaiseStairs(0);
        }
        else if (time < time2Open && time > time3Open)
        {
            RaiseStairs(1);
            LowerStairs(2);
        }
        else if (time < time3Open)
        {
            RaiseStairs(2);
            LowerStairs(3);
        }
        else if (time < 0)
        {
            //TODO add end
        }
        timeText.text = TimeFormat(time, "F0");
    }

    public void UpdateText()
    {
        /*
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

    */
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
