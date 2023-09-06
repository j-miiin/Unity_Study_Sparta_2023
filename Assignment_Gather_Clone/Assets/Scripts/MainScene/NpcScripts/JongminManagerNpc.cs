using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JongminManagerNpc : Npc
{
    static string name = "박종민 매니저";
    static string dialog = "안녕하세요! 무슨 일로 찾아오셨나요?";

    public JongminManagerNpc() : base(name, dialog) { } 
}
