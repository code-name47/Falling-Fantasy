using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
namespace LooneyDog
{
    public class ScreenManager : MonoBehaviour
    {
        public GameScreen Game { get { return _game; } set { _game = value; } }
        public ResultScreen Result { get { return _result; } set { _result = value; } }
        public SplashScreen Splash { get { return _splash; } set { _splash = value; } }
        public ReviveScreen Revive { get { return _revive; } set { _revive = value; } }
        public SettingScreen Setting { get { return _setting; } set { _setting = value; } }
        public HomeScreen Home { get { return _home; } set { _home = value; } }
        public LoadingScreen Load { get { return _load; } set { _load = value; } }
        public ShopScreen Shop { get { return _shop; } set { _shop = value; } }


        [SerializeField] private GameScreen _game;
        [SerializeField] private ResultScreen _result;
        [SerializeField] private SplashScreen _splash;
        [SerializeField] private ReviveScreen _revive;
        [SerializeField] private SettingScreen _setting;
        [SerializeField] private HomeScreen _home;
        [SerializeField] private LoadingScreen _load;
        [SerializeField] private ShopScreen _shop;

        public Vector3 _gamePosition;
        public Vector3 _resultPosition;
        public Vector3 _splashPosition;
        public Vector3 _revivePosition;
        public Vector3 _settingPosition;
        public Vector3 _homePosition;

        [SerializeField] private float ScreenYOffset = 2000;

        private void Awake()
        {
            _gamePosition = _game.transform.position;
            //_resultPosition = _result.transform.position;
            //_splashPosition  = _splash.transform.position;
            //_revivePosition = _revive.transform.position;
            //_settingPosition = _setting.transform.position;
            //_homePosition = _home.transform.position;

            //_game.transform.position    = _gamePosition - new Vector3(0, ScreenYOffset, 0);
            //_result.transform.position  = _resultPosition - new Vector3(0, ScreenYOffset, 0);
            //_splash.transform.position  = _splashPosition - new Vector3(0, ScreenYOffset, 0);
            //_revive.transform.position  = _revivePosition - new Vector3(0, ScreenYOffset, 0);
            //_setting.transform.position = _settingPosition - new Vector3(0, ScreenYOffset, 0);
            //_home.transform.position    = _homePosition - new Vector3(0, ScreenYOffset, 0);
        }
        /// <summary>
        /// This function Changes the screen from one screen to desired screen
        /// </summary>
        /// <param name="FromScreen">The Current Screen From which this function is called</param>
        /// <param name="ToScreen">The next screen which is to be loaded</param>
        /// <param name="initialPosition">The intialposition variable on available in ScreenManager</param>
        public void LoadScreen(GameObject FromScreen, GameObject ToScreen, Vector3 initialPosition)
        {
            ToScreen.SetActive(true);
            FromScreen.transform.DOMoveY(-ScreenYOffset, 1f)
                .OnComplete(() => {
                    FromScreen.SetActive(false);
                    ToScreen.transform.DOMoveY(initialPosition.y, 1f);
                });
        }

        public void LoadFadeScreen(GameObject FromScreen, GameObject ToScreen)
        {
            Image[] FromScreenImages = FromScreen.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] FromScreenText = FromScreen.GetComponentsInChildren<TextMeshProUGUI>();

            Image[] ToScreenImages = ToScreen.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] ToScreenText = ToScreen.GetComponentsInChildren<TextMeshProUGUI>();
            float[] ImageAplhaRef = new float[ToScreenImages.Length];
            float[] TextAplhaRef = new float[ToScreenText.Length];

            for (int i = 0; i < ToScreenImages.Length; i++)
            {
                ImageAplhaRef[i] = ToScreenImages[i].color.a;
            }

            for (int i = 0; i < ToScreenText.Length; i++)
            {
                TextAplhaRef[i] = ToScreenText[i].color.a;
            }

            fadeScreen(FromScreenImages, FromScreenText);
            setAlpha(ToScreenImages, ToScreenText, 0);
            ToScreen.SetActive(true);
            UnfadeScreen(ToScreenImages, ToScreenText, ImageAplhaRef, TextAplhaRef);
            StartCoroutine(DisableScreenAfter(2, FromScreen));

        }



        private void fadeScreen(Image[] images, TextMeshProUGUI[] texts)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOFade(0, 1f);
            }
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].DOFade(0, 1f);
            }
        }

        private void setAlpha(Image[] images, TextMeshProUGUI[] texts, float alphaValue)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color =
                    new Color((byte)images[i].color.r
                    , (byte)images[i].color.g
                    , (byte)images[i].color.b
                    , alphaValue);
            }
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color =
                    new Color((byte)texts[i].color.r
                    , (byte)texts[i].color.g
                    , (byte)texts[i].color.b
                    , alphaValue);
            }


        }

        private void UnfadeScreen(Image[] images, TextMeshProUGUI[] texts, float[] imageAplhaRef, float[] textAplhaRef)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOFade(imageAplhaRef[i], 2f);
            }
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].DOFade(textAplhaRef[i], 2f);
            }
        }

        private IEnumerator DisableScreenAfter(float seconds, GameObject Screen)
        {
            yield return new WaitForSeconds(seconds);
            Screen.SetActive(false);
            setAlpha(Screen.GetComponentsInChildren<Image>(), Screen.GetComponentsInChildren<TextMeshProUGUI>(), 255);
        }

        public void LoadPopScreen(GameObject FromScreen, GameObject ToScreen)
        {
            FromScreen.transform.DOScale(0, 1f).SetEase(Ease.OutFlash).
                  OnComplete(() => {
                      FromScreen.SetActive(false);
                      FromScreen.transform.localScale = Vector3.one;
                      ToScreen.transform.localScale = Vector3.zero;
                      ToScreen.SetActive(true);
                      ToScreen.transform.DOScale(1, 1f).SetEase(Ease.OutBounce)
                      .OnComplete(() => {
                          ToScreen.transform.localScale = Vector3.one;
                      });
                  });

        }
        public void OpenPopUpScreen(Transform PopUpScreen, ScreenLocation startlocation, float Duration)
        {
            switch (startlocation)
            {
                case ScreenLocation.left:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x - Screen.currentResolution.width,
                        PopUpScreen.transform.position.y, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x + Screen.currentResolution.width, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.right:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x + Screen.currentResolution.width,
                        PopUpScreen.transform.position.y, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x - Screen.currentResolution.width, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.up:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x,
                      PopUpScreen.transform.position.y + Screen.currentResolution.height, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y - Screen.currentResolution.height, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.down:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x,
                     PopUpScreen.transform.position.y - Screen.currentResolution.height, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y + Screen.currentResolution.height, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.Pop:
                    PopUpScreen.transform.localScale = Vector3.zero;

                    PopUpScreen.DOScale(Vector3.one, Duration).OnStart(() => {
                        PopUpScreen.gameObject.SetActive(true);
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
            }


        }

         public void OpenPopUpScreen(Transform PopUpScreen, ScreenLocation startlocation, float Duration,Button toBeDisabled)
        {
            switch (startlocation)
            {
                case ScreenLocation.left:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x - Screen.currentResolution.width,
                        PopUpScreen.transform.position.y, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x + Screen.currentResolution.width, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                        toBeDisabled.interactable=false;
                    }).OnComplete(()=>{
                        toBeDisabled.interactable=true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.right:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x + Screen.currentResolution.width,
                        PopUpScreen.transform.position.y, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x - Screen.currentResolution.width, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                         toBeDisabled.interactable=false;
                    }).OnComplete(()=>{
                        toBeDisabled.interactable=true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.up:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x,
                      PopUpScreen.transform.position.y + Screen.currentResolution.height, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y - Screen.currentResolution.height, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                         toBeDisabled.interactable=false;
                    }).OnComplete(()=>{
                        toBeDisabled.interactable=true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.down:
                    PopUpScreen.transform.position = new Vector3(PopUpScreen.transform.position.x,
                     PopUpScreen.transform.position.y - Screen.currentResolution.height, PopUpScreen.transform.position.z);
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y + Screen.currentResolution.height, Duration).OnStart(() =>
                    {
                        PopUpScreen.gameObject.SetActive(true);
                         toBeDisabled.interactable=false;
                    }).OnComplete(()=>{
                        toBeDisabled.interactable=true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.Pop:
                    PopUpScreen.transform.localScale = Vector3.zero;

                    PopUpScreen.DOScale(Vector3.one, Duration).OnStart(() => {
                        PopUpScreen.gameObject.SetActive(true);
                         toBeDisabled.interactable=false;
                    }).OnComplete(()=>{
                        toBeDisabled.interactable=true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
            }


        }

        public void ClosePopUpScreen(Transform PopUpScreen, ScreenLocation startlocation, float Duration)
        {
            Vector3 initialposition = PopUpScreen.position;
            switch (startlocation)
            {
                case ScreenLocation.left:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x - Screen.currentResolution.width, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.right:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x + Screen.currentResolution.width, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.up:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y + Screen.currentResolution.height, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.down:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y - Screen.currentResolution.height, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.Pop:
                    //PopUpScreen.transform.localScale = Vector3.zero;
                    PopUpScreen.DOScale(Vector3.zero, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.localScale = Vector3.one;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
            }
        }

        public void ClosePopUpScreen(Transform PopUpScreen, ScreenLocation startlocation, float Duration, Button DisableButton)
        {
            Vector3 initialposition = PopUpScreen.position;
            DisableButton.interactable = false;
            switch (startlocation)
            {
                case ScreenLocation.left:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x - Screen.currentResolution.width, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                        DisableButton.interactable = true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.right:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveX(PopUpScreen.transform.position.x + Screen.currentResolution.width, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                        DisableButton.interactable = true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.up:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y + Screen.currentResolution.height, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                        DisableButton.interactable = true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.down:
                    //PopUpScreen.transform.position = Vector3.zero;
                    PopUpScreen.DOMoveY(PopUpScreen.transform.position.y - Screen.currentResolution.height, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.position = initialposition;
                        DisableButton.interactable = true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
                case ScreenLocation.Pop:
                    //PopUpScreen.transform.localScale = Vector3.zero;
                    PopUpScreen.DOScale(Vector3.zero, Duration).OnComplete(() =>
                    {
                        PopUpScreen.gameObject.SetActive(false);
                        PopUpScreen.transform.localScale = Vector3.one;
                        DisableButton.interactable = true;
                    }).SetEase(Ease.OutBounce).SetUpdate(true);
                    break;
            }
        }


    }
    public enum ScreenLocation
    {
        left = 1,
        right = 2,
        up = 3,
        down = 4,
        Pop = 5
    }
}