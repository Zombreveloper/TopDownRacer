using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelfdestruct : MonoBehaviour
{
    public ListOfActiveCars activeCars; //connect in hirachy
    public CarDestroyer destroyer;

    // Start is called before the first frame update
    void Start()
    {
        activeCars = FindObjectOfType<ListOfActiveCars>();
        destroyer = FindObjectOfType<CarDestroyer>();
    }

    // Update is called once per frame
    void Update()
    {
        //destroys the first and last positioned on the list, not the race!
        OnCommandDestroyFirst();
        OnCommandDestroyLast();

    }

    void OnCommandDestroyFirst()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Destroy(activeCars.carsList[0]);
            destroyer.callExecuteDestroy(0);
        }
    }

    void OnCommandDestroyLast()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Destroy(activeCars.carsList[0]);
            int i = activeCars.getCarsList().Count -1;
            destroyer.callExecuteDestroy(i);
        }
    }
}
