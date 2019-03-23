using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
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

    public void GoToPoint(int i)
    {
        transform.position = spawnPoints[i].finishPoint.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
