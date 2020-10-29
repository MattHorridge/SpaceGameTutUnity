using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _powerUpPrefab;

    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning() 
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }


    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (!_stopSpawning)
        {
            Vector3 posSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (!_stopSpawning)
        {


            Vector3 posSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPower = Random.Range(0, 3);
            GameObject newPowerup = Instantiate(powerups[randomPower], posSpawn, Quaternion.identity);
            newPowerup.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3,15));
        }
    }



    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
