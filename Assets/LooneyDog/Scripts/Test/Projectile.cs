using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class Projectile : MonoBehaviour
    {
        [SerializeField] float _projectileSpeed;
        [SerializeField] GameObject _hitFlash;

        // Update is called once per frame
        void Update()
        {
            transform.position = transform.position + _projectileSpeed * Time.deltaTime * transform.forward;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(_hitFlash, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
