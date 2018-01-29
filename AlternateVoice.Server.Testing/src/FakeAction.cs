using System;

namespace AlternateVoice.Server.Testing
{
    public class FakeAction<T>
    {
        
        public int InvokeAmount { get; private set; }
        public Action<T> Action { get; }

        public FakeAction()
        {
            Action = e => InvokeAmount++;
        }
        
    }
}
