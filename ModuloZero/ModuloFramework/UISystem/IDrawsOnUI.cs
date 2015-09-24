namespace ModuloFramework.UISystem
{
    public interface IDrawsOnUI
    {
        IDrawingEngine DrawingEngine { get; }
        void InitializeDrawingEngine(IDrawingEngine drawingEngine);
    }
}
