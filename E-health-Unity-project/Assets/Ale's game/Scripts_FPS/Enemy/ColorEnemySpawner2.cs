using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorEnemySpawner2 : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};
    
    public Material[] randomMaterialColor;

    private int currentWave;

    [SerializeField] private Wave[] waves;

    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float waveCountdown = 0;

    private SpawnState state = SpawnState.COUNTING;
    
    [SerializeField] private Transform[] spawners;
    [SerializeField] private List<CharacterStats> enemyList;
    
    public ThreeLL other;

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
        float[] objRadius = new float[enemy.Length];
        Transform[] enemyTransform = new Transform[enemy.Length];
        for (int i = 0; i < enemy.Length; i++)
        {
            Renderer objRenderer = enemy[i].GetComponent<Renderer>();
            enemyTransform[i] = enemy[i].GetComponent<Transform>();
            objRadius[i] = objRenderer.bounds.size.y;
            objRadius[i] = objRadius[i] * 0.75f;
        }
        while (checkSpawn == 0)
        { 
            int randomInt = Random.Range(0, spawners.Length);
            int randomEnemyType = Random.Range(0, enemy.Length);
            int randomColor = Random.Range(0, randomMaterialColor.Length);
            Transform randomSpawner = spawners[randomInt];
            if (!Physics.CheckSphere(randomSpawner.transform.position, objRadius[randomEnemyType]))
            {
                GameObject newEnemy = Instantiate(enemy[randomEnemyType], randomSpawner.position, enemyTransform[randomEnemyType].rotation);
                newEnemy.GetComponent <Renderer>().material = randomMaterialColor[randomColor];
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

