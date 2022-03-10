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
    public GroundController gc;

    public bool scoreAdded = false;

    // Start is called before the first frame update
    void Start()
    {
        obsRB = GetComponent<Rigidbody>();

        SetObsVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        CheckScoreTrigger();
    }

    public void SetObsVelocity()
    {
        obsRB.velocity = new Vector3(xVelocity, obsRB.velocity.y, obsRB.velocity.z);
    }

    public void CheckScoreTrigger()
    {
        if(scoreTrigger.inScoreTrigger && !scoreAdded)
        {
            gameManager.AddScore(10);
            scoreAdded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DestroySensor"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        gc.RemoveObs(gameObject);
    }
}
