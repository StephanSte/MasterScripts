using System;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

namespace ST
{
    public class TestManager : MonoBehaviour
    {
        public GameObject[] bosses;
        public GameObject[] _player;
        public GameObject camera;
        public int testiD = 0;
        public GameObject nextTestUI;
        public GameObject nextTestUIButton;
        public GameObject Endscreen;
        public TextMeshProUGUI EndscreenPfad;

        private UIManager _uiManager;
        private TestParameter _testParameter;
        private TestAmountDisplay _testAmountDisplay;
        public int countdownCur;
        private int countdownMax = 150;
        public string filePath;
        public bool activatedChange = false;
        private bool first = true;
        private bool setLook = false;

        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _testParameter = FindObjectOfType<TestParameter>();
            _testAmountDisplay = FindObjectOfType<TestAmountDisplay>();
            testiD = 0;
            startTest();
            _uiManager.setSens();
        }

        private void FixedUpdate()
        {
            if (countdownCur > 0)
            {
                countdownCur--;
                return;
            }
            
            if (countdownCur <= 0 && !activatedChange)
            {
                switch (testiD)
                {
                    case 3:
                    {
                        PlayerInventory inv = FindObjectOfType<PlayerInventory>();
                        inv.AddCustomFlaskAmount(3);
                        break;
                    }
                    case 4:
                    {
                        DamageCollider invs = FindObjectOfType<DamageCollider>();
                        invs.currentWeaponDamage = 50;
                        break;
                    }
                    case 5:
                    {
                        DamageCollider invs = FindObjectOfType<DamageCollider>();
                        invs.currentWeaponDamage = 12;
                        break;
                    }
                    case 11:
                    {
                        PlayerStats stats = FindObjectOfType<PlayerStats>();
                        stats.currentHealth = 50;
                        break;
                    }

                    case 22:
                    {
                        Action_Attack attack = FindObjectOfType<Action_Attack>();
                        attack.ranomAttackDamage = true;
                        break;
                    }
                    default: break;
                }

                activatedChange = true;
            }

            
        }

        private void Update()
        {
            if (setLook && FindObjectOfType<CameraHandler>())
            {
                _uiManager.setSens();
                setLook = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void startTest()
        {
            if (first)
            {
                first = false;
            }
            else
            {
                testiD++;
            }

            if (testiD >= bosses.Length)
            {
                filePath = _testParameter.endTest();
                nextTestUI.SetActive(false);
                Endscreen.SetActive(true);
                EndscreenPfad.text += filePath;
                return;
            }

            nextTestUI.SetActive(false);

            Instantiate(bosses[testiD]);
            Instantiate(_player[testiD]);

            camera.SetActive(false);

            _testParameter.startTest();
            _testAmountDisplay.SetTestAmountText(testiD, _player.Length);

            Debug.Log("started Test: " + testiD);
            countdownCur = countdownMax;
            activatedChange = false;
            setLook = true;
        }

        public void restartTest()
        {
            PlayerParent player = FindObjectOfType<PlayerParent>();
            EnemyStats enemy = FindObjectOfType<EnemyStats>();

            player.gameObject.SetActive(false);
            enemy.gameObject.SetActive(false);

            Destroy(player.gameObject);
            Destroy(enemy.gameObject);

            Instantiate(bosses[testiD]);
            Instantiate(_player[testiD]);
            countdownCur = countdownMax;
            activatedChange = false;
            setLook = true;

        }

        public void skipTest()
        {
            _uiManager.ClosePauseWindow();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            endTest();
            _testParameter.skipedTest();
        }

        public void endTest()
        {
            _testParameter.skipedTestFalse();
            _testParameter.calcTimeElapsed();
            nextTestUI.SetActive(true);
            camera.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            PlayerParent player = FindObjectOfType<PlayerParent>();
            EnemyStats enemy = FindObjectOfType<EnemyStats>();

            player.gameObject.SetActive(false);
            enemy.gameObject.SetActive(false);

            Destroy(player.gameObject);
            Destroy(enemy.gameObject);
        }
    }
}