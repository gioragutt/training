namespace ModuloFramework.InputSystem
{
    public interface IUsesKeyboardInput
    {
        IKeyboardInputEngine KeyboardInputEngine { get; }
        void InitializeKeyboardInputEngine(IKeyboardInputEngine keyboardInputEngine);
    }
}
