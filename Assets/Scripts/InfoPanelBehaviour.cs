using UnityEngine;
using UnityEngine.UI;

public class InfoPanelBehaviour : MonoBehaviour
{
    [SerializeField]
    private ArrowBehaviour arrowBehaviour;

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private GameObject screenBlur;

    public void InfoPanelClose()
    {
        if (arrowBehaviour != null)
        {
            arrowBehaviour.ResumeGame();
            gameObject.SetActive(false);
            screenBlur.SetActive(false);
            playButton.enabled = true;
            pauseButton.enabled = true;
        }
    }
}
