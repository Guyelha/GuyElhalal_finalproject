using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFoodScript : MonoBehaviour
{
    [SerializeField] public BoxCollider2D gridArea;
    [HideInInspector] public int yesOrNo;
    private void Awake()
    {
        RandomizedPosition();
    }
    private void RandomizedPosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }

    IEnumerator WaitForRespawn()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(7);

        RandomizedPosition();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
           int yesOrNo = Random.Range(0, 15);
            if (yesOrNo==1)
            {
                RandomizedPosition();
            }
            else
            { 
                StartCoroutine( WaitForRespawn());
            }
        }

    }
}
