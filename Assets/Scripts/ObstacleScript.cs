using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleScript : MonoBehaviour
{

    public float yOffset = 0f;
    public float xVelocity = 5f;

    private Rigidbody obsRB;
    public ScoreTriggerScript scoreTrigger;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        obsRB = GetComponent<Rigidbody>();

        SetObsVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetObsVelocity()
    {
        obsRB.velocity = new Vector3(xVelocity, obsRB.velocity.y, obsRB.velocity.z);
    }

    public void CheckScoreTrigger()
    {
        if(scoreTrigger.inScoreTrigger)
        {
            gameManager.AddScore(10);
        }
    }
}
