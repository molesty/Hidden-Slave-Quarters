using UnityEngine;
using UnityEngine.UI;

public class UIRescue : MonoBehaviour
{
    public static UIRescue Instance { get; private set; }

    [SerializeField] Button rescueButton;

    Cage current;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        if (rescueButton != null)
            rescueButton.gameObject.SetActive(false);

        if (rescueButton != null)
        {
            rescueButton.onClick.RemoveAllListeners();
            rescueButton.onClick.AddListener(OnRescueClicked);
        }
    }

    public void ShowFor(Cage cage)
    {
        if (cage == null || cage.IsRescued) { Hide(cage); return; }
        current = cage;
        if (rescueButton != null)
            rescueButton.gameObject.SetActive(true);
    }

    public void Hide(Cage cage = null)
    {
        if (cage != null && current != cage) return; 
        if (rescueButton != null)
            rescueButton.gameObject.SetActive(false);
        current = null;
    }

    void OnRescueClicked()
    {
        if (current != null)
        {
            current.Rescue();
            Hide(current);
        }
    }
}
