using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private List<Vector2> _points;
    private SimpleObjectPool _pool;
    private Vector3 _position;

    public Cell(Vector3 position, List<Vector2> points, SimpleObjectPool pool)
    {
        _points = points;
        _pool = pool;
        _position = position;
    }

    public void TurnOnObjectsInSector()
    {
        for (int i = 0; i < _points.Count; i++)
            _pool.PoolInstantiate(new Vector3(_points[i].x + _position.x, 1, _points[i].y + _position.z),
                Quaternion.identity);
    }
}