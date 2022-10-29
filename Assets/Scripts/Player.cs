using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 _direction = Vector2.right;
    [SerializeField] public PlayerInput playerInput;
    [SerializeField] private float timeInervals1;
    [SerializeField] private float timeInervals2;
    [HideInInspector] public List<Transform> _segments;
    [SerializeField] public Transform segmentPrefab;
    [SerializeField] public GameObject UI;

    private void Awake()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        playerInput.currentActionMap.Enable();
        StartCoroutine(GetMovementInput());
    }
   /* private void Update()
    {
        StartCoroutine(GetMovementInput());
    }*/
    IEnumerator GetMovementInput()
    {
        while (true)
        {
            if (playerInput.currentActionMap.FindAction("Move").inProgress == true)
        {
            _direction = playerInput.currentActionMap.FindAction("Move").ReadValue<Vector2>();
        }
        else
        {
            StartCoroutine(Move());
        }
        yield return new WaitForSecondsRealtime(timeInervals1);
    }
    }
    IEnumerator Move()
    {
        while (true)
        {
            for (int i = _segments.Count-1; i >0; i--)
            {
                _segments[i].position = _segments[i - 1].position;
            }
            
            this.transform.position = new Vector3(
                        Mathf.Round(this.transform.position.x) + _direction.x,
                        Mathf.Round(this.transform.position.y) + _direction.y,
                        0);
            yield return new WaitForSecondsRealtime(timeInervals2);
        }
      
    }
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Food")
        {
            Grow();
            UI.GetComponent<UIManager>().score++;
        }
        else if (other.tag == "SFood")
        {
            Grow();
            UI.GetComponent<UIManager>().score+=3;
        }
        else if (other.tag == "Wall")
        {
            UI.GetComponent<UIManager>().StartGame();
            PlayerPrefs.SetInt("HighScore", UI.GetComponent<UIManager>().highScore);
        }
        else if (_segments.Count>=6)
        {
            if (other.tag=="Player")
            {
                UI.GetComponent<UIManager>().StartGame();
              PlayerPrefs.SetInt("HighScore", UI.GetComponent<UIManager>().highScore);
            }
        }

    }
   
}
