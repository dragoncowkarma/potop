using System;
using System.Collections.Generic;

namespace Potop.Client.Core.Events {
    /// <summary>
    /// 게임 내 이벤트를 관리하고 발행/구독을 처리하는 정적 브로커 클래스입니다.
    /// </summary>
    public static class EventBroker {
        private static readonly Dictionary<Type, Delegate> _subscribers = new Dictionary<Type, Delegate>();

        /// <summary>
        /// 모든 구독을 해제합니다. (테스트 용도)
        /// </summary>
        public static void ClearAllSubscriptions() {
            _subscribers.Clear();
        }

        /// <summary>
        /// 특정 이벤트 타입에 대한 구독을 추가합니다.
        /// </summary>
        /// <typeparam name="T">구독할 이벤트 타입</typeparam>
        /// <param name="action">이벤트 발생 시 호출될 콜백</param>
        public static void Subscribe<T>(Action<T> action) {
            Type eventType = typeof(T);
            if (_subscribers.TryGetValue(eventType, out Delegate currentDel)) {
                _subscribers[eventType] = Delegate.Combine(currentDel, action);
            } else {
                _subscribers[eventType] = action;
            }
        }

        /// <summary>
        /// 특정 이벤트 타입에 대한 구독을 취소합니다.
        /// </summary>
        /// <typeparam name="T">구독을 취소할 이벤트 타입</typeparam>
        /// <param name="action">취소할 콜백</param>
        public static void Unsubscribe<T>(Action<T> action) {
            Type eventType = typeof(T);
            if (_subscribers.TryGetValue(eventType, out Delegate currentDel)) {
                Delegate newDel = Delegate.Remove(currentDel, action);
                if (newDel == null) {
                    _subscribers.Remove(eventType);
                } else {
                    _subscribers[eventType] = newDel;
                }
            }
        }

        /// <summary>
        /// 이벤트를 발행하여 구독 중인 모든 콜백을 호출합니다.
        /// </summary>
        /// <typeparam name="T">발행할 이벤트 타입</typeparam>
        /// <param name="eventData">전달할 이벤트 데이터</param>
        public static void Publish<T>(T eventData) {
            Type eventType = typeof(T);
            if (_subscribers.TryGetValue(eventType, out Delegate currentDel)) {
                if (currentDel is Action<T> action) {
                    action.Invoke(eventData);
                }
            }
        }
    }
}
