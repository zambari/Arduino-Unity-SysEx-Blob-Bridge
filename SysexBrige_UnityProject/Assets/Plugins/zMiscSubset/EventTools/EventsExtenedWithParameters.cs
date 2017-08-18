using UnityEngine.Events;

[System.Serializable]
public class StringEvent : UnityEvent <string> {}
[System.Serializable]
public class IntEvent : UnityEvent <int> {}
[System.Serializable]
public class LongEvent : UnityEvent <long> {}
[System.Serializable]
public class FloatEvent : UnityEvent <float> {} 
[System.Serializable]
public class DoubleEvent : UnityEvent <double> {} 
[System.Serializable]
public class BoolEvent : UnityEvent <bool> {}
[System.Serializable]
public class ByteArrayEvent : UnityEvent <byte[]> {}

[System.Serializable]
public class VoidEvent : UnityEvent  {}

public static class EventsExtenedWithParameters
{   public static void AddOnce(this DoubleEvent thisEvent, UnityAction<double> reciever)
    {
            thisEvent.RemoveListener(reciever);
            thisEvent.AddListener(reciever);
    }
      public static void AddOnce(this VoidEvent thisEvent, UnityAction reciever)
    {
            thisEvent.RemoveListener(reciever);
            thisEvent.AddListener(reciever);
    }
    public static void AddOnce(this StringEvent thisEvent, UnityAction<string> reciever)
    {
            thisEvent.RemoveListener(reciever);
            thisEvent.AddListener(reciever);
    }
        public static void AddOnce(this IntEvent thisEvent, UnityAction<int> reciever)
    {
            thisEvent.RemoveListener(reciever);
            thisEvent.AddListener(reciever);
    }
        public static void AddOnce(this LongEvent thisEvent, UnityAction<long> reciever)
    {
            thisEvent.RemoveListener(reciever);
            thisEvent.AddListener(reciever);
    }
        public static void AddOnce(this FloatEvent thisEvent, UnityAction<float> reciever)
    {
            thisEvent.RemoveListener(reciever);
            thisEvent.AddListener(reciever);
    }
        public static void AddOnce(this BoolEvent thisEvent, UnityAction<bool> reciever)
    {
            thisEvent.RemoveListener(reciever);
            thisEvent.AddListener(reciever);
    }
      public static void AddOnce(this UnityEvent thisEvent, UnityAction reciever)
    {
            thisEvent.RemoveListener(reciever);
            thisEvent.AddListener(reciever);
    }
}