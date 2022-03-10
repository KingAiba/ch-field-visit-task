using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs = new List<GameObject>();

    public List<GameObject> activeObjects = new List<GameObject>();

    public float moveSpeed = 2f;
    public Vector3 moveDirection = new Vector3(1f, 0f, 0f);

    public bool enableMove = true;

    public Collider spawnArea;

    public bool canSpawn = true;
    public int numberToSpawn = 2;

    public GameManager gameManager;

    public float spawnCooldown = 0f;

    
    void Start()
    {
        //SpawnObstacles();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        if(canSpawn)
        {
            for(int i =0; i < numberToSpawn; i++)
            {
                GameObject obs = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
                Vector3 spawnPos = GenerateRandomSpawnPoint();
                Debug.Log(spawnPos);
                ObstacleScript obsScript = obs.GetComponent<ObstacleScript>();
                obsScript.gameManager = gameManager;
                obsScript.gc = this;
                //spawnPos.y = obsScript.yOffset;

                GameObject go = Instantiate(obs);

                activeObjects.Add(go);
                //go.transform.SetParent(gameObject.transform, false);
                //go.transform.position = spawnPos;
            }

            canSpawn = false;
            spawnCooldown = Random.Range(1f, 4f);
            StartCoroutine(spawnProcedure());
        }
    }

    public Vector3 GenerateRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-6f, -1f), 0, 0);
    }

    public void moveGround()
    {
        if(enableMove)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        
    }

    public void RemoveObs(GameObject obs)
    {
        activeObjects.Remove(obs);
    }

    public IEnumerator spawnProcedure()
    {
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
