using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LooneyDog
{
    public class GunTest : MonoBehaviour
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private GameObject _projectile, _muzzleFlash;
        [SerializeField] private Transform _spwanPoint;

        [SerializeField] private float _fireRate;

        private void OnFire(InputValue input) {
            Vector3 direction = (transform.forward * 5f)- _spwanPoint.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            Instantiate(_projectile,_spwanPoint.position, _spwanPoint.rotation);
            Instantiate(_muzzleFlash, _spwanPoint.position, _spwanPoint.rotation);
        }

        
    }
}
