using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private Transform _projectileSpwanPointRight, _projectileSpwanPointLeft;
        [SerializeField] private GameObject _activeProjectile;
        [SerializeField] private GameObject _activeMuzzleFlash;
        [SerializeField] private PlayerController _playercontroller;


        public void FireRight() {
            Vector3 direction = _playercontroller.GetCrossheirPosition() - _projectileSpwanPointRight.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            Instantiate(_activeMuzzleFlash, _projectileSpwanPointRight.position, rotation);
            Instantiate(_activeProjectile, _projectileSpwanPointRight.position, rotation);
        }

        public void FireLeft() {

            Vector3 direction = _playercontroller.GetCrossheirPosition() - _projectileSpwanPointLeft.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            Instantiate(_activeMuzzleFlash, _projectileSpwanPointLeft.position, rotation);
            Instantiate(_activeProjectile, _projectileSpwanPointLeft.position, rotation);
        }
    }
}
