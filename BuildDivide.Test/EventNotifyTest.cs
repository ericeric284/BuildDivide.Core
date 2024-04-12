using BuildDivide.Core.Games;
using BuildDivide.Core.Utilities;
using Shouldly;

namespace BuildDivide.Test
{
    public class EventNotifyTest
    {

        [Fact]
        public async Task EventNotifyTestAsync()
        {
            var notifier = new AsyncEventNotifier<IGameEvent>();
            
            notifier.Subscribe(async (e) =>
            {
                e.EventType.ShouldBe(GameEventType.Shuffle);
                await Task.Delay(100);
            });

            notifier.Subscribe(async (e) =>
            {
                e.EventType.ShouldBe(GameEventType.Shuffle);
                await Task.Delay(100);
            });

            await notifier.RaiseEventAsync(new ShuffleEvent());
        }
        
        [Fact]
        public async Task DisposeTest()
        {
            var notifier = new AsyncEventNotifier<IGameEvent>();

            var d1 = notifier.Subscribe(async (e) =>
            {
                throw new Exception("This should not be called");
            });

            notifier.Subscribe(async (e) =>
            {
                e.EventType.ShouldBe(GameEventType.Shuffle);
                await Task.Delay(100);
            });

            d1.Dispose();

            await notifier.RaiseEventAsync(new ShuffleEvent());
        }
    }
}
