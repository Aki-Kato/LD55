using Navigation;
using UnityEngine;

namespace Buildings
{
    public sealed class Building : MonoBehaviour, INavigationFinalPoint
    {
        [SerializeField] private Transform buildingEntry;

        public Vector3 EntryPosition => buildingEntry.position;
    }
}

