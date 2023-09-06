using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, INpc
{
    public string Name { get; }
    public string Dialog { get; }

    private GameObject canvas;

    public Npc(string name, string dialog)
    {
        Name = name;
        Dialog = dialog;
    }

    public void InteractWithPlayer()
    {
        UIManager.U.SetNpcDialogPanel(Name, Dialog);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canvas = GameObject.FindWithTag("Canvas");

            if (canvas == null) return;
            
            InteractWithPlayer();
            canvas.transform.Find("CallNpcPanel").gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvas = GameObject.FindWithTag("Canvas");

        if (canvas == null) return;

        canvas.transform.Find("CallNpcPanel").gameObject.SetActive(false);
        canvas.transform.Find("NpcInteractionPanel").gameObject.SetActive(false);
    }
}
