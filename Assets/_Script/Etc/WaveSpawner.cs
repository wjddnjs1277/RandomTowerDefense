using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WaveData
    {
        public GameObject enemy;
        public int count;
        public float spawnTime;
        public float speed;
        public float hp;
        public float defence;

        public WaveData(string objName, int count, float spawnTime, float speed, float hp, float defence)
        {
            enemy = Resources.Load<GameObject>(objName);
            this.count = count;
            this.spawnTime = spawnTime;
            this.speed = speed;
            this.hp = hp;
            this.defence = defence;
        }
    }

    public static int EnemiesAlive = 0;

    [SerializeField] Transform spawnPoint;

    [SerializeField] WaveData[] waves;
    [SerializeField] List<WaveData> waveList = new List<WaveData>();

    float timeBetweenWaves = 5f;
    float startTime = 2f;

    int waveIndex = 0;

    private void Start()
    {
        //WaveInit();
    }

    private void Update()
    {
        if (EnemiesAlive > 0)
            return;

        if(startTime <= 0f)
        {
            StartCoroutine(SpawnWave());
            startTime = timeBetweenWaves;
            Player.Instance.Money += 40;
            return;
        }

        startTime -= Time.deltaTime;

    }

    IEnumerator SpawnWave()
    {
        WaveData wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for(int i= 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(wave.spawnTime);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, Quaternion.identity);
    }

    public void WaveInit()
    {
        waveList = JsonManager.LoadJsonList<WaveData>(Application.streamingAssetsPath,"wavedata");
        
        foreach(var wave in waveList)
        {
            Debug.Log(wave.enemy.name);
            Debug.Log(wave.count);
            Debug.Log(wave.spawnTime);
            Debug.Log(wave.speed);
        }
    }
}
