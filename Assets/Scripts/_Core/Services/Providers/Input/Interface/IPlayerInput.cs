public interface IPlayerInput
{
  bool IsButtonPressed(Button button);
  bool IsButtonHeld(Button button);
  float GetAxisHorizontal();
  float GetVerticalAxis();
}