using UnityEngine;

public class CircleInput : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastObject(Input.mousePosition);
        }
    }

    private void RaycastObject(Vector2 position)
    {
        Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(position);
        var hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit)
        {
            IRaycastable raycastedObj = hit.transform.GetComponent<IRaycastable>();
            raycastedObj?.OnRaycasted();
        }
    }
}
