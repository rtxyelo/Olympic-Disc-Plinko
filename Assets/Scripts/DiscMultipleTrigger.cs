using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscMultipleTrigger : MonoBehaviour
{
    [SerializeField]
    private List<LayerMask> layerMasksList;

    private int multipleValue = 1;

    public int MultipleValue => multipleValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            for (int i = 0; i < layerMasksList.Count; i++)
            {
                if ((layerMasksList[i] & 1 << collision.gameObject.layer) != 0)
                {
                    //Debug.Log($"X{i}!");
                    multipleValue = i;
                    break;
                }
            }
        }
    }
}
