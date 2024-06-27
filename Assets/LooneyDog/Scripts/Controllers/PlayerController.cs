using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LooneyDog
{

    public class PlayerController : MonoBehaviour
    {
        public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }
        public Animator AniCtrl { get => _aniCtrl; set => _aniCtrl = value; }

        [SerializeField] private float _playerSpeed, _playerCrossheirDistance, _crossHeirReturnSpeed, _playerAimLineDistance, _playerAimSensitivity;

        [SerializeField] private Vector2 _playerDirection;

        [SerializeField] private Animator _aniCtrl;

        [SerializeField] private Transform _crossheir;
        [SerializeField] private LineRenderer _playerAimRenderer;
        [SerializeField] private Vector3 _aimStartPosition;
        [SerializeField] private float _testVariable;
        [SerializeField] private Transform _playerAimsprite;
        [SerializeField] private LayerMask _layerMask;



        [SerializeField] private Vector2 _maxCrossHeirMovement;
        private Vector2 moveInput;
        private Vector2 aimInput;
        private Vector3 _crossheirPosition;
        private Vector3 _rayhitPosition;



        private void OnEnable()
        {
            GameManager.Game.Level.PlayerController = this;
            
        }

        private void Update()
        {
            // _playerDirection = GameManager.Game.Screen.GameScreen.JoypadController.NormalizedInput;
            _playerDirection = moveInput;
            transform.position = new Vector3(transform.position.x + (_playerDirection.x * _playerSpeed * Time.deltaTime), transform.position.y, transform.position.z + (_playerDirection.y * _playerSpeed * Time.deltaTime));
            AniCtrl.SetFloat("InputX", _playerDirection.x);
            AniCtrl.SetFloat("InputY", _playerDirection.y);

            PlayerAimControl();
        }

        private void PlayerAimControl() {
            if (aimInput != Vector2.zero)
            {
                _aniCtrl.SetTrigger("AttackTrigger");
                float XCrosspos = Mathf.Clamp(_crossheir.position.x + (aimInput.x * _playerAimSensitivity * Time.deltaTime),-_maxCrossHeirMovement.x, _maxCrossHeirMovement.x);
                float ZCrosspos = Mathf.Clamp(_crossheir.position.z + (aimInput.y * _playerAimSensitivity * Time.deltaTime), -_maxCrossHeirMovement.y, _maxCrossHeirMovement.y);
                /*_crossheir.position = new Vector3(_crossheir.position.x + (aimInput.x * _playerAimSensitivity * Time.deltaTime), _crossheir.position.y, _crossheir.position.z + (aimInput.y * _playerAimSensitivity * Time.deltaTime));*/
                //_crossheir.position = new Vector3(XCrosspos, _crossheir.position.y, ZCrosspos);
                float XScreenCrosspos = Mathf.Clamp((aimInput.x * _playerAimSensitivity), -_maxCrossHeirMovement.x, _maxCrossHeirMovement.x);
                float ZScreenCrosspos = Mathf.Clamp((aimInput.y * _playerAimSensitivity), -_maxCrossHeirMovement.y, _maxCrossHeirMovement.y);


                _crossheir.position = _crossheir.position = new Vector3(XScreenCrosspos, _crossheir.position.y, ZScreenCrosspos);
            }
            else
            {
                _crossheirPosition = transform.position + (Vector3.up * (-1 * _playerCrossheirDistance));
                _crossheir.position = Vector3.Lerp(_crossheir.position, _crossheirPosition, _crossHeirReturnSpeed * Time.deltaTime);
            }
           

            _playerAimRenderer.SetPosition(0, transform.position - (Vector3.up * (-1)*_playerAimLineDistance));
            //_playerAimRenderer.SetPosition(0, _aimStartPosition);
            //_playerAimRenderer.SetPosition(1, _crossheir.position);
            _rayhitPosition = CastRayAndGetCollisionPoint(transform, _crossheir);
            _playerAimsprite.position = _rayhitPosition;
            _playerAimRenderer.SetPosition(1, _rayhitPosition);

        }


        Vector3 CastRayAndGetCollisionPoint(Transform origin, Transform target)
        {
            // Calculate the direction from the origin to the target
            Vector3 direction = target.position - origin.position;

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(origin.position, direction, out hit,100f,_layerMask))
            {
                // If the ray hits something, return the point of collision
                return hit.point;
            }

            // If the ray doesn't hit anything, return Vector3.zero
            return Vector3.zero;
        }
        public Vector3 GetCrossheirPosition() {
            
            return _rayhitPosition;
        }
        private void OnMove(InputValue input)
        {
            moveInput = input.Get<Vector2>();
        }

        private void OnMoveCanceled(InputValue input)
        {
            moveInput = Vector2.zero;
            
        }

        private void OnLook(InputValue input)
        {
            aimInput = input.Get<Vector2>();
        }

        private void OnLookCanceled(InputValue input)
        {
            aimInput = Vector2.zero;
            
        }

        /*public void OnMove(InputValue input)
        {
            _playerDirection = input.Get<Vector2>();

            Debug.Log("Move is being called");
        }

        public void OnLook(InputValue input)
        {
            _aniCtrl.SetTrigger("AttackTrigger");
            Debug.Log("Look is being called");
        }*/
    }

}
