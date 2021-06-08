using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Wrapping : MonoBehaviour
{
    //script for screen wraping/loop

    public bool advancedWrapping = true;
    
    private Renderer[] renderers_;

    bool isWrappingX_ = false;
    bool isWrappingY_ = false;

    Transform[] ghosts_ = new Transform [8];

    float screenWidth_;
    float screenHeight_;
    
    void Start()
    {
        renderers_ = GetComponentsInChildren<Renderer>();

        var cam = Camera.main;

        var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

        screenWidth_ = screenTopRight.x - screenBottomLeft.x;
        screenHeight_ = screenTopRight.y - screenBottomLeft.y;

        if (advancedWrapping)
        {
            CreateGhostPlayers();
        }
    }

    void Update()
    {
        if (advancedWrapping)
        {
            AdvancedScreenWrap();
        }
        else
        {
            ScreenWrap();
        }
    }

    void ScreenWrap()
    {
        foreach (var renderer in renderers_)
        {
            if (renderer.isVisible)
            {
                isWrappingX_ = false;
                isWrappingY_ = false;
                return;
            }
        }

        if (isWrappingX_ && isWrappingY_)
        {
            return;
        }

        var cam = Camera.main;
        var newPosition = transform.position;

        var viewportPosition = cam.WorldToViewportPoint(transform.position);

        if (!isWrappingX_ && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            isWrappingX_ = true;
        }
        
        if (!isWrappingY_ && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
            isWrappingY_ = true;
        }

        transform.position = newPosition;
    }

    void AdvancedScreenWrap()
    {
        var isVisible = false;
        foreach (var renderer in renderers_)
        {
            if (renderer.isVisible)
            {
                isVisible = true;
                break;
            }
        }

        if (!isVisible)
        {
            SwapPlayers();
        }
    }

    void CreateGhostPlayers()
    {
        for (int i = 0; i < 8; i++)
        {
            ghosts_[i] = Instantiate(transform, Vector3.zero, Quaternion.identity) as Transform;
            DestroyImmediate(ghosts_[i].GetComponent<Wrapping>());
        }

        PositionGhostPlayers();
    }

    void PositionGhostPlayers()
    {
        var ghostPosition = transform.position;

        ghostPosition.x = transform.position.x + screenWidth_;
        ghostPosition.y = transform.position.y;
        ghosts_[0].position = ghostPosition;
        
        ghostPosition.x = transform.position.x + screenWidth_;
        ghostPosition.y = transform.position.y - screenHeight_;
        ghosts_[1].position = ghostPosition;
        
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y - screenHeight_;
        ghosts_[2].position = ghostPosition;
        
        ghostPosition.x = transform.position.x - screenWidth_;
        ghostPosition.y = transform.position.y - screenHeight_;
        ghosts_[3].position = ghostPosition;
        
        ghostPosition.x = transform.position.x - screenWidth_;
        ghostPosition.y = transform.position.y ;
        ghosts_[4].position = ghostPosition;
        
        ghostPosition.x = transform.position.x - screenWidth_;
        ghostPosition.y = transform.position.y + screenHeight_;
        ghosts_[5].position = ghostPosition;
        
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y + screenHeight_;
        ghosts_[6].position = ghostPosition;
        
        ghostPosition.x = transform.position.x + screenWidth_;
        ghostPosition.y = transform.position.y + screenHeight_;
        ghosts_[7].position = ghostPosition;

        for (int i = 0; i < 8; i++)
        {
            ghosts_[i].rotation = transform.rotation;
        }
    }

    void SwapPlayers()
    {
        foreach (var ghost in ghosts_)
        {
            if (ghost.position.x < screenWidth_ && ghost.position.x > -screenWidth_ &&
                ghost.position.y < screenHeight_ && ghost.position.y > - screenHeight_)
            {
                transform.position = ghost.position;
                break;
            }
        }
        PositionGhostPlayers();
    }

    /*private void OnGUI()
    {
        if(GUI.Button(new Rect(20, 20, 160, 48), "Simple Wrapping"))
        {
            SwitchToSimpleWrapping();
        }
		
        if(GUI.Button(new Rect(200, 20, 160, 48), "Advanced Wrapping"))
        {
            SwitchToAdvancedWrapping();
        }
    }*/

    /*void SwitchToSimpleWrapping()
    {
        advancedWrapping = false;

        foreach (var ghost in ghosts_)
        {
            Destroy(ghost.gameObject);
        }
    }

    void SwitchToAdvancedWrapping()
    {
        advancedWrapping = true;
        CreateGhostPlayers();
    }*/
}
