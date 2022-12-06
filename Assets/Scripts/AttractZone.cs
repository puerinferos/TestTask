using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class AttractZone : MonoBehaviour
    {
        [SerializeField] private float force;
        [SerializeField] private float distanceToInteract;

        private void OnTriggerStay(Collider other)
        {
            var attractable = other.GetComponent<IAttractable>();
            attractable?.Attract(transform, force);

            if (Vector3.Distance(transform.position,other.transform.position) <= distanceToInteract)
            {
                var interactable = other.gameObject.GetComponent<IInterectable>();
                interactable?.Interact();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var attractable = other.GetComponent<IAttractable>();
            attractable?.StopAttract();
        }
    }
}