using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum KEYBOARDSTATE
{
    Normal,
    Shift,
    AltGr,
    ShiftAltGr
}

public enum KEYBUTTONTYPE
{
    Backspace,
    Charater,
    Space,
    Fuction
}

/// <summary>
/// 키보드 클래스
/// </summary>
public class SR_KeyBoard : MonoBehaviour
{
    /// <summary>
    /// 키보드 버튼 하나하나 
    /// </summary>
    [SerializeField] private SR_KeyBoardButton[] keyBoardButtons = null;

    /// <summary>
    /// 현재 InputField
    /// </summary>
    public SR_InputField curInputfield = null;
    /// <summary>
    /// 한글 알고리즘
    /// </summary>
    public Hangul myHangulKeyBoard;

    /// <summary>
    /// shift 클릭 상태인가
    /// </summary>
    public bool shift = false;
    /// <summary>
    /// 한/영 클릭 상태인가
    /// </summary>
    public bool altGr = false;



    #region Singleton
    private static SR_KeyBoard _instance;
    public static SR_KeyBoard Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SR_KeyBoard>();

                if (_instance == null)
                    Debug.LogError("SZ_Keyboard를 찾을 수 없습니다. ");
            }
            return _instance;
        }
    }
    #endregion
    
    /// <summary>
    /// 키보드 현재 상태 반환
    /// </summary>
    public KEYBOARDSTATE CurState
    {
        get
        {
            if (shift && altGr)
            {
                //영어+대문자 상태
                return KEYBOARDSTATE.ShiftAltGr;
            }
            else if (shift && !altGr)
            {
                //한국어+Shift상태
                return KEYBOARDSTATE.Shift;
            }
            else if (!shift && altGr)
            {
                //영어+소문자 상태
                return KEYBOARDSTATE.AltGr;
            }
            else
            {
                //한국어
                return KEYBOARDSTATE.Normal;
            }
        }
    }

    /// <summary>
    /// 시작, 초기화
    /// </summary>
    void Start()
    {
        int enumCount = System.Enum.GetValues(typeof(KEYBOARDSTATE)).Length;
        for (int i = 0; i < keyBoardButtons.Length; i++)
        {
            keyBoardButtons[i].Init(enumCount);
        }

        shift = false;
        altGr = false;

        myHangulKeyBoard = new Hangul();
    }

    /// <summary>
    /// 모든 버튼 호출, 전체 키보드 Text 바꿀때 사용
    /// </summary>
    public void CallAllButtonFunc()
    {
        int enumCount = System.Enum.GetValues(typeof(KEYBOARDSTATE)).Length;

        for (int i = 0; i < keyBoardButtons.Length; i++)
        {
            keyBoardButtons[i].ChangeKeyText(enumCount,CurState);
        }
    }


}
