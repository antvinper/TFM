using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// TODO: Remove the allocs here, use a static memory pool?
public static class Awaiters
{
    readonly static WaitForUpdate _waitForUpdate = new WaitForUpdate();
    readonly static WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    readonly static WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

    public static WaitForUpdate NextFrame
    {
        get { return _waitForUpdate; }
    }

    public static WaitForFixedUpdate FixedUpdate
    {
        get { return _waitForFixedUpdate; }
    }

    public static WaitForEndOfFrame EndOfFrame
    {
        get { return _waitForEndOfFrame; }
    }

    public static WaitForSeconds Seconds(float seconds)
    {
        return new WaitForSeconds(seconds);
    }

    public static WaitForSecondsRealtime SecondsRealtime(float seconds)
    {
        return new WaitForSecondsRealtime(seconds);
    }

    public static WaitUntil Until(Func<bool> predicate)
    {
        return new WaitUntil(predicate);
    }

    public static WaitWhile While(Func<bool> predicate)
    {
        return new WaitWhile(predicate);
    }
    
    public static ButtonAwaiter GetAwaiter(this Button button)
    {
        return new ButtonAwaiter()
        {
            button = button
        };
    }
    public class ButtonAwaiter : INotifyCompletion
    {
        public bool IsCompleted
        {
            get { return false; }
        }

        public void GetResult()
        {

        }

        public Button button { get; set; }

        public void OnCompleted(Action continuation)
        {
            UnityAction h = null;
            h = () =>
            {
                button.onClick.RemoveListener(h);
                continuation();
            };
            button.onClick.AddListener(h);
        }
    }
}
