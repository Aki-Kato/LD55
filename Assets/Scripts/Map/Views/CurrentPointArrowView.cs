using Navigation.Controllers;
using Navigation.Input;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Map.Views
{
    [RequireComponent(typeof(Button))]
    public sealed class CurrentPointArrowView : MonoBehaviour
    {
        public event Action<GraphNodeController> Selected;

        private Button _button;
        private GraphNodeController _graphNode;

        private Direction currentDirection;

        private bool _inputBlocked;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Selected?.Invoke(_graphNode);
        }

        private void Update()
        {
            if (((Input.GetKeyDown(KeyCode.DownArrow) && currentDirection == Direction.Down) || 
                (Input.GetKeyDown(KeyCode.UpArrow) && currentDirection == Direction.Up) || 
                (Input.GetKeyDown(KeyCode.LeftArrow) && currentDirection == Direction.Left) || 
                (Input.GetKeyDown(KeyCode.RightArrow) && currentDirection == Direction.Right)) &&
                !NavigationInput.Blocked)
            {
                OnClicked();
                NavigationInput.Blocked = true;
                UnblockInputAsync();
            }
        }

        private async void UnblockInputAsync()
        {
            await Task.Delay(50);
            NavigationInput.Blocked = false;
        }
        
        public void LookAt(Vector3 destination, GraphNodeController node)
        {
            Vector3 rotateTowards = destination - transform.position;
            transform.rotation = Quaternion.LookRotation(transform.forward, rotateTowards);
            _graphNode = node;

            CalculateDirection();
        }

        public void CalculateDirection()
        {
            float number = transform.rotation.eulerAngles.z;
            float multiple = 90f;

            int result;
            
            float remainder = number % multiple;
            if (Math.Abs(remainder) < multiple / 2)
                result = (int)(number - remainder);
            else
                result = (int)(number + (multiple - Math.Abs(remainder)) * Math.Sign(remainder));

            switch (result)
            {
                case 0:
                case 360:
                    currentDirection = Direction.Up;
                    break;
                case 90:
                    currentDirection = Direction.Left;
                    break;
                case 180:
                    currentDirection = Direction.Down;
                    break;
                case 270:
                    currentDirection = Direction.Right;
                    break;
                default:
                    currentDirection = Direction.Up;
                    Debug.Log("Direction not normalized");
                    break;
            }
        }
    }

    enum Direction
    {
        Up,
        Right,
        Down,
        Left,
    }
}
