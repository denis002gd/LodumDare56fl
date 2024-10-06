using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI LarvaNum;
    static public int larvaCount;
    // Start is called before the first frame update
    void Start()
    {
        LarvaNum.text = "Larvas: 0";
    }

    // Update is called once per frame
    void Update()
    {
        LarvaNum.text = "Larvas: " + larvaCount.ToString();
    }
    
}
