using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgardeScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private GameObject range;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Turret")
                {
                    transform.GetChild(0).GetComponent<PointerScript>().target = hit.transform;
                    transform.GetChild(0).gameObject.SetActive(true);
                    range = hit.transform.GetComponent<TurretScript>().range;
                    range.SetActive(true);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    if(range) range.SetActive(false);
                }
            }
        }
    }
}
