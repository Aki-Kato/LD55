using Employees.Enums;
using Navigation.Employee;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Employees.Controllers
{
    public sealed class EmployeeController : MonoBehaviour
    {
        private const float HORSE_SPEED_MODIFIER = 2f;
        private const float FESTIVAL_SPEED_MODIFIER = 0.5f;

        public event Action FinalDestinationReached;

        [SerializeField] private EmployeeAgent agent;
        [SerializeField] private AnimationCurve catapultingCurve;
        [SerializeField] private float catapultingTime;

        private TravelOptions _travelOption = TravelOptions.Run;

        private bool HasGuards => 
            _travelOption == TravelOptions.Guard;

        private void Awake()
        {
            agent.FinalDestinationReached += Agent_OnFinalDestinationReached;
        }

        public void Initialise(float speed)
        {
            SetSpeed(speed);
        }

        private void SetSpeed(float speed)
        {
            agent.Speed = speed;
        }
        
        public void SetTravelOption(TravelOptions travelOptions)
        {
            _travelOption = travelOptions;
        }

        public void SetHorse()
        {
            var speed = agent.Speed * HORSE_SPEED_MODIFIER;
            SetSpeed(speed);
        }

        public void SetFestivalSpeed(bool value)
        {
            if (HasGuards)
                return;

            if (value)
                agent.Speed *= FESTIVAL_SPEED_MODIFIER;
            else
                agent.Speed /= FESTIVAL_SPEED_MODIFIER;
        }

        public void SendBy(LinkedList<Vector3> pathPoints)
        {
            if (_travelOption == TravelOptions.Horse)
                SetHorse();

            agent.SendBy(pathPoints);
        }

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

            OnFinalDestinationReached();
        }

        private void Agent_OnFinalDestinationReached(EmployeeAgent employeeAgent)
        {
            OnFinalDestinationReached();
        }

        private void OnFinalDestinationReached()
        {
            FinalDestinationReached?.Invoke();
        }
    }
}

