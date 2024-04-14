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
        private float running_speed_perk_modifier = 1f;
        private float horse_speed_perk_modifier = 1f;
        public int work_contribution_perk_modifier = 1;
        public bool isTrader, isGrumpy, isBrute, isDubious = false;
        private const float HORSE_SPEED_MODIFIER = 2f;
        private const float FESTIVAL_SPEED_MODIFIER = 0.5f;

        public event Action FinalDestinationReached;

        [SerializeField] private EmployeeAgent agent;
        [SerializeField] private AnimationCurve catapultingCurve;
        [SerializeField] private float catapultingTime;

        private TravelOptions _travelOption = TravelOptions.Run;

        private bool HasGuards =>
            _travelOption == TravelOptions.Guard;

        private bool HasHorse;

        private float speedBeforeCabbageCart;
        private Employee thisEmployee;

        private void Awake()
        {
            agent.FinalDestinationReached += Agent_OnFinalDestinationReached;
        }

        public void Initialise(Employee employee)
        {
            //Agile/Corpulent
            var runningSpeedModifierPerk = employee.listOfPerks.FirstOrDefault(x => x.speedModifier != 1);
            if (runningSpeedModifierPerk != null)
            {
                running_speed_perk_modifier *= runningSpeedModifierPerk.speedModifier;
            }

            //Rider
            var horseSpeedModifierPerk = employee.listOfPerks.FirstOrDefault(x => x.horseSpeedModifier != 1);
            if (horseSpeedModifierPerk != null)
            {
                horse_speed_perk_modifier *= horseSpeedModifierPerk.horseSpeedModifier;
            }

            //Talented
            var workContributionModifierPerk = employee.listOfPerks.FirstOrDefault(x => x.workUnitModifier > 1);
            if (workContributionModifierPerk != null)
            {
                work_contribution_perk_modifier *= workContributionModifierPerk.workUnitModifier;
            }

            //Trader
            var traderModifierPerk = employee.listOfPerks.FirstOrDefault(x => x.isTrader);
            if (traderModifierPerk != null)
            {
                isTrader = traderModifierPerk.isTrader;
            }

            //Grumpy
            var grumpyModifierPerk = employee.listOfPerks.FirstOrDefault(x => x.isGrumpy);
            if (grumpyModifierPerk != null)
            {
                isGrumpy = grumpyModifierPerk.isGrumpy;
            }

            //Brute
            var bruteModifierPerk = employee.listOfPerks.FirstOrDefault(x => x.isBrute);
            if (bruteModifierPerk != null)
            {
                isBrute = bruteModifierPerk.isBrute;
            }

            //Dubious
            var dubiousModifierPerk = employee.listOfPerks.FirstOrDefault(x => x.isDubious);
            if (dubiousModifierPerk != null)
            {
                isDubious = dubiousModifierPerk.isDubious;
            }

            SetSpeed(employee.speed * running_speed_perk_modifier);

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
            if (HasHorse)
                return;

            HasHorse = true;

            //Ignore/Reverse running speed modifiers
            var speed = agent.Speed / running_speed_perk_modifier * HORSE_SPEED_MODIFIER * horse_speed_perk_modifier;
            SetSpeed(speed);
        }

        public void TryKidnap()
        {
            //Check for Guard
            if (HasGuards || isDubious)
            {
                return;
            }

            //Kidnaps employee
            Destroy(gameObject);

        }

        public void SetFestivalSpeed(bool value)
        {
            if (HasGuards || isGrumpy)
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
                speedBeforeCabbageCart = agent.Speed;

                //Set speed to 0
                SetSpeed(0);
            }

            else
            {
                SetSpeed(speedBeforeCabbageCart);
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

