using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum spawnState
    {
        SPAWN, WAIT, COUNT
    }
    [System.Serializable]
    public class waveProps
    {
        public string name;
        public Transform prefab;
        public int count;
    }

    public waveProps[] enemyWaves;
    public float timeBetweenWave = 2f;
    public float waveCountdown;
    public int currentWave = 0;
    public Transform spawnPoint;
    public spawnState state = spawnState.COUNT;

    private void Start()
    {
        waveCountdown = timeBetweenWave;
    }

    private void Update()
    {
        if (state == spawnState.WAIT)
        {
            if (!enemyIsAlive())
            {
                waveCountdown = timeBetweenWave;
                state = spawnState.COUNT;
                if (currentWave >= enemyWaves.Length - 1)
                {
                    currentWave = 0;
                    print("Ok DOne");
                }
                else
                {
                    currentWave++;
                }
            }
            return;
        }
        if (waveCountdown < 0)
        {
            //spawn
            if (state != spawnState.SPAWN)
            {
                StartCoroutine(SpawnEnemy(enemyWaves[currentWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    bool enemyIsAlive()
    {
        return GameObject.FindGameObjectWithTag("Enemy");
    }
    void waveCompleted()
    {
        state = spawnState.COUNT;

    }
    IEnumerator SpawnEnemy(waveProps _wave)
    {
        state = spawnState.SPAWN;
        for (int i = 0; i < _wave.count; i++)
        {
            Instantiate(_wave.prefab, spawnPoint.position, _wave.prefab.rotation);
            yield return new WaitForSeconds(2f);

        }
        state = spawnState.WAIT;
        yield break;
    }
}
