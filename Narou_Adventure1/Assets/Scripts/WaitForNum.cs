
/// <summary>
/// 決まった回数分呼ばれるまでfalse,
/// 決まった機数を越えたらtrueになるフラグWaitingを待つ。
/// ○使い方
/// 処理を待ちたいポイントでWait(10)などと呼ぶ
/// 待っているかどうかをWaitingで判定。
/// カウントのリセットはRest()で行う。
/// </summary>
public class WaitForNum {
    int duration;
    int currentNum;
    public void Wait(int duration)
    {
        this.duration = duration;
        currentNum++;
        if (Waiting == false) { currentNum = duration; }
    }
    public void Reset()
    {
        currentNum = 0;
    }

    public bool Waiting { get => currentNum < duration; }
}
