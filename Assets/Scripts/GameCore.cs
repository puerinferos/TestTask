using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private int initialPrefabCount;
        [SerializeField] private Grid grid;
        [SerializeField] private Player player;
        [SerializeField] private List<MonoPoolable> prefabs = new List<MonoPoolable>();

        private SimpleObjectPool _pool;

        private void Awake()
        {
            _pool = new SimpleObjectPool(initialPrefabCount, prefabs);
            grid.Initialize(_pool);
        }

        private void Start()
        {
            player.onGridPositionChanged += grid.ActivateCellAndAround;
        }
    }
}