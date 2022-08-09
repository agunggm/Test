using UnityEngine;

public class CircleObject : MonoBehaviour, IRaycastable
{
    private const float _despawnPosY = -5.5f;

    [SerializeField] private float _speed = 2f;

    public event System.Action OnDespawned;

    private void Update()
    {
        transform.Translate(0f, -_speed * Time.deltaTime, 0f);
        if (transform.position.y < _despawnPosY)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        OnDespawned?.Invoke();
    }

    public void OnRaycasted()
    {
        gameObject.SetActive(false);
    }
}
