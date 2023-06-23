using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigurations;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] int numOfEnemies;
    bool nextWave = false;
    [SerializeField] int numOfWaves;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping == true);
    }

    private IEnumerator SpawnAllWaves()
    {
        numOfWaves = waveConfigurations.Count;
        for (int waveIndex = startingWave; waveIndex < waveConfigurations.Count; waveIndex++)
        {
            var currentWave = waveConfigurations[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            numOfWaves--;
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        numOfEnemies = wave.GetNumberOfEnemies();
        for(int enemyCount = 0; enemyCount < wave.GetNumberOfEnemies();enemyCount ++)
        {
            var newEnemy = Instantiate(wave.GetEnemyPrefab(), wave.GetWaypoints()[0].transform.position, transform.rotation);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);
            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
        while(nextWave == false)
        {
            yield return null;
        }
        
    }

    private bool SpawnNextWave()
    { 
        if (numOfEnemies <= 0)
        {
            nextWave = true;
            return nextWave;
        }
        else
        {
            nextWave = false;
            return nextWave;
        }   
    }    
    
    public void SubtractEnemy()
    {
        numOfEnemies--;
    }

    public void GoToWinScreen()
    {
        if (numOfWaves <= 0 && numOfEnemies <= 0)
        {
            FindObjectOfType<Level>().LoadWinScreen();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Calling the new functions I made above
        SpawnNextWave();
        GoToWinScreen();
    }
}
