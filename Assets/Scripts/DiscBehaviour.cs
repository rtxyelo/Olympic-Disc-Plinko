using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _impulseSpeed = 2f;
    [SerializeField]
    private float _velosityThreshold = 0.08f;
    [SerializeField]
    private float _angularVelosityThreshold = 300f;

    private Rigidbody2D _rb;

    private bool _isGameStarted = false;

    //private bool _spacePressed = false;  // Debug

    private bool _isGamePaused = false;

    private bool _isGameBeenPaused = false;

    private Vector2 _velocity = new();

    private float _angularVelocity;

    private ArrowBehaviour _arrowBehaviour;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Static;
        _arrowBehaviour = GameObject.Find("Arrow").GetComponent<ArrowBehaviour>();
    }

    private void Update()
    {
        if (_rb.bodyType != RigidbodyType2D.Static)
        {
            if (_isGamePaused)
            {
                _rb.velocity = Vector2.zero;
                _rb.angularVelocity = 0f;
                _isGameBeenPaused = true;
            }
            else
            {
                if (_isGameBeenPaused) 
                {
                    _isGameBeenPaused = false;
                    _rb.velocity = _velocity;
                    _rb.angularVelocity = _angularVelocity;
                }
                _velocity = _rb.velocity;
                _angularVelocity = _rb.angularVelocity;
            }

            //Debug.Log("Current velocity " + _rb.velocity);
            //Debug.Log("Current angularVelocity " + _rb.angularVelocity);

            if (Mathf.Abs(_rb.angularVelocity) < _angularVelosityThreshold)
            {
                _rb.angularVelocity = 0f;
            }

            var currentVelocityMagnitude = (float)Mathf.Sqrt(_rb.velocity.x * _rb.velocity.x + _rb.velocity.y * _rb.velocity.y);

            Debug.Log("Current Velocity Magnitude " + currentVelocityMagnitude);

            if (currentVelocityMagnitude < _velosityThreshold)
            {
                _rb.velocity = new Vector2(0f, 0f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isGameStarted)
        {
            _isGameStarted = false;
            var dir = new Vector2(_arrowBehaviour.TipPosition.x, 1f).normalized;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.AddForce(dir * _impulseSpeed, ForceMode2D.Impulse);
            _rb.angularVelocity = 1500f;
        }

        // Debug
        //if (Input.GetKey(KeyCode.Space) && !_spacePressed)
        //{
        //    var dir = new Vector2(_arrowBehaviour.TipPosition.x, 1f).normalized;
        //    _rb.bodyType = RigidbodyType2D.Dynamic;
        //    _rb.AddForce(dir * _impulseSpeed, ForceMode2D.Impulse);
        //    _rb.angularVelocity = 1500f;
        //    _spacePressed = true; 
        //}
        //else if (!Input.GetKey(KeyCode.Space))
        //{
        //    _spacePressed = false; 
        //}
    }

    public void StartGame()
    {
        _isGameStarted = true;
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
