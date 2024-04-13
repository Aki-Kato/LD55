using Navigation.Employee;
using System.Collections.Generic;
using UnityEngine;

namespace Employees.Controllers
{
    public sealed class EmployeeController : MonoBehaviour
    {
        [SerializeField] private EmployeeAgent agent;

        private void Awake()
        {
            agent.FinalDestinationReached += Agent_FinalDestinationReached;
        }

        public void Initialise(float speed)
        {
            agent.Speed = speed;
        }

        public void SendBy(LinkedList<Vector3> pathPoints) =>
            agent.SendBy(pathPoints);


        private void Agent_FinalDestinationReached(EmployeeAgent obj)
        {
            throw new System.NotImplementedException();
        }

    }
}

