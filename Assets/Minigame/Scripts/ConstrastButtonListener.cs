using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstrastButtonListener : MonoBehaviour
{
    [SerializeField] private float VRadius = 0.7f;
    [SerializeField] private float VSoft = 0.5f;

    private float defaultRadius = 1f;
    private float defaultSoft = 0f;

    [SerializeField] private CameraPostProcess postprocess = null;

    private Button m_Button;

    private bool m_Pressed;

    

    private void Awake()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(delegate { ButtonClick(); });
    }

    public void ButtonClick()
    {
        if (!m_Pressed)
        {
            if (postprocess != null)
            {
                postprocess.SetMaterialProperty(VRadius, VSoft);
            }
        }
        else
        {
            if (postprocess != null)
            {
                postprocess.SetMaterialProperty(defaultRadius, defaultSoft);
            }
        }
        m_Pressed = !m_Pressed;
    }

}
