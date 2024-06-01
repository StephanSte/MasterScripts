using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace ST
{
    public class UIManager : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        public EquipmentWindowUI equipmentWindowUI;
        public Text _Text;
        public Text _Text2;
        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject equipmentScreenWindow;
        public GameObject weaponInventoryWindow;
        public GameObject pauseMenu;
        public GameObject beginMenu;

        [Header("Equipment Window Slot Selected")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotsParent;
        WeaponInventorySlot[] weaponInventorySlots;
        private CameraHandler _cameraHandler;
        private float looksens = 0.01f;
        private float pivotsens = 0.01f;

        public bool GameIsPaused = false;

        private void Awake()
        {

        }

        private void Start()
        {
            _cameraHandler = FindObjectOfType<CameraHandler>();
            weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
            _Text.text = _cameraHandler.lookSpeed.ToString();
            Time.timeScale = 0f;
        }

        public void UpdateUI()
        {
            #region Weapon Inventory Slots
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerInventory.weaponsInventory.Count)
                {
                    if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion
        }
        
        public void increaseSens()
        {
            _cameraHandler = FindObjectOfType<CameraHandler>();
            looksens *= (1f + 0.1f);
            _cameraHandler.lookSpeed = looksens;
            _Text.text = looksens.ToString();
        }
        
        public void decreaseSens()
        {
            _cameraHandler = FindObjectOfType<CameraHandler>();
            looksens *= (1f + (-0.1f));
            _cameraHandler.lookSpeed = looksens;
            _Text.text = looksens.ToString();
        }
        
        public void setSens()
        {
            _cameraHandler = FindObjectOfType<CameraHandler>();
            _cameraHandler.lookSpeed = looksens;
            _Text.text = looksens.ToString();
        }
        
        public void decreaseSensPivot()
        {
            _cameraHandler = FindObjectOfType<CameraHandler>();
            pivotsens *= (1f + (-0.1f));
            _cameraHandler.pivotSpeed = pivotsens;
            _Text2.text = pivotsens.ToString();
        }
        
        public void increaseSensPivot()
        {
            _cameraHandler = FindObjectOfType<CameraHandler>();
            pivotsens *= (1f + 0.1f);
            _cameraHandler.pivotSpeed = pivotsens;
            _Text2.text = pivotsens.ToString();
        }
        
        public void link()
        {
            Application.OpenURL("https://forms.gle/27SQqvWEER9xVnpcA");
        }
        
        public void beginMenuClose()
        {
            Cursor.lockState = CursorLockMode.None;
            beginMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }

        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }
        
        public void OpenPauseWindow()
        {
            pauseMenu.SetActive(true);
            PauseGame();
        }

        public void ClosePauseWindow()
        {
            pauseMenu.SetActive(false);
            ResumeGame();
        }

        public void CloseAllInventoryWindows()
        {
            ResetAllSelectedSlots();
            weaponInventoryWindow.SetActive(false);
            equipmentScreenWindow.SetActive(false);
        }

        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;
        }
        
        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            hudWindow.SetActive(true);
            Time.timeScale = 1f;
            GameIsPaused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void PauseGame()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        public void LoadMenu()
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
            Debug.Log("Going to main menu");
            SceneManager.LoadScene("Main Menu");
        }
        
        public void LoadNextTest()
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
            SceneManager.LoadScene("Main Menu");
        }
        
        public void QuitGame()
        {
            Debug.Log("Quitting");
            try
            {
                TestParameter endTests = FindObjectOfType<TestParameter>();
                endTests.endTest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Application.Quit();
            }
            Application.Quit();
        }
        
        public void openLink() {
            try
            {
                TestManager _testmanager = FindObjectOfType<TestManager>();
                string path = _testmanager.filePath.Substring(0, _testmanager.filePath.Length - "/NewFile1.txt".Length);
                Process.Start(path);
            }
            catch (Win32Exception win32Exception)
            {
                //The system cannot find the file specified...
                Console.WriteLine(win32Exception.Message);
            }
        }
    }
}