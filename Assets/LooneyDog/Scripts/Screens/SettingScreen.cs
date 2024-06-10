using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{

    public class SettingScreen : MonoBehaviour
    { 
        [SerializeField] private Button _settingBackButton;
        [SerializeField] private float _screenTransitionSpeed;

        private void Awake()
        {
            _settingBackButton.onClick.AddListener(OnClickBack);
        }

        private void OnClickBack()
        {

            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _screenTransitionSpeed);
            //GameManager.Game.Screen.LoadFadeScreen(transform,GameManager.Game.Screen.Home.transform,);

        }
        
    }
}