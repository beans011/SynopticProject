using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public static NPCSpawner instance;

    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private GameObject npcWalkerPrefab;
    [SerializeField] private List<Material> materials;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject shopEntranceObj;
    [SerializeField] private List<GameObject> tills;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        
    }

    public void SpawnShopperNPC()
    {
        for (int i = 0; i < Random.Range(1, 3); i++) 
        {
            Instantiate(npcPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity); ;          
        }
    }

    public void SpawnWalkingNPC()
    {
        for (int i = 0; i < Random.Range(3,9); i++)
        {
            Transform transform = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Vector3 spawnLocation = new Vector3(transform.position.x, 1, transform.position.z);

            Instantiate(npcWalkerPrefab, spawnLocation, gameObject.transform.rotation);
        }
    }

    public Transform GetASpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
    public Material GetMaterial()
    {
        return materials[Random.Range(0, materials.Count)];
    }
    public Vector3 GetShopEntrance()
    {
        Vector3 shopEntrance = new Vector3(shopEntranceObj.transform.position.x, 0, shopEntranceObj.transform.position.z);
        return shopEntrance;
    }

    public Vector3 GetTillLocation()
    {
        GameObject tempObj = tills[Random.Range(0, tills.Count)];
        Vector3 tillTransform = new Vector3(tempObj.transform.position.x, 0, tempObj.transform.position.z);
        return tillTransform;
    }
}
