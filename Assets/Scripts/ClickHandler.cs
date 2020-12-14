using UnityEngine;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    [Header("Please set up links to the object.")]
    [Tooltip("Link to the text of the score counter.")]
    [SerializeField] private Text ScoreText;

    private int scorCount = default;

    private void Start()
    {
        if (ScoreText == null)
            Debug.LogError("Set a link to the (Score game object) the class Input Handler..");
    }

    public void OnClick()
    {
        scorCount++;

        ScoreText.text = $"{scorCount}$";
    }
}