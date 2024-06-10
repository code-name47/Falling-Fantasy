using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
    
{

    public class ShopScreen : MonoBehaviour
    {
        public int MaxPage { get => _maxPage; set => _maxPage = value; }
        public Vector3 PageSetUp { get => pageSetUp; set => pageSetUp = value; }
        public float TweenTime { get => _tweenTime; set => _tweenTime = value; }
        public LeanTweenType TweenType { get => _tweenType; set => _tweenType = value; }

        [SerializeField] private int _maxPage;
        int currentPage;
        [SerializeField] private Vector3 pageSetUp;
        [SerializeField] RectTransform _levelPageRect;
        Vector3 _targetPosition;
        [SerializeField] private float _tweenTime;
        [SerializeField] LeanTweenType _tweenType;

        public void Next()
        {
            if(currentPage<_maxPage)
            {
                currentPage++;
                _targetPosition += pageSetUp;
                MovePage();

            }
        }

        public void Previous()
        {
          
        }

        void MovePage()
        {
            _levelPageRect.LeanMoveLocal(_targetPosition, TweenTime).setEase(TweenType);
        }



        private void Awake()
        {
            
        }

    }
}
