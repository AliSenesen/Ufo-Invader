using UnityEngine.Events;

namespace _0_Scripts.Events
{
    public static class GameEvents
    {
        public static UnityEvent onGameOpen = new UnityEvent();
        public static UnityEvent onWin = new UnityEvent();
        public static UnityEvent onFail = new UnityEvent();
        public static UnityEvent onCoinAdded = new UnityEvent();
        public static UnityEvent onRadarClosed = new UnityEvent();
        public static UnityEvent onLevelChange = new UnityEvent();
    }
}