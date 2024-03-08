using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowBehaviour : MonoBehaviour
{
    [Tooltip("Rotation speed")]
    [SerializeField]
    private float m_rapidity = 3.2f;

    private RectTransform m_rectTransform;

    private RectTransform m_tipTransform;

    private Vector3 m_tipPosition;

    public Vector2 TipPosition => (Vector2)m_tipPosition;

    private Vector3 m_index = Vector3.down;

    private bool _isGamePaused = false;

    private void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_tipTransform = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (m_rectTransform != null && !_isGamePaused)
        {
            if (m_rectTransform.rotation.z >= 1f)
                m_index = Vector3.up;
            else if (m_rectTransform.rotation.z <= 0f)
                m_index = Vector3.down;

            var rotation = Quaternion.LookRotation(new Vector3(0, 0, 180), m_index);
            m_rectTransform.transform.rotation = Quaternion.RotateTowards(m_rectTransform.transform.rotation, rotation, m_rapidity);

            m_tipPosition = m_tipTransform.position;
            //Debug.Log("m_tipPosition " + m_tipPosition);

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
