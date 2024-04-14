using Employees.Enums;
using Navigation.Employee;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

        private Employee _employee;

        private bool _hasHorse;
        private float _speedBeforeCabbageCart;

        private float _runningSpeedPerkModifier = 1f;
        private float _horseSpeedPerkModifier = 1f;
        public int _workContributionPerkModifier = 1;

        public bool IsTrader, IsGrumpy, IsBrute, IsDubious, IsEquinophobe, IsAviophobe;

        private void Awake()
        {
            agent.FinalDestinationReached += Agent_OnFinalDestinationReached;
        }

        public void Initialise(Employee employee)
        {
            _employee = employee;
            //Agile/Corpulent
            var runningSpeedModifierPerk = _employee.listOfPerks.FirstOrDefault(x => x.speedModifier != 1);
            if (runningSpeedModifierPerk != null)
            {
                _runningSpeedPerkModifier *= runningSpeedModifierPerk.speedModifier;
            }

            //Rider
            var horseSpeedModifierPerk = _employee.listOfPerks.FirstOrDefault(x => x.horseSpeedModifier != 1);
            if (horseSpeedModifierPerk != null)
            {
                _horseSpeedPerkModifier *= horseSpeedModifierPerk.horseSpeedModifier;
            }

            //Talented
            var workContributionModifierPerk = _employee.listOfPerks.FirstOrDefault(x => x.workUnitModifier > 1);
            if (workContributionModifierPerk != null)
            {
                _workContributionPerkModifier *= workContributionModifierPerk.workUnitModifier;
            }

            //Trader
            var traderModifierPerk = _employee.listOfPerks.FirstOrDefault(x => x.isTrader);
            if (traderModifierPerk != null)
            {
                IsTrader = traderModifierPerk.isTrader;
            }

            //Grumpy
            var grumpyModifierPerk = _employee.listOfPerks.FirstOrDefault(x => x.isGrumpy);
            if (grumpyModifierPerk != null)
            {
                IsGrumpy = grumpyModifierPerk.isGrumpy;
            }

            //Brute
            var bruteModifierPerk = _employee.listOfPerks.FirstOrDefault(x => x.isBrute);
            if (bruteModifierPerk != null)
            {
                IsBrute = bruteModifierPerk.isBrute;
            }

            //Dubious
            var dubiousModifierPerk = _employee.listOfPerks.FirstOrDefault(x => x.isDubious);
            if (dubiousModifierPerk != null)
            {
                IsDubious = dubiousModifierPerk.isDubious;
            }

            //Loshadka-phobe
            var equinophobeModifierPerk = _employee.listOfPerks.FirstOrDefault(x => x.isEquinophobe);
            if (equinophobeModifierPerk != null)
            {
                IsEquinophobe = equinophobeModifierPerk.isEquinophobe;
            }

            //Aviophobe
            var aviophobeModifierPerk = _employee.listOfPerks.FirstOrDefault(x => x.isAviophobe);
            if (aviophobeModifierPerk != null)
            {
                IsAviophobe = aviophobeModifierPerk.isAviophobe;
            }

            SetSpeed(employee.speed * _runningSpeedPerkModifier);

            //Set Perks
            //Adjust running speed

            //Adjust horse speed

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
            if (_hasHorse || IsEquinophobe)
                return;

            _hasHorse = true;

            //Ignore/Reverse running speed modifiers
            var speed = agent.Speed / _runningSpeedPerkModifier * HORSE_SPEED_MODIFIER * _horseSpeedPerkModifier;
            SetSpeed(speed);
        }

        public void TryKidnap()
        {
            //Check for Guard
            if (HasGuards || IsDubious)
            {
                return;
            }

            //Kidnaps employee
            Destroy(gameObject);

        }

        public void SetFestivalSpeed(bool value)
        {
            if (HasGuards || IsGrumpy)
                return;

            if (value)
                agent.Speed *= FESTIVAL_SPEED_MODIFIER;
            else
                agent.Speed /= FESTIVAL_SPEED_MODIFIER;
        }

        public void SetCabbageCartSpeed(bool value)
        {
            if (value)
            {
                //Store original speed (with/without horse)
                _speedBeforeCabbageCart = agent.Speed;

                //Set speed to 0
                SetSpeed(0);
            }

            else
            {
                SetSpeed(_speedBeforeCabbageCart);
            }
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
            if (_travelOption == TravelOptions.Catapult)
                StartCoroutine(CatapultingRoutine(point));
            else
                transform.position = point;
        }

        private IEnumerator CatapultingRoutine(Vector3 endPoint)
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

