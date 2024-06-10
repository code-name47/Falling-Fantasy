using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace LooneyDog
{
    

    public class FallingLevel : MonoBehaviour
    {

        
        public Transform StartPoint { get => _startPoint; set => _startPoint = value; }
        public Transform EndPoint { get => _endPoint; set => _endPoint = value; }

        [SerializeField] private Transform _startPoint,_endPoint;
        [SerializeField] private Transform _envStartPoint, _envEndPoint;

        public void OnReachingEnd(Transform Object,ObjectId id)
        {
            switch (id)
            {
                case ObjectId.Environment:
                    Object.position = new Vector3(Object.position.x, _envStartPoint.position.y, Object.position.z);
                    break;
                case ObjectId.Obstacle:
                    Object.position = new Vector3(Object.position.x, _startPoint.position.y, Object.position.z);
                    break;
            }
            
        }




    }
}
