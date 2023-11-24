using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float DelayToStart;
    [SerializeField] public int waveDelay;
    [SerializeField] List<GameObject> Waves;
    [SerializeField] bool AutomaticStart;
    public int WaveDelay { get => waveDelay; }
    public bool WaveInProgress {get; set;}
    public int CurrentWave { get; private set; }
    public int WavesCount { get; private set; }
    public bool LevelSupered { get; private set; } = false;
    public bool LevelFinished { get=>Waves.All(wave => wave.IsDestroyed()); }
    private float timeToStart = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        WavesCount = Waves.Count;
        WaveInProgress = false;
        CurrentWave = 0;
        timeToStart = Time.time + DelayToStart;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeToStart<= Time.time)
        {
            StartNexWave();
            if (WaveInProgress && Waves.Count > 0 && Waves[0] != null && Waves[0].GetComponent<EnemyPool>().IsEmpty)
            {
                WaveInProgress = false;
                Destroy(Waves[0]);
                Waves.RemoveAt(0);
            }
        }
    }
    public void StartNexWave()
    {
        if (!WaveInProgress && !LevelFinished)
        {
            WaveInProgress = true;
            Waves[0].SetActive(true);
            CurrentWave++;
        }
        if (!WaveInProgress && LevelFinished)
        {
            LevelSupered = true;
        }
    }
}
