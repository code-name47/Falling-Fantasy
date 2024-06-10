using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    public class GameScreen : MonoBehaviour
    {

        public JoypadController JoypadController { get => _joypadController; set => _joypadController = value; }
        public Button PauseButton { get => _pauseButton; set => _pauseButton = value; }

        [SerializeField] private JoypadController _joypadController;
        [SerializeField] private Button _pauseButton;

        
    }
}

