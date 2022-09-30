using UnityEngine;

public class DevKeys : MonoBehaviour
{ 
    public bool IsCollisionDisabled {get; private set;} = false;

    void Update()
    {
        //Load next level
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     GetComponent<CollisionHandler>().StartNextLevel();
        //     Debug.Log("Collision enabled");
        // }

        //Reload level
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<CollisionHandler>().ReloadLevel();
            Debug.Log("Collision enabled");
        }

        //Disable collision
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     IsCollisionDisabled = IsCollisionDisabled == false ? true : false;

        //     if (IsCollisionDisabled)
        //         Debug.Log("Collision disabled");
        //     else
        //         Debug.Log("Collision enabled");
        // }
    }
}
