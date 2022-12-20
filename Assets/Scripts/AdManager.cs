using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private bool testMode = true;

    public static AdManager Instance;

#if UNITY_ANDROID
    private string gameId = "5020151";
#elif UNITY_IOS
    private string gameId = "5020150";
#endif

    private GameOverHandler gameOverHandler;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.Initialize(gameId, testMode, this);
            
        }
    }

    
    public void LoadAd()
    {
        Advertisement.Load("rewardedVideo", this);
        Advertisement.Load("Interstitial_Android", this);
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
         bool continuePlaying = gameOverHandler.isContinue;
         bool playAgain = gameOverHandler.isPlayAgain;
         bool returnToMenu = gameOverHandler.isReturnToMenu;

         this.gameOverHandler = gameOverHandler;

         if (playAgain || returnToMenu) { Advertisement.Show("Interstitial_Android", this); }
         else if (continuePlaying)
         {
             Advertisement.Show("rewardedVideo", this);
         }
        // else if (returnToMenu) { Advertisement.Show("Interstitial_Android", this); }
         
    }


    public void OnInitializationComplete()
    {
       // Debug.Log("Ad initialized");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
       // Debug.Log("Ad loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {

    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {

    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        bool continuePlaying = gameOverHandler.isContinue;
        bool playAgain = gameOverHandler.isPlayAgain;
        bool returnToMenu = gameOverHandler.isReturnToMenu;
       
        if (playAgain)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    SceneManager.LoadScene(1);
                    break;
                case UnityAdsShowCompletionState.SKIPPED:
                    SceneManager.LoadScene(1);
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    //  Debug.LogWarning("Ad Failed");
                    SceneManager.LoadScene(1);
                    break;
            }
        } 

        else if (continuePlaying)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    gameOverHandler.ContinueGame();
                    break;
                case UnityAdsShowCompletionState.SKIPPED:
                    // Ad was skipped
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    //    Debug.LogWarning("Ad Failed");
                    break;
            }
        }
        else if (returnToMenu) { return; }
    }
}
