using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    [SerializeField] private CircleObject _circlePrefab;
    [SerializeField] private float _spawnRadius = 5f;
    [SerializeField] private float _spawnDelay = 2f;

    private List<CircleObject> _circlePools = new List<CircleObject>();
    private float _spawnDelayCounter;
    private bool _isSpawning;

    public event System.Action OnCircleSpawned;
    public event System.Action OnCircleDespawned;

    private void Update()
    {
        if (!_isSpawning)
        {
            return;
        }

        _spawnDelayCounter += Time.deltaTime;
        if (_spawnDelayCounter > _spawnDelay)
        {
            SpawnCircle(OnCircleDespawned);
            _spawnDelayCounter = 0f;
        }
    }

    public void RunSpawner()
    {
        _isSpawning = true;
    }

    public void StopSpawner()
    {
        _isSpawning = false;
    }

    public void SpawnCircle(System.Action onDespawned = null)
    {
        CircleObject circle = _circlePools.Find(c => !c.gameObject.activeSelf);
        if (circle == null)
        {
            circle = Instantiate(_circlePrefab, transform);
            circle.OnDespawned += onDespawned;
            _circlePools.Add(circle);
        }

        circle.transform.localPosition = new Vector2(
            Random.Range(-_spawnRadius, _spawnRadius), 0f
        );
        circle.gameObject.SetActive(true);

        OnCircleSpawned?.Invoke();
    }
}
