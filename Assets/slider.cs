using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    private Slider hpSlider;
    private Text hpText;
    private float hp;
    public  moveAnimCont hipSc;
    
        // Start is called before the first frame update
    void Start()
    {
        hpSlider = transform.GetChild(0).GetComponent<Slider>();
        hpText = transform.GetChild(1).GetComponent<Text>();
        hp=hipSc.hp;
        hpSlider.value = hipSc.hp / 5;
        hpText.text = hipSc.hp.ToString() + "/" + "5";
    }

    // Update is called once per frame
    void Update()
    {
        //这里要判断是否被碰撞体碰撞而且碰撞的对象是 1或者0
        //要被木头撞 而且不是主角
        //或者这里只是显示血量
        //血量控制放在hip那里
        hp = hipSc.hp;
        hpSlider.value = hp / 5;
        hpText.text = hp.ToString() + "/" + "5";
    }
}
