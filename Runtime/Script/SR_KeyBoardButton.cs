using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 키보드 버튼 하나하나에 붙어 있는 스크립트
/// </summary>
public class SR_KeyBoardButton : MonoBehaviour
{
    /// <summary>
    /// 키보드 버튼 타입
    /// </summary>
    public KEYBUTTONTYPE keyType = KEYBUTTONTYPE.Charater;
    /// <summary>
    /// 키보드 상태 별 텍스트 
    /// </summary>
    private Text[] stateText = null;

    /// <summary>
    /// 버튼 초기화
    /// </summary>
    /// <param name="stateCount">키보드 상태 갯수</param>
    public void Init(int stateCount)
    {
        Button keyButton = GetComponent<Button>();
        stateText = new Text[stateCount];

        for (int i = 0; i < stateCount; i++)
        {
            stateText[i] = transform.GetChild(i).GetComponent<Text>();
        }

        keyButton.onClick.AddListener(OnButtonClickEvent);
    }

    /// <summary>
    /// 버튼 클릭 이벤트
    /// </summary>
    private void OnButtonClickEvent()
    {
        //현재 상태에 따른 Text 
        string curText = stateText[(int)SR_KeyBoard.Instance.CurState].text;

        //키 타입마다 다른 함수 호출
        switch (keyType)
        {
            case KEYBUTTONTYPE.Backspace:
                InputKeyTypeBackspace();
                break;
            case KEYBUTTONTYPE.Charater:
                InputKeyTypeCharater(curText);
                break;
            case KEYBUTTONTYPE.Space:
                InputKeyTypeSpace();
                break;
            case KEYBUTTONTYPE.Fuction:
                InputKeyTypeFuction(curText);
                break;
            default:
                break;
        }

        //현재 InputField 에 새로 그려준다. 
        SR_KeyBoard.Instance.curInputfield.ActivateInputField();
    }

    /// <summary>
    /// 지우기
    /// </summary>
    private void InputKeyTypeBackspace()
    {
        //지우고
        SR_KeyBoard.Instance.myHangulKeyBoard.BackKeyProc();

        //텍스트 넣어주기
        if (SR_KeyBoard.Instance.curInputfield != null)
            SR_KeyBoard.Instance.curInputfield.SetText(SR_KeyBoard.Instance.myHangulKeyBoard.Text);
    }

    /// <summary>
    /// 일반 단어
    /// </summary>
    /// <param name="text"></param>
    private void InputKeyTypeCharater(string text)
    {
        SR_KeyBoard.Instance.myHangulKeyBoard.Input(text.ToCharArray()[0]);

        if(SR_KeyBoard.Instance.curInputfield!=null)
            SR_KeyBoard.Instance.curInputfield.SetText(SR_KeyBoard.Instance.myHangulKeyBoard.Text);

    }

    /// <summary>
    /// 스페이스
    /// </summary>
    private void InputKeyTypeSpace()
    {
        SR_KeyBoard.Instance.myHangulKeyBoard.Input(' ');

        if (SR_KeyBoard.Instance.curInputfield != null)
            SR_KeyBoard.Instance.curInputfield.SetText(SR_KeyBoard.Instance.myHangulKeyBoard.Text);
    }

    /// <summary>
    /// 함수
    /// </summary>
    /// <param name="text"></param>
    private void InputKeyTypeFuction(string text)
    {
        switch (text)
        {
            case "한/영":
                SR_KeyBoard.Instance.altGr = !SR_KeyBoard.Instance.altGr;
                SR_KeyBoard.Instance.CallAllButtonFunc();
                SR_KeyBoard.Instance.myHangulKeyBoard.Reset();
                break;
            case "Shift":
                SR_KeyBoard.Instance.shift = !SR_KeyBoard.Instance.shift;
                SR_KeyBoard.Instance.CallAllButtonFunc();
                break;
            case "Enter":
                SR_KeyBoard.Instance.myHangulKeyBoard.Input('\n');

                if (SR_KeyBoard.Instance.curInputfield != null)
                    SR_KeyBoard.Instance.curInputfield.SetText(SR_KeyBoard.Instance.myHangulKeyBoard.Text);

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 키 텍스트 바꿔주는 함수
    /// </summary>
    /// <param name="stateCount">키보드 상태 갯수</param>
    /// <param name="boardType">바뀔 현재 타입</param>
    public void ChangeKeyText(int stateCount, KEYBOARDSTATE boardType)
    {
        for (int i = 0; i < stateCount; i++)
        {
            stateText[i].gameObject.SetActive(false);
        }

        stateText[(int)boardType].gameObject.SetActive(true);

    }
    

}
