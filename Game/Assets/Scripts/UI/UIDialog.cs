// Date   : 14.10.2018 10:58
// Project: Game2
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDialog : MonoBehaviour {

    [SerializeField]
    private Text txtContent;

    [SerializeField]
    private Text txtTitle;

    [SerializeField]
    private Animator animator;

    private string defaultMenuString = string.Format(
        "{0}\n{1}\n{2}",
        "Press <color=green>R</color> to restart level.",
        "Press <color=green>Q</color> to quit(desktop).",
        "Press <color=green>Esc</color> or <color=green>O</color> to return to game."
    );

    private string defaultMenuTitle = "Game paused";

    public void Show(string message, string title)
    {
        animator.SetTrigger("Show");
        txtContent.text = message;
        txtTitle.text = title;
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        txtContent.text = defaultMenuString;
        txtTitle.text = defaultMenuTitle;
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

}
