using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 한글 키보드 알고리즘
/// </summary>
public static class KoreanCharMaker
{
    const int BASE_CODE = 0xAC00; //Base code 
    const int BASE_INIT = 0x1100; //초성 시작점 'ㄱ'
    const int BASE_VOWEL = 0x1161; //중성 시작점 'ㅏ'

    static string nonshiftkey_str = "ABCDFGHIJKLMNSUVXYZ";
    static string nonshiftkey_kstr = "ㅁㅠㅊㅇㄹㅎㅗㅑㅓㅏㅣㅡㅜㄴㅕㅍㅌㅛㅋ";

    static string chostring = "rRseEfaqQtTdwWczxvg";//초성에 사용하는 키 목록
    static string chostring_k = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";//한글 초성
    //static string[] jungstrs = null;//중성에 사용하는 문자열 목록
    static string[] jungstrs_k = null;//한글 중성
    //static string[] jongstrs = null;//종성에 사용하는 문자열 목록
    static string[] jongstrs_k = null;//한글 종성

    static KoreanCharMaker()
    {
        //jungstrs = new string[] { "k", "o", "i", "O", "j", "p", "u", "P", "h", "hk", "ho", "hl", "y", "n", "nj", "np", "nl", "b", "m", "ml", "l" };
        jungstrs_k = new string[] { "ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ", "ㅚ", "ㅛ", "ㅜ", "ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ" };
        //jongstrs = new string[] { string.Empty, "r", "R", "rt", "s", "sw", "sg", "e", "f", "fr", "fa", "fq", "ft", "fx", "fv", "fg", "a", "q", "qt", "t", "T", "d", "w", "c", "z", "x", "v", "g" };
        jongstrs_k = new string[] { string.Empty, "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ", "ㄺ", "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ", "ㅂ", "ㅄ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };

    }

    /// <summary>
    /// 초성 자판 문자를 입력인자로 받아서 초성 코드 반환
    /// </summary>
    public static int GetInitSoundCode(char ch)
    {
        int index = chostring_k.IndexOf(ch);
        if (index != -1) { return index; }
        return chostring.IndexOf(ch);

    }

    /// <summary>
    /// 중성 문자열을 입력인자로 받아서 중성 코드로 반환, 이중 모음으로 만들 수 있기 때문에 문자열
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int GetVowelCode(string str)
    {
        //int cnt = jungstrs.Length;
        string myStr = GetVowelDoubleConsonantCode(str);

        //for (int i = 0; i < cnt; i++)
        //{
        //    if (jungstrs[i] == myStr) { return i; }
        //}

        int cnt = jungstrs_k.Length;

        for (int i = 0; i < cnt; i++)
        {
            if (jungstrs_k[i] == myStr) { return i; }
        }

        return -1;

    }

    public static string GetVowelDoubleConsonantCode(string str)
    {
        switch (str)
        {
            case "ㅗㅏ":
                return "ㅘ";
            case "ㅗㅐ":
                return "ㅙ";
            case "ㅗㅣ":
                return "ㅚ";
            case "ㅜㅓ":
                return "ㅝ";
            case "ㅜㅔ":
                return "ㅞ";
            case "ㅜㅣ":
                return "ㅟ";
            case "ㅡㅣ":
                return "ㅢ";
            default:
                break;
        }
        return str;
    }

    /// <summary>
    /// 중성 문자열을 입력인자로 받아서 중성 코드로 반환, 이중 모음으로 만들 수 있기 때문에 문자열
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    public static int GetVowelCode(char ch)
    {
        return GetVowelCode(ch.ToString());
    }

    /// <summary>
    /// 종성 문자열 입력
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int GetFinalConsonantCode(string str)
    {
        //int cnt = jongstrs.Length;
        string myStr = GetFinalDoubleConsonantCode(str);

        //for (int i = 0; i < cnt; i++)
        //{
        //    if (jongstrs[i] == myStr) { return i; }
        //}

        int cnt = jongstrs_k.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (jongstrs_k[i] == myStr)
            { return i; }
        }

        return -1;

    }

    public static string GetFinalDoubleConsonantCode(string str)
    {
        switch (str)
        {
            case "ㄱㅅ":
                return "ㄳ";
            case "ㄴㅈ":
                return "ㄵ";
            case "ㄴㅎ":
                return "ㄶ";
            case "ㄹㄱ":
                return "ㄺ";
            case "ㄹㅁ":
                return "ㄻ";
            case "ㄹㅂ":
                return "ㄼ";
            case "ㄹㅅ":
                return "ㄽ";
            case "ㄹㅌ":
                return "ㄾ";
            case "ㄹㅍ":
                return "ㄿ";
            case "ㄹㅎ":
                return "ㅀ";
            case "ㅂㅅ":
                return "ㅄ";
            default:
                break;
        }

        return str;

    }

    /// <summary>
    /// 종성 문자열 입력
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    public static int GetFinalConsonantCode(char ch)
    {
        return GetFinalConsonantCode(ch.ToString());
    }

    /// <summary>
    /// 자음 하나로 만들어진 한글 문자를 만드는 메서드
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static char GetSingleJa(int value)
    {
        byte[] bytes = BitConverter.GetBytes((short)(BASE_INIT + value));

        return Char.Parse(Encoding.Unicode.GetString(bytes));

    }

    /// <summary>
    /// 모음 하나로 만들어진 한글 문자를 만드는 메서드
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static char GetSingleVowel(int value)
    {
        byte[] bytes = BitConverter.GetBytes((short)(BASE_VOWEL + value));
        return Char.Parse(Encoding.Unicode.GetString(bytes));
    }


    /// <summary>
    /// 초성,중성,종성으로 만들어진 한글 문자를 만드는 메서드 정의
    /// </summary>
    /// <param name="init_sound"></param>
    /// <param name="vowel"></param>
    /// <param name="final"></param>
    /// <returns></returns>
    public static char GetCompleteChar(int init_sound, int vowel, int final)
    {
        int tempFinalConsonant = 0;

        if (final >= 0)
        {
            tempFinalConsonant = final;
        }

        //int jungcnt = jungstrs.Length;
        //int jongcnt = jongstrs.Length;

        int jungcnt = jungstrs_k.Length;
        int jongcnt = jongstrs_k.Length;

        int completeChar = init_sound * jungcnt * jongcnt + vowel * jongcnt

                           + tempFinalConsonant + BASE_CODE;

        byte[] naeBytes = BitConverter.GetBytes((short)(completeChar));
        return char.Parse(Encoding.Unicode.GetString(naeBytes));

    }


    /// <summary>
    /// 쉬프트 키가 필요없는 문자를 소문자로 바꿔주는 메서드도 제공합시다.
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    public static char NonShiftKeyToLower(char ch)
    {

        //먼저 쉬프트 키가 필요없는 문자인지 확인하여 소문자로 변환하여 반환하세요.
        if (nonshiftkey_str.Contains(ch.ToString()))
        {
            return Char.ToLower(ch);
        }

        int index = nonshiftkey_kstr.IndexOf(ch);

        if (index != -1)
        {

            //반환 값은 영문 문자열의 인덱스를 반환하세요.

            return Char.ToLower(nonshiftkey_str[index]);

        }

        //그렇지 않을 때는 입력 인자로 받은 문자 그대로 전달합니다.

        return ch;

    }


    /// <summary>
    /// 이중 받침이 가능한 문자인지 확인하는 메서드를 정의합시다.
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    public static bool EnableDoubleFinalConsonant(char ch)
    {
        //종성 중에 이중 받침이 가능한 문자는 ㄱ,ㄴ,ㄹ,ㅂ입니다.
         int code = GetFinalConsonantCode(ch);
        return code == 1 || code == 4 || code == 8 || code == 17;
    }


    /// <summary>
    /// 이중 모음이 가능한지 체크
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    public static bool EnableDoubleVowel(char ch)
    {
        int code = GetVowelCode(ch);
        return EnableDoubleVowel(code);
    }

    internal static bool EnableDoubleVowel(int code)
    {
        //이중 모음이 가능한 문자는 ㅗ, ㅜ, ㅡ 입니다.
        return (code == 8) || (code == 13) || (code == 18);

    }

    /// <summary>
    /// 초성 체크
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    internal static bool IsInitSound(char ch) { return GetInitSoundCode(ch) != -1; }

    /// <summary>
    /// 중성 체크
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    internal static bool IsVowel(string str) { return GetVowelCode(str) != -1; }

    /// <summary>
    /// 유니코드 글자인지 여부
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    internal static bool IsCharKey(char ch) { return char.IsLetter(ch); }

    /// <summary>
    /// 종성 체크
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    internal static bool IsFinalConsonant(char ch) { return GetFinalConsonantCode(ch) != -1; }




}

/// <summary>
/// 한글 상태를 정의하는 열거형
/// </summary>
enum HGState
{ 
    TURN_INIT_SOUND, //초성을 기대
    TURN_VOWEL, //모음을 기대
    TURN_FINAL_CONSONANT //종성을 기대
}

/// <summary>
/// 한글 상태는 하나의 한글이 형성되는 상태를 기억하는 클래스
/// </summary>
class HangulState
{
    const char VOWEL_INIT_CHAR = char.MinValue; //초성 초기 문자
    const char FINAL_INIT_CHAR = char.MinValue; //종성 초기 문자


    /// <summary>
    /// 초성 
    /// </summary>
    int init_sound;
    /// <summary>
    /// 중성 ( 모음 )
    /// </summary>
    int vowel;
    /// <summary>
    /// 첫 모음
    /// </summary>
    char vowel_first;
    /// <summary>
    /// 이중 모음이 가능 여부
    /// </summary>
    bool vowelPossible;
    /// <summary>
    /// 종성
    /// </summary>
    int final_conso;
    /// <summary>
    /// 종성의 첫 받침
    /// </summary>
    char final_consoFirst;
    /// <summary>
    /// 종성의 마지막 받침(이중 받침일 때 의미가 있음)
    /// </summary>
    char final_consoLast;
    /// <summary>
    /// 이중 받침 가능 여부
    /// </summary>
    bool final_consoPossible;

    HGState state;
    internal HGState State
    {
        get
        {
            return state;
        }

    }

    internal HangulState()
    {
        Init();
    }

    /// <summary>
    /// 초기 상태는 초성이 오기를 기대하는 상태입니다.
    /// </summary>
    internal void Init()
    {
        state = HGState.TURN_INIT_SOUND;

        init_sound = -1;
        vowel = -1;
        final_conso = -1;
        vowelPossible = false;
        final_consoPossible = false;
        vowel_first = VOWEL_INIT_CHAR;
        final_consoFirst = FINAL_INIT_CHAR;
        final_consoLast = FINAL_INIT_CHAR;
    }

    /// <summary>
    /// 상태를 초성 상태로 전이하는 메서드를 제공합니다.
    /// </summary>
    internal void SetStateInitSound()
    {
        state = HGState.TURN_INIT_SOUND;
    }

    /// <summary>
    /// 중성 상태로 전이하는 메서드도 제공합니다.
    /// </summary>
    internal void SetStateVowel()
    {
        state = HGState.TURN_VOWEL;

        //중성과 종성에 관한 나머지 멤버도 초기값으로 설정합니다.
        vowel = -1;
        final_conso = -1;
        vowelPossible = false;
        final_consoFirst = FINAL_INIT_CHAR;
        final_consoLast = FINAL_INIT_CHAR;
    }

    /// <summary>
    /// 종성에 관한 멤버 필드를 초기값으로 설정하세요.
    /// </summary>
    internal void SetStateFinalConsonant()
    {
        state = HGState.TURN_FINAL_CONSONANT;
        final_conso = -1;
        final_consoFirst = FINAL_INIT_CHAR;
        final_consoLast = FINAL_INIT_CHAR;
    }

    /// <summary>
    /// 현재의 상태로 하나의 한글 구하는 메서드
    /// </summary>
    /// <returns></returns>
    internal char GetCompleteChar()
    {
        return KoreanCharMaker.GetCompleteChar(init_sound, vowel, final_conso);
    }

    /// <summary>
    /// 이중 모음인지 확인하는 메서드
    /// </summary>
    /// <returns></returns>
    internal bool IsDoubleVowel()
    {
        bool check = KoreanCharMaker.EnableDoubleVowel(vowel);
        return vowelPossible && (check == false) && (ExistFristFinalConsonant() == false); //여기서 vowelPossible 은 좀 어리둥절

    }
    /// <summary>
    /// 종성을 설정하였는지 확인하는 메서드를 제공하세요.
    /// </summary>
    /// <returns></returns>
    bool ExistFristFinalConsonant()
    {
        return final_consoFirst != FINAL_INIT_CHAR;
    }

    /// <summary>
    /// 종성이 존재하는지 확인하는 메서드를 제공하세요.
    /// </summary>
    /// <returns></returns>
    bool ExistLastFinalConsonant() { return final_consoLast != FINAL_INIT_CHAR; }


    /// <summary>
    /// 초성만 있는 것인지 확인하세요.
    /// </summary>
    /// <returns></returns>
    internal bool IsInitSound()
    {
        //초성만 있을 때는 현재 상태는 모음을 기대하는 상태이면서 모음이 없을 때입니다.
        return state == HGState.TURN_VOWEL && !ExistVowel();

    }


    /// <summary>
    /// 단일 모음인지 확인하는 메서드도 정의합시다.
    /// </summary>
    /// <returns></returns>
    internal bool IsSingleVowel()
    {
        //먼저 첫 모음이 vowel이 아니면 단일 모음이 아닙니다.
        if (vowel != KoreanCharMaker.GetVowelCode(vowel_first)) { return false; }

        //만약 모음이 존재하면서 모음을 기대하는 상태이거나 종성을 기대하는 상태인데 종성이 없는 상태이면 단일 모음입니다.
        return (state == HGState.TURN_VOWEL && ExistVowel()) ||
               ((state == HGState.TURN_FINAL_CONSONANT) && (!ExistFinalConsonant()));

    }

    /// <summary>
    /// 모음이 존재하는지 확인하는 메서드를 제공하세요.
    /// </summary>
    /// <returns></returns>
    internal bool ExistVowel()
    {
        //모음이 초기 값이 아니면서 -1이 아니어야 합니다.
        return (vowel != VOWEL_INIT_CHAR) && (vowel != -1);
    }

    /// <summary>
    /// 종성이 존재하는지 확인하는 메서드를 제공합시다.
    /// </summary>
    /// <returns></returns>
    internal bool ExistFinalConsonant()
    {
        //종성이 초기 값이 아니면서 -1도 아니면 종성이 존재하는 것입니다.
        return (final_conso != FINAL_INIT_CHAR) && (final_conso != -1);
    }

    /// <summary>
    /// 종성이 하나인지 확인하는 메서드를 제공합시다.
    /// </summary>
    /// <returns></returns>
    internal bool IsSingleFinalConsonant()
    {
        //현재 상태가 종성이 오기를 기대하면서 첫 종성이 존재하고 마지막 종성이 존재하지 않을 때입니다.
        return state == HGState.TURN_FINAL_CONSONANT && ExistFristFinalConsonant()
                       && (!ExistLastFinalConsonant());

    }

    /// <summary>
    /// 꽉 찼는지 확인하는 메서드도 제공하세요.
    /// </summary>
    /// <returns></returns>
    internal bool IsFull() { return ExistFristFinalConsonant() && ExistLastFinalConsonant(); }

    /// <summary>
    /// 이중 종성이 가능한지 확인하는 메서드를 제공합시다.
    /// </summary>
    /// <returns></returns>
    bool EnableDoubleFinalConsonant()
    {
        //이중 종성이 가능한 자음은 ㄱ, ㄴ, ㄹ, ㅂ 입니다.
        return final_conso == 1 || final_conso == 4 || final_conso == 8 || final_conso == 17;
    }


    /// <summary>
    /// 모음을 설정하는 메서드를 제공합시다.
    /// </summary>
    /// <param name="ch"></param>
    void SetVowel(char ch)
    {

        //KoreanMamber의 정적 메서드를 이용하여 이중 모음이 가능한지 확인합니다.
        if (KoreanCharMaker.EnableDoubleVowel(ch))
        {
           //가능하면 vowelPossible을 true로 설정하고 현재 상태를 모음을 기대하는 상태로 설정합니다.
            vowelPossible = true;
            state = HGState.TURN_VOWEL;
        }
        else
        {
           //그렇지 않으면 종성을 기대하는 상태로 전이합니다.
            state = HGState.TURN_FINAL_CONSONANT;
        }

        //그리고 초기 모음을 설정합니다.
        vowel_first = ch;

    }



    /// <summary>
    /// 초성을 문자열에 추가하는 메서드를 제공합시다.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ch"></param>
    /// <returns></returns>
    internal bool InputAtInitSound(ref string source, char ch)
    {

        //먼저 다음 상태는 중성을 기대하는 상태로 전이합니다.
          state = HGState.TURN_VOWEL;
        //KoreanCharMaker 클래스의 정적 메서드 GetInitSoundCode 호출로 초성 코드 값을 구합니다.
        init_sound = KoreanCharMaker.GetInitSoundCode(ch);

        if (init_sound >= 0)
        {
            //초성 문자가 맞으면 초성 코드 값은 0보다 크거나 같습니다.이 때 초성으로만 구성한 한글을 기존 문자열에 더합니다.
            //source += KoreanCharMaker.GetSingleJa(init_sound);
            source += ch;
            return true;
        }
        //만약 초성 코드 값이 0보다 크거나 같지 않으면 실패를 반환합니다.
        return false;
    }




    /// <summary>
    /// 첫 모음을 추가하는 메서드도 제공합시다.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ch"></param>
    /// <returns></returns>
    internal bool InputFirstVowel(ref string source, char ch)
    {

        //먼저 문자의 중성 코드 값을 구합니다.
        vowel = KoreanCharMaker.GetVowelCode(ch);

        //만약 중성 코드가 아니면 상태를 초기 상태로 전이합니다.
        if (vowel < 0) { state = HGState.TURN_INIT_SOUND; return false; }

        //초성이 없을 때는 모음 하나로 구성한 한글을 문자열에 추가하고 Init 메서드를 호출합니다.
        if (init_sound < 0)
        {
            source += KoreanCharMaker.GetSingleVowel(KoreanCharMaker.GetVowelCode(ch));
            Init();
        }
        else
        {
            //초성이 있을 때는 중성을 설정합니다.
            SetVowel(ch);
            //그리고 마지막 문자를 중성을 설정한 한글로 변경합니다.
            source = source.Substring(0, source.Length - 1);

            source += GetCompleteChar();

        }
        return true;
    }


    /// <summary>
    /// 두 번째 모음을 추가하는 메서드를 제공합시다.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ch"></param>
    /// <returns></returns>
    internal bool InputSecondVowel(ref string source, char ch)
    {
        //현재 첫번째 모음 문자와 입력 인자로 받은 문자로 구성한 문자열을 만듭니다.
        string tempvowel = string.Empty;

        tempvowel += vowel_first;

        tempvowel += ch;


        //구성한 문자열로 종성 코드 값을 구합니다.
        int temp = KoreanCharMaker.GetVowelCode(tempvowel);

        if (temp >= 0)   //tempvowel이 모음
        {
            //종성 코드 값이 0보다 크거나 같으면 유효한 모음입니다.이중 모음을 구성한 문자열을 모음으로 설정합니다.
            vowel = temp;
            //원본 문자열의 맨 마지막 문자를 새로 형성한 문자열로 변경합니다.
            source = source.Substring(0, source.Length - 1);
            source += GetCompleteChar();

            //종성이 오기를 기대하는 상태로 전이합니다.
            SetStateFinalConsonant();

            return true;
        }
        else
        {
            //그렇지 않으면 첫 종성을 기대하는 상태로 전이합니다.
            SetStateFinalConsonant();

            //그리고 다시 입력할 수 있게 false를 반환합니다.

            return false;

        }

    }




    /// <summary>
    /// 첫 종성을 추가하는 메서드를 구현합시다.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ch"></param>
    /// <returns></returns>
    internal bool InputFirstFinalConsonant(ref string source, char ch)
    {
        //먼저 입력인자로 받은 문자를 종성 코드로 변환합니다.
        final_conso = KoreanCharMaker.GetFinalConsonantCode(ch);

        if (final_conso > 0)
        {
            //종성 코드가 유효하면 원본 문자열의 마지막 문자를 종성을 추가한 완성형 문자로 변경합니다.
            source = source.Substring(0, source.Length - 1);
            source += GetCompleteChar();

            //첫 종성 문자를 설정합니다.
            SetFirstFinalConsonant(ch); return true;
        }

        //입력 인자로 받은 문자가 모음일 때는 초기화를 하고 false를 반환하여 다시 입력 시도할 수 있게 합니다.
        if (KoreanCharMaker.GetVowelCode(ch) >= 0) { Init(); return false; }

        //종성으로 쓸 수 없는 자음이 왔을 때도 초기화를 합니다.
        if (KoreanCharMaker.GetInitSoundCode(ch) >= 0)
        {
            Init();
            final_consoPossible = false; final_conso = 0; return false;
        }

        return false;
    }




    /// <summary>
    /// 첫 종성을 설정하는 메서드를 구현합시다.
    /// </summary>
    /// <param name="ch"></param>
    void SetFirstFinalConsonant(char ch)
    {
        //입력 인자로 받은 문자를 첫 종성 문자로 설정합니다.
        final_consoFirst = ch;
        //이중 받침이 가능한지 확인합니다.
         final_consoPossible = EnableDoubleFinalConsonant();
    }

    /// <summary>
    /// 두번째 종성을 추가하는 메서드를 제공합시다.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ch"></param>
    /// <returns></returns>
    internal bool InputSecondFinalConsonant(ref string source, char ch)
    {
        //이중 받침이 가능하면 이중 모음을 설정하는 메서드를 호출합니다.
        if (final_consoPossible) { return SetSecondFinalConsonant(ref source, ch); }
        else
        {

            //그렇지 않으면 입력 인자로 받은 문자가 초성인지 확인하여 초성이면 초기 상태로 전이합니다.
            if (KoreanCharMaker.GetInitSoundCode(ch) >= 0) { Init(); }

           // 그렇지 않으면 마지막 문자의 종성을 새로운 초성으로 설정합니다.
            else { LastFinalConsonantToInitSound(ref source); }
        }
        return false;
    }




    /// <summary>
    /// 두번째 받침을 설정하는 메서드를 구현합시다.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ch"></param>
    /// <returns></returns>
    private bool SetSecondFinalConsonant(ref string source, char ch)
    {
        //더 이상 종성은 받을 수 없습니다. final_consoPossible을 false로 지정합니다.
        final_consoPossible = false;

        //첫 받침과 입력 인자로 받은 문자로 임시 문자열을 구성합니다.
        string tempfinal_conso = string.Empty;
        tempfinal_conso += final_consoFirst;
        tempfinal_conso += ch;

        //KoreanCharMaker의 정적 메서드 GetFinalConsonantCode로 종성 코드를 구합니다.
        int temp = KoreanCharMaker.GetFinalConsonantCode(tempfinal_conso);

        if (temp > 0)
        {
            //종성 코드가 0보다 크면 이중 받침입니다. 최종 종성을 입력 받은 문자로 설정하고 종성을 임시로 구성한 문자열로 설정합니다.
            final_consoLast = ch;
            final_conso = temp;

            //원본 문자열의 마지막 문자열 완성한 문자로 변경합니다.
            source = source.Substring(0, source.Length - 1);
            source += GetCompleteChar();

            return true;
        }

        return false;
    }

    /// <summary>
    /// 마지막 종성을 초기 초성으로 변환하는 메서드를 구현합시다.
    /// </summary>
    /// <param name="source"></param>
    private void LastFinalConsonantToInitSound(ref string source)
    {
        if (IsFull())
        {
            //이중 받침으로 구성한 문자일 때는 마지막 문자를 마지막 받침만 없는 문자로 변경합니다.
            source = source.Substring(0, source.Length - 1);
            final_conso = KoreanCharMaker.GetFinalConsonantCode(final_consoFirst);
            source += GetCompleteChar();

            //그리고 마지막 받침으로 초성 코드를 구합니다.
            init_sound = KoreanCharMaker.GetInitSoundCode(final_consoLast);

        }
        else
        {

            //그렇지 않다면 종성 문자를 얻어와서 원본 문자열의 마지막 문자에서 종성을 제거합니다.
            char tempinit_sound = final_consoFirst;

            source = source.Substring(0, source.Length - 1);
            ClearFinalConsonant();
            source += GetCompleteChar();

            //그리고 종성으로 초성 코드를 구합니다.
            init_sound = KoreanCharMaker.GetInitSoundCode(tempinit_sound);

        }

        //받침으로 만든 초성 코드로 초성 글자를 만들어 원본 문자열에 추가하고 상태를 전이합니다.
        source += KoreanCharMaker.GetSingleJa(init_sound);

        SetStateVowel();
    }


    /// <summary>
    /// 종성을 지우는 메서드를 제공합시다.
    /// </summary>
    internal void ClearFinalConsonant()
    {

        //상태를 종성을 기대하는 상태로 전이하고 모음을 설정합니다.
        SetStateFinalConsonant();
        SetVowel(vowel_first);

    }


    /// <summary>
    /// 마지막 모음을 지우는 메서드를 구현합시다.
    /// </summary>
    internal void ClearLastVowel()
    {

        //상태를 모음을 기대하는 상태로 전이합니다.
        state = HGState.TURN_VOWEL;

        //모음 코드를 구하여 설정합니다.
        vowel = KoreanCharMaker.GetVowelCode(vowel_first);

    }

    /// <summary>
    /// 마지막 종성을 지우는 메서드를 구현합시다.
    /// </summary>
    internal void ClearLastFinalConsonant()
    {
        //마지막 종성을 기대하는 상태로 전이한니다.
        state = HGState.TURN_FINAL_CONSONANT;
        final_consoLast = FINAL_INIT_CHAR;
        final_conso = KoreanCharMaker.GetFinalConsonantCode(final_consoFirst);
        final_consoPossible = true;
    }


    /// <summary>
    /// 초성으로 한 글자를 구하는 메서드를 제공합시다.
    /// </summary>
    /// <returns></returns>
    internal char GetJaFormInitSound()
    {
        //KoreanCharMaker 클래스의 정적 메서드 GetSingleja로 글자를 만들어 반환합니다.
        return KoreanCharMaker.GetSingleJa(init_sound);
    }


    /// <summary>
    /// 첫 종성이 있는지 확인하는 메서드도 제공하세요.
    /// </summary>
    /// <returns></returns>
    internal bool IsFirstFinalConsonant() { return final_conso > 0; }
}


/// <summary>
/// 한글 오토마타
/// </summary>
public class Hangul
{
    // 멤버 필드로 한글 입력 상태와 현재까지 입력한 내용을 기억할 멤버 선언
    HangulState hs;
    string source;

    /// <summary>
    /// 한글 오토마타로 만들어진 문자열
    /// </summary>
    public string Text
    {
        get { return source; }
        set { source = value; }
    }

    /// <summary>
    /// 한글 오토마타 생성기
    /// </summary>
    public Hangul()
    {
        hs = new HangulState();
        Text = string.Empty;
    }

    /// <summary>
    /// 한글 오토마타 초기화
    /// </summary>
    public void Reset() { hs.Init(); }

    /// <summary>
    /// 새로운 문자를 추가하는 메서드
    /// </summary>       
    /// <param name="ch">추가할 문자</param>

    public void Input(char ch)
    {
        if (ch == '\b')
        {
            //되돌리기
            BackKeyProc();
            return;
        }
        if (!Char.IsLetter(ch)) //알파벳이 아니면 원본 문자열에 더하기
        {
            source += ch;
            hs.Init();
            return;
        }
        if(32 <= ch && ch <= 126)
        {
            source += ch;
            hs.Init();
            return;
        }

        //ch = KoreanCharMaker.NonShiftKeyToLower(ch);

        switch (hs.State)
        {
            case HGState.TURN_INIT_SOUND: InputInitSoundProc(ch); break;
            case HGState.TURN_VOWEL: InputVowelProc(ch); break;
            case HGState.TURN_FINAL_CONSONANT: InputFinalConsonantProc(ch); break;
        }
    }

    /// <summary>
    /// 한글이 아닌 문자를 소스에 추가
    /// </summary>
    /// <param name="ch">추가할 문자</param>
    public void InputNoKorea(char ch)
    {
        Reset();
        source += ch.ToString();
    }

    /// <summary>
    /// 초성을 추가하는 메서드
    /// </summary>
    /// <param name="ch"></param>
    private void InputInitSoundProc(char ch)
    {
        if (!hs.InputAtInitSound(ref source, ch)) { Input(ch); }
    }

    /// <summary>
    /// 중성을 추가하는 메서드 
    /// </summary>
    /// <param name="ch"></param>
    private void InputVowelProc(char ch)
    {
        //중성이 있는지 확인
        if (hs.ExistVowel() == false)
        {
            if (hs.InputFirstVowel(ref source, ch) == false) { Input(ch); }
        }
        // 없으면 첫번째 모음을 추가하는 메서드
        else
        {
            //만약 이미 모음이 있다면 두번째 모음을 추가하는 메서드
            if (hs.InputSecondVowel(ref source, ch) == false)
            {
                Input(ch);
            }
        }
    }

    //종성을 추가하는 메서드
    private void InputFinalConsonantProc(char ch)
    {
        //첫번째 종성이 있는지 확인
        if (hs.IsFirstFinalConsonant() == false)
        {
            //없다면 첫번째 종성 추가
            if (hs.InputFirstFinalConsonant(ref source, ch) == false)
            {
                Input(ch);
            }

        }

        else
        {
            //첫번째 종성이 있으면 두번째 종성 추가하는 메소드 
            if (hs.InputSecondFinalConsonant(ref source, ch) == false)
            {
                Input(ch);
            }
        }
    }

    /// <summary>
    /// 지웠을때
    /// </summary>
    public void BackKeyProc()
    {
        if (source.Length <= 0) { return; }

        if ((hs.State == HGState.TURN_INIT_SOUND) || hs.IsInitSound())
        {
            hs.SetStateInitSound();
            source = source.Substring(0, source.Length - 1);
            return;
        }

        if (hs.IsSingleVowel())
        {
            hs.SetStateVowel();
            source = source.Substring(0, source.Length - 1);
            source += hs.GetJaFormInitSound();
            return;
        }

        if (hs.IsDoubleVowel())
        {
            source = source.Substring(0, source.Length - 1);
            hs.ClearLastVowel();
            source += hs.GetCompleteChar();
            return;
        }

        if (hs.IsSingleFinalConsonant())
        {
            hs.ClearFinalConsonant();
            source = source.Substring(0, source.Length - 1);
            source += hs.GetCompleteChar();
            return;
        }

        else if (hs.IsFull())
        {
            hs.ClearLastFinalConsonant();
            source = source.Substring(0, source.Length - 1);
            source += hs.GetCompleteChar();
        }

    }
}



