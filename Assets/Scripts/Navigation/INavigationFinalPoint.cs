using UnityEngine;

namespace Navigation
{
    public interface INavigationFinalPoint
    {
        /// <summary>
        /// Position that is used as final point of path.
        /// </summary>
        Vector3 EntryPosition { get; }
    }
}

