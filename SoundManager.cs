using ST;
using UnityEngine;
using UnityEngine.UI;


namespace Manager
{
    
    public class SoundManager: MonoBehaviour
    {
        [SerializeField] Slider volumeSlider;

        private CameraHandler _cameraHandler;

        private void Start()
        {
            _cameraHandler = FindObjectOfType<CameraHandler>();
        }
        
        public void ChangeVolume()
        {
            float percentage = volumeSlider.value / 100f;
            _cameraHandler.lookSpeed *= (1f + percentage);
        }

        private void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        private void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }
    }
}