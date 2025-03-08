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

    public void SpawnShopperNPC(int count)
    {
        for (int i = 0; i < count; i++) 
        { 
            GameObject newNPC = Instantiate(npcPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)]);
            newNPC.GetComponent<Renderer>().material = materials[Random.Range(0,materials.Count)];
        }
    }

    public void SpawnWalkingNPC()
    {
        Debug.Log("1");
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
}
