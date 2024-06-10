using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace LooneyDog
{
    public class MovingObject : MonoBehaviour
    {
        public float MovingSpeed { get => _movingSpeed; set => _movingSpeed = value; }
        public FallingLevel ParentLevel { get => _parentLevel; set => _parentLevel = value; }

        [SerializeField] private float _movingSpeed;
        [SerializeField] private FallingLevel _parentLevel;
        [SerializeField] private ObjectId _id;
        
        private void Update()
        {
            transform.position= new Vector3(transform.position.x,(transform.position.y+(_movingSpeed*Time.deltaTime)),transform.position.z);
        }
        
       private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("EndPoint")){
                _parentLevel.OnReachingEnd(transform,_id);
        }
       }

        
        
    }
    public enum ObjectId
    {
        Environment = 0,
        Obstacle = 1
    }

}