using UnityEngine;

public class SelectManager : MonoBehaviour
{
    //public BattleController battleController;

    Shader defaultShader;
    public Shader highlightShader;

    public LayerMask selectMask;

    public static GameObject selectObject;

    private bool selected;

    void OnEnable()
    {
        defaultShader = Shader.Find("Mobile/Diffuse");
        highlightShader = Shader.Find("Outlined/Silhouetted Diffuse");
    }
    

    // Update is called once per frame
    void Update()
    {
        if (BattleController.state == BattleState.PREPLAYERBATTLEPHASE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, selectMask))
                {
                    var rig = hitInfo.collider.GetComponent<Rigidbody>();
                    if (rig != null && selected != true)
                    {
                        rig.GetComponentInChildren<Renderer>().material.shader = highlightShader;
                        selectObject = rig.gameObject;
                        selected = true;
                    }
                }
            }
        }
        else
            selected = false;
    }

    void OnDisable()
    {
        selectObject.GetComponentInChildren<Renderer>().material.shader = defaultShader;
        selectObject = null;
        selected = false;
    }
}