using TMPro;
using UnityEngine;

public class MenuScoreView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;


    private void OnEnable()
    {
        text.text = $"Score: {ScoreStorage.GetScoreValue()}!";
    }
}
