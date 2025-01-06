using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform groundSpawnPoint;
    public Transform airSpawnMin;
    public Transform airSpawnMax;

    private float trashCanYOffset = 0.5f; // Offset for trash cans
    private float dogYOffset = 0.25f;

    public float spawnInterval = 2f; // Time between spawns
    public string[] beachObstacles; // Tags for different obstacles in the pool
    public string[] streetObstacles;
    public string[] labObstacles; 
    public string[] spaceObstacles; 

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        string tagToSpawn = ChooseTag(3); // Choose what to spawn based on the scene TODO: Change
        Vector3 spawnPosition;

        if (tagToSpawn == "Rock" || tagToSpawn == "Spill"){
            spawnPosition = groundSpawnPoint.position; // Always on the ground
        }
        else if (tagToSpawn == "TrashCan"){
            spawnPosition = groundSpawnPoint.position + new Vector3(0, trashCanYOffset, 0);
        }
        else if (tagToSpawn == "Dog"){
            spawnPosition = groundSpawnPoint.position + new Vector3(0, dogYOffset, 0);
        }
        else if (tagToSpawn == "Seagull"){
            float randomHeight = Random.Range(airSpawnMin.position.y, airSpawnMin.position.y+airSpawnMax.position.y/2);  // Min - 1/2 Max
            spawnPosition = new Vector3(airSpawnMin.position.x, randomHeight, 0); // Within air range
        }
        else if (tagToSpawn == "BeachBall"){
            spawnPosition = new Vector3(airSpawnMin.position.x, airSpawnMin.position.y+airSpawnMax.position.y/2, 0);
        }
        else if (tagToSpawn == "Sign"){
            spawnPosition = new Vector3(airSpawnMin.position.x, airSpawnMax.position.y, 0);     // Always at the top
        }
        else if (tagToSpawn == "Drone"){
            float randomHeight = Random.Range(airSpawnMin.position.y+airSpawnMax.position.y/2, airSpawnMax.position.y);  // 1/2 Max - Max
            spawnPosition = new Vector3(airSpawnMin.position.x, airSpawnMax.position.y, 0);     // Always at the top
        }
        else
        {
            Debug.LogWarning("Unknown obstacle tag: " + tagToSpawn);
            return;
        }


        // Use the object pool to spawn the obstacle
        ObjectPooler.Instance.SpawnFromPool(tagToSpawn, spawnPosition, Quaternion.identity);
    }

    // Chooses a tag based on the level
    // 1 - Beach
    // 2 - Streets
    // 3 - Lab
    // 4 - Space
    private string ChooseTag(int currentLevel){
        switch (currentLevel){
            case 1:
                return beachObstacles[Random.Range(0, beachObstacles.Length)];
            case 2:
                return streetObstacles[Random.Range(0, streetObstacles.Length)];
            case 3:
                return labObstacles[Random.Range(0, labObstacles.Length)];
            case 4:
                return spaceObstacles[Random.Range(0, spaceObstacles.Length)];
            default:
                return "Unknown Level";
        }
    }
}
