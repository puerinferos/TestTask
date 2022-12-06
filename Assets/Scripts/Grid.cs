using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private const float planeSize = 10f;

    private static readonly Vector2Int[] VectorsAroundCell =
    {
        new Vector2Int(-1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 1)
    };

    [SerializeField] private Transform gridPlane;
    [SerializeField] private int _sideDivider;
    
    private SimpleObjectPool _pool;
    private Cell[,] cells;
    private Vector2 _cellSize;
    private Vector2 _widthHeight;

    private void Awake()
    {
        _widthHeight = new Vector2(gridPlane.localScale.x, gridPlane.localScale.z) * planeSize;
        transform.position = new Vector3(gridPlane.position.x - _widthHeight.x / 2, 0, gridPlane.position.z - _widthHeight.y / 2);

        _cellSize = _widthHeight / _sideDivider;
        cells = new Cell[_sideDivider, _sideDivider];
    }

    public void Initialize(SimpleObjectPool pool) =>
        _pool = pool;

    public Vector2Int PositionToGridPosition(Vector2 position)
    {
        position -= new Vector2(transform.position.x, transform.position.z);
        int x = Mathf.RoundToInt(position.x / _cellSize.x);
        int y = Mathf.RoundToInt(position.y / _cellSize.y);

        return new Vector2Int(x, y);
    }

    public void ActivateCellAndAround(Vector2Int cell)
    {
        ActivateCell(cell);
        for (int i = 0; i < VectorsAroundCell.Length; i++)
        {
            var nearCellIndex = new Vector2Int(cell.x + VectorsAroundCell[i].x, cell.y + VectorsAroundCell[i].y);
            ActivateCell(nearCellIndex);
        }
    }

    private void ActivateCell(Vector2Int cellIndex)
    {
        if (!CellExist(cellIndex))
            return;

        cells[cellIndex.x, cellIndex.y] =
            new Cell(CellToPosition(cellIndex), PoissonDiscSampling.GeneratePoints(_cellSize), _pool);
        cells[cellIndex.x, cellIndex.y].TurnOnObjectsInSector();
    }

    private Vector3 CellToPosition(Vector2Int cellIndex) =>
        new Vector3(_cellSize.x * cellIndex.x, 1, cellIndex.y * _cellSize.y) + transform.position;

    private bool CellExist(Vector2Int cellIndex)
    {
        if (cells.GetLength(0) - 1 < cellIndex.x || cells.GetLength(1) - 1 < cellIndex.y)
            return false;
        if (cellIndex.x < 0 || cellIndex.y < 0)
            return false;
        if (cells[cellIndex.x, cellIndex.y] != null)
            return false;

        return true;
    }

    private void OnDrawGizmos()
    {
        if (cells == null || cells.Length == 0)
            return;
        
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                Gizmos.DrawWireCube(CellToPosition(new Vector2Int(i, j)) + new Vector3(_cellSize.x, 0, _cellSize.y) / 2,
                    new Vector3(_cellSize.x, 1, _cellSize.y));
            }
        }
    }
}