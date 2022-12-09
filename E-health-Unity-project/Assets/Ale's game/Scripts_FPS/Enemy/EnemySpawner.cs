using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    private int currentWave;

    [SerializeField] private Wave[] waves;

    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float waveCountdown = 0;

    private SpawnState state = SpawnState.COUNTING;
    
    [SerializeField] private Transform[] spawners;
    [SerializeField] private List<CharacterStats> enemyList;

    public LevelLoader other;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        currentWave = 0;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemiesAreDead())
                return;
            else
                CompleteWave();
        }
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[currentWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemiesAmount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(wave.delay);
        }

        state = SpawnState.WAITING;
        
        yield break;
    }

    private void SpawnEnemy(GameObject[] enemy)
    {
        int checkSpawn = 0;
        while (checkSpawn == 0)
        { 
            int randomInt = Random.Range(0, spawners.Length);
            int randomEnemyType = Random.Range(0, enemy.Length);
            Transform randomSpawner = spawners[randomInt];
            Renderer objRenderer = enemy[randomEnemyType].GetComponent<Renderer>();
            Transform enemyTransform = enemy[randomEnemyType].GetComponent<Transform>();
            float objRadius = objRenderer.bounds.size.y;
            objRadius = objRadius * 0.75f;
            if (!Physics.CheckSphere(randomSpawner.transform.position, objRadius))
            {
                GameObject newEnemy = Instantiate(enemy[randomEnemyType], randomSpawner.position, enemyTransform.rotation);
                checkSpawn = 1;
                CharacterStats newEnemyStats = newEnemy.GetComponent<CharacterStats>();
                enemyList.Add(newEnemyStats);
            }
        }
    }

    private bool EnemiesAreDead()
    {
        int i = 0;
        foreach (CharacterStats enemy in enemyList)
        {
            if (enemy.IsDead())
                i++;
            else
                return false;
        }
        return true;
    }

    private void CompleteWave()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (currentWave + 1 > waves.Length - 1)
        {
            other.LoadNextLevel();
            currentWave = 0;
        }
        else
        {
            currentWave++;
        }
    }
}
