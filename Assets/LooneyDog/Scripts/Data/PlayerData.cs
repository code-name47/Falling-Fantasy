using UnityEngine;
using System;

namespace LooneyDog
{
    [Serializable]
    public class PlayerData
    {
        public int level_Number { get { return LEVEL_NUMBER; } private set { LEVEL_NUMBER = value; } }
        public int coins_Balance { get { return COINS_BALANCE; } set { COINS_BALANCE = value; } }
        public int score_Balance { get { return SCORE_BALANCE; } set { SCORE_BALANCE = value; } }
        public int weekly_Score { get { return WEEKLY_SCORE; } set { WEEKLY_SCORE = value; } }
        public bool ads_Free { get { return ADS_FREE; } set { ADS_FREE = value; } }
        public bool sound_State { get { return SOUND_STATE; } set { SOUND_STATE = value; } }
        public bool music_State { get { return MUSIC_STATE; } set { MUSIC_STATE = value; } }
        public bool vibrate_State { get { return VIBRATE_STATE; } set { VIBRATE_STATE = value; } }
        public bool notif_State { get { return NOTIF_STATE; } set { NOTIF_STATE = value; } }
        public bool rate_State { get { return RATE_STATE; } set { RATE_STATE = value; } }
        public bool relevant_Ads { get { return RELEVANT_ADS; } set { RELEVANT_ADS = value; } }
        public bool synced { get { return DATA_SYNCED; } set { DATA_SYNCED = value; } }
        public ScreenOrientation orientation { get { return ORIENTATION; } set { ORIENTATION = value; } }

        public int Spin_Fill_Time { get { return 86400; } }
        public int Reveal_Letter_Cost { get { return 10; } }

        public int Character_Skin { get { return CHARACTER_SKIN; } set { CHARACTER_SKIN = value; } }
        public int Ship_Skin { get { return SHIP_SKIN; } set { SHIP_SKIN = value; } }

        public int Gun_Skin { get { return GUN_SKIN; } set { GUN_SKIN = value; } }

        public int OneHandedMalee_Skin { get { return ONEHANDEDMALEE_SKIN; } set { ONEHANDEDMALEE_SKIN = value; } }

        [SerializeField, HideInInspector]
        private int SHIP_SKIN;
        [SerializeField, HideInInspector]
        private int CHARACTER_SKIN;
        [SerializeField, HideInInspector]
        private int GUN_SKIN;
        [SerializeField, HideInInspector]
        private int ONEHANDEDMALEE_SKIN;
        [SerializeField]
        private int LEVEL_NUMBER = 1;
        [SerializeField]
        private int COINS_BALANCE = 100;
        [SerializeField]
        private int SCORE_BALANCE;
        [SerializeField]
        private int WEEKLY_SCORE;
        [SerializeField]
        private ScreenOrientation ORIENTATION = ScreenOrientation.Portrait;
        [SerializeField]
        private bool ADS_FREE;
        [SerializeField]
        private bool RATE_STATE;
        [SerializeField]
        private bool RELEVANT_ADS;
        [SerializeField]
        private bool MUSIC_STATE;
        [SerializeField]
        private bool SOUND_STATE = true;
        [SerializeField]
        private bool VIBRATE_STATE = true;
        [SerializeField]
        private bool NOTIF_STATE = true;
        [SerializeField]
        private bool DATA_SYNCED;
        [SerializeField]
        private string SPINTIME;
        [SerializeField]
        private string SCORETIME;
        internal bool promo;

        void Awake()
        {
            /*GameManager.Game.Skin.
            SetSelectedcharacterSkin((CharacterSkins)CHARACTER_SKIN);*/
            ONEHANDEDMALEE_SKIN = 0;
        }
       
        public void Credit(int coins)
        {
            COINS_BALANCE += coins;
        }

        public void Score(int score)
        {
            SCORE_BALANCE += score;
            WEEKLY_SCORE += score;
        }

        public void ChangeSoundState(bool state)
        {
            SOUND_STATE = state;
        }

        public void ChangeMusicState(bool state)
        {
            MUSIC_STATE = state;
        }

        public void ChangeVibrateState(bool state)
        {
            VIBRATE_STATE = state;
        }

        public void ChangeNotificationState()
        {
            NOTIF_STATE = !NOTIF_STATE;
        }

        public void PuzzleSolved()
        {
            LEVEL_NUMBER++;
        }

        public DateTime GetWeeklyScoreTime()
        {
            if (!string.IsNullOrEmpty(SCORETIME))
            {
                return DateTime.FromBinary(Convert.ToInt64(SCORETIME));
            }

            return DateTime.Now.AddDays(-8);
        }

        public void SetWeeklyScoreTime()
        {
            if (DateTime.Now.Subtract(GetWeeklyScoreTime()).Days > 7)
            {
                WEEKLY_SCORE = 0;
                SCORETIME = DateTime.Today.AddDays(-(int)DateTime.Now.DayOfWeek).ToBinary().ToString();
            }
        }

        public DateTime GetSpinTime()
        {
            if (!string.IsNullOrEmpty(SPINTIME))
            {
                return DateTime.FromBinary(Convert.ToInt64(SPINTIME));
            }

            return DateTime.Now.AddDays(-2);
        }

        public void SetSpinTime()
        {
            SPINTIME = DateTime.Now.ToBinary().ToString();
        }

        public bool IsSpinActive()
        {
            float timeLapsed = (float)DateTime.Now.Subtract(GetSpinTime()).TotalSeconds;
            float totalWaitTimeLeft = Spin_Fill_Time - timeLapsed;

            return totalWaitTimeLeft <= 0;
        }
    }
}
