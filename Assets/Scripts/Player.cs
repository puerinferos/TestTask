using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Grid _grid;

    public Action<Vector2Int> onGridPositionChanged;
    private float _horizontalInput;
    private float _verticalInput;

    private Vector2Int gridOldPosition;
    private Vector2Int gridPosition;

    private Rigidbody rb;

    private void Start()
    {
        gridPosition = _grid.PositionToGridPosition(new Vector2(transform.position.x, transform.position.z));
        rb = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        _horizontalInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _verticalInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        rb.MovePosition(transform.position + (Vector3.right * _horizontalInput) + (Vector3.forward * _verticalInput));

        gridPosition = _grid.PositionToGridPosition(new Vector2(transform.position.x, transform.position.z));
        if (gridOldPosition != gridPosition)
        {
            onGridPositionChanged?.Invoke(gridPosition);
            gridOldPosition = gridPosition;
        }

    }

    private void FixedUpdate()
    {
        Move();
    }
}