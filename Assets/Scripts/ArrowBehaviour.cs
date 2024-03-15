using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [Tooltip("Rotation speed")]
    [SerializeField]
    private float m_rapidity = 3.2f;

    [SerializeField]
    private AudioSource arrowCornerSound;

    private Transform m_transform;

    private Transform m_tipTransform;

    private Vector3 m_tipPosition;

    public Vector2 TipPosition => (Vector2)m_tipPosition;

    private Vector3 m_index = Vector3.down;

    private bool _isGamePaused = true;
    
    private readonly string _musicVolumeKey = "MusicVolumeKey";

    private void Start()
    {
        m_transform = GetComponent<Transform>();
        m_tipTransform = gameObject.transform.GetChild(0).GetComponent<Transform>();
        arrowCornerSound.volume = PlayerPrefs.GetFloat(_musicVolumeKey, 0.2f);
    }

    private void Update()
    {
        if (m_transform != null && !_isGamePaused)
        {
            if (m_transform.localRotation.z >= 1f)
            {
                m_index = Vector3.up;
                arrowCornerSound.Play();
            }
            else if (m_transform.localRotation.z <= 0f)
            { 
                m_index = Vector3.down;
                arrowCornerSound.Play();
            }

            var rotation = Quaternion.LookRotation(new Vector3(0, 0, 180), m_index);
            m_transform.transform.localRotation = Quaternion.RotateTowards(m_transform.transform.localRotation, rotation, m_rapidity);

            m_tipPosition = m_tipTransform.position;
            //Debug.Log("m_tipPosition " + m_tipPosition.normalized);

        }
    }

    public void PauseGame()
    {
        _isGamePaused = true;
    }

    public void ResumeGame()
    {
        _isGamePaused = false;
    }
}
