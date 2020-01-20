using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    RaycastHit hit;
    GameObject draggable;
    public Transform ground;
    int layer_mask;
    bool isMouseDragging = false;
    Vector3 mousePos;

    private void Start()
    {
        layer_mask = LayerMask.GetMask("DragTargets");

        if (Screen.width < 300) 
            ChangeScale();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics.Raycast(mousePos, ground.transform.position, out hit, 10f, layer_mask))
            {
                if (hit.collider.gameObject.tag == "Draggable")
                {
                    draggable = hit.collider.gameObject;
                    isMouseDragging = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
            isMouseDragging = false;

        if (isMouseDragging)
        {
            draggable.GetComponent<Rigidbody>().AddForce(100f * (new Vector3(mousePos.x, 0, mousePos.z) - draggable.transform.position));
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    
    public void Restart()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    
    void ChangeScale()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Draggable"))
            go.transform.localScale = new Vector3(2, 2, 2);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Metal"))
            go.transform.localScale = new Vector3(2, 2, 2);
    }
}
