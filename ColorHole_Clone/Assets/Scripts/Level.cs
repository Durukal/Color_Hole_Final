using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour {
    #region Singleton class: Level

    public static Level Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    #endregion

    //parameters for Level update color method

    #region Level Collectibles & Obstacles

    [Space]
    [Header("Level Collectibles & Obstacles")]
    [SerializeField]
    private Material groundMaterial;

    [SerializeField]
    private Material collectibleMaterial;

    [SerializeField]
    private Material obstacleMaterial;

    [SerializeField]
    private SpriteRenderer groundBorderSprite;

    [SerializeField]
    private SpriteRenderer groundSideSprite;

    [SerializeField]
    private Image progressFillImage;

    [SerializeField]
    private SpriteRenderer bgFadeSprite;

    [Space]
    [Header("Level Colors-------")]
    [Header("Ground")]
    [SerializeField]
    private Color groundColor;

    [SerializeField]
    private Color bordersColor;

    [SerializeField]
    private Color sideColor;


    [Header("Collectiles & Obstacles")]
    [SerializeField]
    private Color collectibleColor;

    [SerializeField]
    private Color obstacleColor;

    [Header("UI (progress)")]
    [SerializeField]
    private Color progressfillColor;

    [Header("Background")]
    [SerializeField]
    private Color cameraColor;

    [SerializeField]
    private Color fadeColor;

    #endregion

    [SerializeField]
    private ParticleSystem winFx;

    [Space]
    [HideInInspector]
    public int collectiblesInScene;

    [HideInInspector]
    public int totalCollectibles;

    [SerializeField]
    private Transform collectiblesParent;


    void Start() {
        CountCollectibles();
        UpdateLevelColors();
    }


    void CountCollectibles() {
        totalCollectibles = collectiblesParent.childCount;
        collectiblesInScene = totalCollectibles;
    }

    public void PlayWinFx() {
        winFx.Play();
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateLevelColors() {
        groundMaterial.color = groundColor;
        groundSideSprite.color = sideColor;
        groundBorderSprite.color = bordersColor;
        collectibleMaterial.color = collectibleColor;
        obstacleMaterial.color = obstacleColor;
        progressFillImage.color = progressfillColor;
        Camera.main.backgroundColor = cameraColor;
        bgFadeSprite.color = fadeColor;
    }

    //UpdateLevelColors method also needs to be called when colors changed in the Inspector
    void OnValidate() {
        UpdateLevelColors();
    }
}