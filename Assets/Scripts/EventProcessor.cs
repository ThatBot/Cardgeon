using System;
using System.Reflection;

public class EventProcessor
{
    private MethodInfo method;

	public void ProcessEvent(CardEventObject _event, bool _isEnemy)
    {
        method = Type.GetType("EventProcessor").GetMethod(_event.eventName);

        object[] par = new object[] { _isEnemy };

        method.Invoke(this, par);
    }

    public void Lightning(bool _isEnemy)
    {
        int rand = UnityEngine.Random.Range(0, 3);

        if(rand == 0)
        {
            if (_isEnemy)
            {
                BattleManager.instance.HurtPlayer(5);
            }
            else
            {
                BattleManager.instance.HurtEnemy(5);
            }
        }
    }
}
