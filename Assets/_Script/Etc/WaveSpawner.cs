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

        //public WaveData(string objName, int count, float spawnTime, float speed, float hp, float defence)
        //{
        //    enemy = Resources.Load<GameObject>(objName);
        //    this.count = count;
        //    this.spawnTime = spawnTime;
        //    this.speed = speed;
        //    this.hp = hp;
        //    this.defence = defence;
        //}
    }

    public static int EnemiesAlive = 0;

    [SerializeField] Transform spawnPoint;

    [SerializeField] WaveData[] waves;
    [SerializeField] TextAsset wavedata;

    float timeBetweenWaves = 5f;
    float startTime = 2f;

    int waveIndex = 0;

    //private void Awake()
    //{
    //    List<Dictionary<string, object>> data = CSVReader.Read(wavedata);
    //    waves = new WaveData[data.Count];
    //}
    private void Start()
    {
        int index = 0;
        List<Dictionary<string, object>> data = CSVReader.Read(wavedata);
        foreach (Dictionary<string, object> d in data)
        {
            waves[index].enemy = Resources.Load<GameObject>(d["enemyName"].ToString());
            waves[index].count = int.Parse(d["count"].ToString());
            waves[index].spawnTime = float.Parse(d["spawnTime"].ToString());
            waves[index].speed = float.Parse(d["speed"].ToString());
            waves[index].hp = float.Parse(d["hp"].ToString());
            waves[index].defence = float.Parse(d["defense"].ToString());
            index++;
        }
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

        if(waveIndex == waves.Length)
        {
            GamaManager.Instance.GameClear();
            this.enabled = false;
        }

        startTime -= Time.deltaTime;

    }

    IEnumerator SpawnWave()
    {
        WaveData wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for(int i= 0; i < wave.count; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.spawnTime);
        }

        waveIndex++;
    }

    void SpawnEnemy(WaveData wave)
    {
        Statable stat = wave.enemy.GetComponent<Statable>();
        stat.maxHp = wave.hp;
        stat.defense = wave.defence;
        EnemyController enemy = wave.enemy.GetComponent<EnemyController>();
        enemy.speed = wave.speed;
        Instantiate(wave.enemy, spawnPoint.position, Quaternion.identity);
    }
}
