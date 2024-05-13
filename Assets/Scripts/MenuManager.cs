using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    
    [SerializeField] private Button soundButton;
    [SerializeField] private Image soundOn;
    [SerializeField] private Image soundOff;
    
    [SerializeField] private GameObject settingsDialog;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            AudioListener.volume = 1;
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
            
        }
        else
        {
            AudioListener.volume = 0;
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        playButton.onClick.AddListener(OpenGame);
        soundButton.onClick.AddListener(MuteSound);
    }

    public void OpenSettings(bool open)
    {
        settingsDialog.SetActive(open);
    }
  
    private void OpenGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MuteSound()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Sound", 0);
            AudioListener.volume = 0;
        }
        else if (PlayerPrefs.GetInt("Sound", 1) == 0)
        {
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Sound", 1);
            AudioListener.volume = 1;
        }
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(OpenGame);
        soundButton.onClick.RemoveListener(MuteSound);
    }
}
