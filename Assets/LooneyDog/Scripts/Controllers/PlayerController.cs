using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class PlayerController : MonoBehaviour
    {
        public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }

        [SerializeField] private float _playerSpeed;

        [SerializeField] private Vector2 _playerDirection;

        [SerializeField] private Animator _aniCtrl;

        private void Update()
        {
            _playerDirection = GameManager.Game.Screen.GameScreen.JoypadController.NormalizedInput;
            transform.position = new Vector3(transform.position.x + (_playerDirection.x * _playerSpeed * Time.deltaTime), transform.position.y,transform.position.z + (_playerDirection.y * _playerSpeed * Time.deltaTime));
            _aniCtrl.SetFloat("InputX", _playerDirection.x);
            _aniCtrl.SetFloat("InputY", _playerDirection.y);
        }


    }

}
