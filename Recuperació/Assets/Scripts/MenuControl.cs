using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    private GameObject continueButton;
    private GameObject newGameButton;
    private GameObject levelsButton;
    private GameObject optionsButton;
    private GameObject exitButton;
    private GameObject backButton;
    private GameObject musicImage;
    private GameObject musicToggle;
    private GameObject volumeImage;
    private GameObject volumeSlider;
    private GameObject fullscreenImage;
    private GameObject fullscreenToggle;
    private GameObject resolutionImage;
    private GameObject resolutionDropdown;
    private GameObject resetOptionsButton;
    private GameObject level1Button;
    private GameObject level2Button;
    private GameObject level3Button;
    private GameObject levelNotAllowed;

    private GameObject resumeButton;
    private GameObject mainMenuButton;

    public AudioMixer audioMixer;

    public bool silenced;
    public float lastVolume;

    Resolution[] resolutions;
    public bool delete;

    private void Awake()
    {
        if (GameObject.Find("ContinueButton") != null) continueButton = GameObject.Find("ContinueButton");
        if (GameObject.Find("NewGameButton") != null) newGameButton = GameObject.Find("NewGameButton");
        if (GameObject.Find("LevelsButton") != null) levelsButton = GameObject.Find("LevelsButton");
        if (GameObject.Find("OptionsButton") != null) optionsButton = GameObject.Find("OptionsButton");
        if (GameObject.Find("ExitButton") != null) exitButton = GameObject.Find("ExitButton");
        if (GameObject.Find("BackButton") != null) backButton = GameObject.Find("BackButton");
        if (GameObject.Find("musicImage") != null) musicImage = GameObject.Find("musicImage");
        if (GameObject.Find("musicToggle") != null) musicToggle = GameObject.Find("musicToggle");
        if (GameObject.Find("volumeImage") != null) volumeImage = GameObject.Find("volumeImage");
        if (GameObject.Find("volumeSlider") != null) volumeSlider = GameObject.Find("volumeSlider");
        if (GameObject.Find("fullscreenImage") != null) fullscreenImage = GameObject.Find("fullscreenImage");
        if (GameObject.Find("fullscreenToggle") != null) fullscreenToggle = GameObject.Find("fullscreenToggle");
        if (GameObject.Find("resolutionImage") != null) resolutionImage = GameObject.Find("resolutionImage");
        if (GameObject.Find("resolutionDropdown") != null) resolutionDropdown = GameObject.Find("resolutionDropdown");
        if (GameObject.Find("ResetOptionsButton") != null) resetOptionsButton = GameObject.Find("ResetOptionsButton");
        if (GameObject.Find("Level1Button") != null) level1Button = GameObject.Find("Level1Button");
        if (GameObject.Find("Level2Button") != null) level2Button = GameObject.Find("Level2Button");
        if (GameObject.Find("Level3Button") != null) level3Button = GameObject.Find("Level3Button");
        if (GameObject.Find("LevelNotAllowed") != null) levelNotAllowed = GameObject.Find("LevelNotAllowed");

        if (GameObject.Find("ResumeButton") != null) resumeButton = GameObject.Find("ResumeButton");
        if (GameObject.Find("MainMenuButton") != null) mainMenuButton = GameObject.Find("MainMenuButton");

        if (SceneManager.GetActiveScene().name == "Previous")
        {
            delete = true;
            StartCoroutine(ExecuteAfterTime(2.0f));
        }
    }

    // Use this for initialization
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "Previous")
        {
            if (SceneManager.GetActiveScene().name != "Menus")
            {
                resumeButton.SetActive(false);
                optionsButton.SetActive(false);
                mainMenuButton.SetActive(false);
                backButton.SetActive(false);
                musicImage.SetActive(false);
                musicToggle.SetActive(false);
                volumeImage.SetActive(false);
                volumeSlider.SetActive(false);
                fullscreenImage.SetActive(false);
                fullscreenToggle.SetActive(false);
                resolutionImage.SetActive(false);
                resolutionDropdown.SetActive(false);
                resetOptionsButton.SetActive(false);
            }
            else
            {
                goMainMenu();
            }
            silenced = GameControl.control.music;
            if (!silenced)
            {
                audioMixer.SetFloat("Volume", GameControl.control.volume);
                musicToggle.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                audioMixer.SetFloat("Volume", -80);
                musicToggle.GetComponent<Toggle>().isOn = false;
            }

            lastVolume = GameControl.control.volume;
            volumeSlider.GetComponent<Slider>().value = lastVolume;

            if (GameControl.control.fullscreen) fullscreenToggle.GetComponent<Toggle>().isOn = true;
            else fullscreenToggle.GetComponent<Toggle>().isOn = false;
            Screen.fullScreen = fullscreenToggle.GetComponent<Toggle>().isOn;

            resolutions = Screen.resolutions;

            resolutionDropdown.GetComponent<Dropdown>().ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == GameControl.control.resolution[0] && resolutions[i].height == GameControl.control.resolution[1]) currentResolutionIndex = i;
            }
            changeResolution(currentResolutionIndex);
            resolutionDropdown.GetComponent<Dropdown>().AddOptions(options);
            resolutionDropdown.GetComponent<Dropdown>().value = currentResolutionIndex;
            resolutionDropdown.GetComponent<Dropdown>().RefreshShownValue();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void goMainMenu()
    {
        continueButton.SetActive(true);
        if (!GameControl.control.continueAvailable)
        {
            continueButton.GetComponent<Image>().enabled = false;
            continueButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            continueButton.GetComponent<Image>().enabled = true;
            continueButton.GetComponent<Button>().enabled = true;
        }
        newGameButton.SetActive(true);
        levelsButton.SetActive(true);
        optionsButton.SetActive(true);
        exitButton.SetActive(true);
        backButton.SetActive(false);
        musicImage.SetActive(false);
        musicToggle.SetActive(false);
        volumeImage.SetActive(false);
        volumeSlider.SetActive(false);
        fullscreenImage.SetActive(false);
        fullscreenToggle.SetActive(false);
        resolutionImage.SetActive(false);
        resolutionDropdown.SetActive(false);
        resetOptionsButton.SetActive(false);
        level1Button.SetActive(false);
        level2Button.SetActive(false);
        level3Button.SetActive(false);
        levelNotAllowed.SetActive(false);
    }

    public void goOptionsMenu()
    {
        continueButton.SetActive(false);
        newGameButton.SetActive(false);
        levelsButton.SetActive(false);
        optionsButton.SetActive(false);
        exitButton.SetActive(false);
        backButton.SetActive(true);
        musicImage.SetActive(true);
        musicToggle.SetActive(true);
        volumeImage.SetActive(true);
        volumeSlider.SetActive(true);
        fullscreenImage.SetActive(true);
        fullscreenToggle.SetActive(true);
        resolutionImage.SetActive(true);
        resolutionDropdown.SetActive(true);
        resetOptionsButton.SetActive(true);
        level1Button.SetActive(false);
        level2Button.SetActive(false);
        level3Button.SetActive(false);
        levelNotAllowed.SetActive(false);
    }

    public void goLevelsMenu()
    {
        continueButton.SetActive(false);
        newGameButton.SetActive(false);
        levelsButton.SetActive(false);
        optionsButton.SetActive(false);
        exitButton.SetActive(false);
        backButton.SetActive(true);
        musicImage.SetActive(false);
        musicToggle.SetActive(false);
        volumeImage.SetActive(false);
        volumeSlider.SetActive(false);
        fullscreenImage.SetActive(false);
        fullscreenToggle.SetActive(false);
        resolutionImage.SetActive(false);
        resolutionDropdown.SetActive(false);
        resetOptionsButton.SetActive(false);
        level1Button.SetActive(true);
        level2Button.SetActive(true);
        level3Button.SetActive(true);
        levelNotAllowed.SetActive(false);
    }

    public void Fade(int level)
    {
        GameObject.Find("LevelChanger").GetComponent<levelChanger>().fadeToLevel(level);
    }

    public void newGame()
    {
        if (!GameControl.control.continueAvailable) GameControl.control.continueAvailable = true;

        GameControl.control.saveData();

        goToLevelNumber(0);
    }

    public void goToLevelNumber(int numLevel)
    {
        if (numLevel == 3) numLevel = GameControl.control.lastLevelPlayed - 1;

        GameControl.control.firstCheck = true;

        switch (numLevel)
        {
            case 0:
                GameControl.control.lastLevelPlayed = 1;
                Fade(numLevel);
                break;
            case 1:
                if (GameControl.control.levels[numLevel])
                {
                    GameControl.control.lastLevelPlayed = 2;
                    Fade(numLevel);
                }
                else
                {
                    levelNotAllowed.SetActive(true);
                    StartCoroutine(LevelNotAllowed(2));
                }
                break;
            case 2:
                if (GameControl.control.levels[numLevel])
                {
                    GameControl.control.lastLevelPlayed = 3;
                    Fade(numLevel);
                }
                else
                {
                    levelNotAllowed.SetActive(true);
                    StartCoroutine(LevelNotAllowed(2));
                }
                break;
            default:
                break;
        }
    }

    public void resumeGame()
    {
        resumeButton.SetActive(false);
        optionsButton.SetActive(false);
        mainMenuButton.SetActive(false);
        backButton.SetActive(false);
        musicImage.SetActive(false);
        musicToggle.SetActive(false);
        volumeImage.SetActive(false);
        volumeSlider.SetActive(false);
        fullscreenImage.SetActive(false);
        fullscreenToggle.SetActive(false);
        resolutionImage.SetActive(false);
        resolutionDropdown.SetActive(false);
        resetOptionsButton.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        GameControl.control.gamePaused = false;
        GameControl.control.gameMusic.Play();
        GameControl.control.planeSound.Play();
    }

    public void goPauseMenu()
    {
        resumeButton.SetActive(true);
        optionsButton.SetActive(true);
        mainMenuButton.SetActive(true);
        backButton.SetActive(false);
        musicImage.SetActive(false);
        musicToggle.SetActive(false);
        volumeImage.SetActive(false);
        volumeSlider.SetActive(false);
        fullscreenImage.SetActive(false);
        fullscreenToggle.SetActive(false);
        resolutionImage.SetActive(false);
        resolutionDropdown.SetActive(false);
        resetOptionsButton.SetActive(false);
    }

    public void goOptionsFromPause()
    {
        resumeButton.SetActive(false);
        optionsButton.SetActive(false);
        mainMenuButton.SetActive(false);
        backButton.SetActive(true);
        musicImage.SetActive(true);
        musicToggle.SetActive(true);
        volumeImage.SetActive(true);
        volumeSlider.SetActive(true);
        fullscreenImage.SetActive(true);
        fullscreenToggle.SetActive(true);
        resolutionImage.SetActive(true);
        resolutionDropdown.SetActive(true);
        resetOptionsButton.SetActive(true);
    }

    public void goMainMenuFromPause()
    {
        //GameControl.control.firstCheck = true;
        GameControl.control.lastLevelPlayed = GameControl.control.level;
        GameControl.control.saveData();
        Fade(3);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }

    public void loadLevel(int levelNum)
    {
        if (levelNum == 3)
        {
            SceneManager.LoadScene("Menus");
        }
        else
        {
            if (GameControl.control.levels[levelNum])
            {
                switch (levelNum)
                {
                    case 0:
                        SceneManager.LoadScene("Level1");
                        break;
                    case 1:
                        SceneManager.LoadScene("Level2");
                        break;
                    case 2:
                        SceneManager.LoadScene("Level3");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                levelNotAllowed.SetActive(true);
                StartCoroutine(LevelNotAllowed(2));
            }
        }
    }

    public void back()
    {
        if (musicImage.activeSelf) GameControl.control.saveData();
        goMainMenu();
    }

    public IEnumerator LevelNotAllowed(float time)
    {
        yield return new WaitForSeconds(time);

        levelNotAllowed.SetActive(false);
    }

    public void activateMusic(bool isOn)
    {
        silenced = !isOn;
        if (silenced) audioMixer.SetFloat("Volume", -80);
        else audioMixer.SetFloat("Volume", lastVolume);
        GameControl.control.music = !isOn;
    }

    public void regulateVolume(float volume)
    {
        if (!silenced)
        {
            audioMixer.SetFloat("Volume", volume);
        }
        lastVolume = volume;
        GameControl.control.volume = volume;
    }

    public void activateFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        GameControl.control.fullscreen = isFullscreen;
    }

    public void changeResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        GameControl.control.resolution[0] = resolutions[resolutionIndex].width;
        GameControl.control.resolution[1] = resolutions[resolutionIndex].height;
    }

    public void resetOptions()
    {
        musicToggle.GetComponent<Toggle>().isOn = true;
        GameControl.control.music = false;
        volumeSlider.GetComponent<Slider>().value = -10;
        audioMixer.SetFloat("Volume", -10);
        GameControl.control.volume = -10;
        fullscreenToggle.GetComponent<Toggle>().isOn = true;
        activateFullscreen(true);
        resolutionDropdown.GetComponent<Dropdown>().value = resolutions.Length;
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        GameControl.control.resolution[0] = 1920;
        GameControl.control.resolution[1] = 1080;
    }

    public IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject.Find("Fade").GetComponent<FadeStart>().FadeIn();
    }
}
