using TMPro;
using UnityEngine;

public class WaveControl : MonoBehaviour
{
    [SerializeField] private CircleSpawner _spawner;
    [SerializeField] private TextMeshProUGUI _waveInfo;
    [SerializeField] private int _spawnPerWave = 10;
    [SerializeField] private float _delayPerWave = 5f;

    private int _wave;
    private bool _waveIsRunning;
    private float _waveDelayCounter;

    private int _spawnedEnemyCount;
    private int _despawnedEnemyCount;

    private void Start()
    {
        _spawner.OnCircleSpawned += OnCircleSpawned;
        _spawner.OnCircleDespawned += OnCircleDespawned;
    }

    private void Update()
    {
        if (_waveIsRunning)
        {
            return;
        }

        _waveDelayCounter += Time.deltaTime;
        if (_waveDelayCounter > _delayPerWave)
        {
            StartWave();
        }
    }

    private void StartWave()
    {
        _waveIsRunning = true;
        _wave++;
        _waveInfo.SetText("Wave " + _wave);

        _spawnedEnemyCount = _despawnedEnemyCount = 0;
        _spawner.RunSpawner();
        Debug.Log($"Wave {_wave} has started!");
    }

    private void FinishWave()
    {
        _waveIsRunning = false;
        Debug.Log($"Wave {_wave} has been finished!");
    }

    private void OnCircleSpawned()
    {
        if (++_spawnedEnemyCount >= _spawnPerWave)
        {
            _spawner.StopSpawner();
        }
    }

    private void OnCircleDespawned()
    {
        if (++_despawnedEnemyCount >= _spawnPerWave)
        {
            FinishWave();
        }
    }
}
