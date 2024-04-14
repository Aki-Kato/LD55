using Navigation.Employee;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Employees.Controllers
{
    public sealed class EmployeeController : MonoBehaviour
    {
        public event Action FinalDestinationReached;

        [SerializeField] private EmployeeAgent agent;
        [SerializeField] private AnimationCurve catapultingCurve;
        [SerializeField] private float catapultingTime;

        private void Awake()
        {
            agent.FinalDestinationReached += Agent_OnFinalDestinationReached;
        }

        public void Initialise(float speed)
        {
            agent.Speed = speed;
        }

        public void SendBy(LinkedList<Vector3> pathPoints) =>
            agent.SendBy(pathPoints);

        public void CatapultTo(Vector3 point)
        {
            agent.Enabled = false;
            StartCoroutine(CatapultingRoutine(point));
        }

        public IEnumerator CatapultingRoutine(Vector3 endPoint)
        {
            Vector3 startPosition = transform.position;

            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
            float time = 0f;
            while (time < 1f)
            {
                Vector3 newPosition = Vector3.Lerp(startPosition, endPoint, time);
                newPosition.y = catapultingCurve.Evaluate(time);
                transform.position = newPosition;
                
                yield return waitForEndOfFrame;
                time += Time.deltaTime / catapultingTime;
            }
        }

        private void Agent_OnFinalDestinationReached(EmployeeAgent employeeAgent)
        {
            throw new System.NotImplementedException();
        }

    }
}

