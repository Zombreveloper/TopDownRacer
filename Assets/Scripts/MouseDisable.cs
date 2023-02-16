using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDisable : MonoBehaviour
{
    private static MouseDisable instance = null;
    public static MouseDisable Instance
    {
        get { return instance; }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        // Hides the cursor
        Cursor.visible = false;
        InputSystem.DisableDevice(Mouse.current);
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}