namespace ILNumerics.Core.Global {
    public interface ICacheable<T> {

        ref T Previous { get; }

    }
}