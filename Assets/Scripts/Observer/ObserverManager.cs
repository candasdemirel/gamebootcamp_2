
using System.Collections.Generic;

public class ObserverManager : Singleton<ObserverManager>
{
    List<ISubject> typeObserverList;
    public Dictionary<string, List<ISubject>> observerDic = new Dictionary<string, List<ISubject>>();

    public void Subscribe(ISubject subject, string key)
    {
        //observerDic.Add(key,)
    }

    public void UnSubscribe(ISubject subject)
    {

    }

    private void Postmessage(string key)
    {
        // observerDic[key].
    }
}
