using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Employee
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class EmployeeAgent : MonoBehaviour
    {
        private const float CHECK_FOR_DESTINATION_REACHED_TICK = 0.1f;

        public event Action FinalDestinationReached;

        [SerializeField] private float reachPointDistance;

        private NavMeshAgent _agent;

        private LinkedList<Vector3> _pathPoints;
        private LinkedListNode<Vector3> _currentDestination;

        public float Speed { get => _agent.speed; set => _agent.speed = value; }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void SendBy(LinkedList<Vector3> pathPoints)
        {
            _pathPoints = pathPoints;
            StartMoving();
        }

        private void StartMoving()
        {
            _currentDestination = _pathPoints.First;
            SetDesinationAt(_currentDestination);
            _agent.isStopped = false;

            StartCoroutine(CheckDestinationReachedRoutine());
        }

        private void SetDesinationAt(LinkedListNode<Vector3> point)
        {
            _agent.SetDestination(point.Value);
        }

        private IEnumerator CheckDestinationReachedRoutine()
        {
            WaitForSeconds waitNextTick = new WaitForSeconds(CHECK_FOR_DESTINATION_REACHED_TICK);
            while (_agent.isStopped == false)
            {
                if (IsReachedDestination())
                    MoveToTheNextPoint();
                yield return waitNextTick;
            }
        }

        private bool IsReachedDestination()
        {
            float distance = Vector3.Distance(transform.position, _currentDestination.Value);
            return distance < reachPointDistance;
        }

        private void MoveToTheNextPoint()
        {
            if (_currentDestination.Next != null)
            {
                _currentDestination = _currentDestination.Next;
                SetDesinationAt(_currentDestination);
                return;
            }

            _agent.isStopped = true;
            FinalDestinationReached?.Invoke();
        }
    }
}
