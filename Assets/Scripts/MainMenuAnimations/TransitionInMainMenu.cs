using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionInMainMenu : MonoBehaviour
{
    public Animator mainMenuPanel;
    public Animator settingsPanel;
    public Animator locationsPanel;
    public Animator progressPanel;

    public void closeMainMenu()
    {
        mainMenuPanel.SetTrigger("go_out");
        mainMenuPanel.SetBool("isHidden", true);
    }

    public void openMainMenu()
    {
        mainMenuPanel.SetTrigger("go_in");
        mainMenuPanel.SetBool("isHidden", false);
    }

    public void openSettings()
    {
        settingsPanel.SetTrigger("go_in");
        settingsPanel.SetBool("isHidden", false);
    }

    public void closeSettings()
    {
        settingsPanel.SetTrigger("go_out");
        settingsPanel.SetBool("isHidden", true);
    }

    public void openLocationsMenu()
    {
        locationsPanel.SetTrigger("go_in");
        locationsPanel.SetBool("isHidden", false);
    }

    public void closeLocationsMenu()
    {
        locationsPanel.SetTrigger("go_out");
        locationsPanel.SetBool("isHidden", true);
    }

    public void openProgressMenu()
    {
        progressPanel.SetTrigger("go_in");
        //progressPanel.SetBool("isHidden", false);
    }

    public void closeProgressMenu()
    {
        progressPanel.SetTrigger("go_out");
        //progressPanel.SetBool("isHidden", true);
    }
}
