using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float scaleSpeed = 5f;

    public Vector3 downScale = new Vector3(1f, 0.4f, 1f);
    public Vector3 upScale = new Vector3(1f, 2f, 0.4f);

    public bool isScalingDown = false;
    public bool isScalingUp = false;

    public bool isDead = false;


    private InputManager playerInputManager;

    public delegate void OnPlayerDeathDelegate();
    public OnPlayerDeathDelegate OnPlayerDeath;

    void Start()
    {
        playerInputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ScalePlayer();
    }

    public void ScalePlayerDown()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, downScale, scaleSpeed * Time.deltaTime);
    }

    public void ScalePlayerUp()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, upScale, scaleSpeed * Time.deltaTime);
    }

    public void ScalePlayer()
    {
        if(playerInputManager.inputVector.y < 0)
        {
            ScalePlayerDown();
            //isScalingDown = true;
        }
        else if(playerInputManager.inputVector.y > 0)
        {
            ScalePlayerUp();
            //isScalingUp = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("here");
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
            //Debug.Log("obstacle hit");
            OnPlayerDeath?.Invoke();
        }
    }
}
