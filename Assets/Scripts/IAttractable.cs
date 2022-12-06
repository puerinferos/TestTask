using UnityEngine;

namespace DefaultNamespace
{
    public interface IAttractable
    {
        public void Attract(Transform objectAttractTo,float force);
        public void StopAttract();
    }
}