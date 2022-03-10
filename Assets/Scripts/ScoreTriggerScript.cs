using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerScript : MonoBehaviour
{
    public bool inScoreTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("HERe");
        if(other.CompareTag("Player"))
        {
            inScoreTrigger = true;
        }
        else
        {
            inScoreTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inScoreTrigger = false;
    }
}
