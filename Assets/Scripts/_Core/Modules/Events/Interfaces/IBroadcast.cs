public interface IBroadcast<T>
{
  void Invoke(T value);
}