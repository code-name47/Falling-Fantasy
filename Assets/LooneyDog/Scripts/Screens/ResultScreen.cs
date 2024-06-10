using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{
    public class ResultScreen : MonoBehaviour
    {
        public Button BackButton { get => _backButton; set => _backButton = value; }
        public float ResultScreentransitionSpeed { get => _resultScreentransitionSpeed; set => _resultScreentransitionSpeed = value; }

        [SerializeField] private Button _backButton;
        [SerializeField] private float _resultScreentransitionSpeed;




        // Start is called before the first frame update
        private void Awake()
        {


            _backButton.onClick.AddListener(OnClickBack);


        }


        private void OnClickBack()
        {

            GameManager.Game.Screen.ClosePopUpScreen(transform,ScreenLocation.Pop, _resultScreentransitionSpeed);


        }

    }

}
