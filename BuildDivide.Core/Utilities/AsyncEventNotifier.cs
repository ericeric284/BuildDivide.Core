using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildDivide.Core.Games;

namespace BuildDivide.Core.Utilities
{
    public class AsyncEventNotifier<T>
    {
        private readonly List<Func<T, Task>> _subscribers = new List<Func<T, Task>>();

        public IDisposable Subscribe(Func<T, Task> subscriber)
        {
            _subscribers.Add(subscriber);
            return new Subscription<T>(this, subscriber);
        }

        public async Task RaiseEventAsync(T ev)
        {
            var tasks = new List<Task>();
            foreach (var subscriber in _subscribers)
            {
                tasks.Add(subscriber.Invoke(ev));
            }

            await Task.WhenAll(tasks);
        }

        private class Subscription<T> : IDisposable
        {
            private readonly AsyncEventNotifier<T> _notifier;
            private readonly Func<T, Task> _subscriber;

            public Subscription(AsyncEventNotifier<T> notifier, Func<T, Task> subscriber)
            {
                _notifier = notifier;
                _subscriber = subscriber;
            }

            public void Dispose()
            {
                _notifier._subscribers.Remove(_subscriber);
            }
        }
    }
}