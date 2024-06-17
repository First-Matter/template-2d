public interface IPlayerInput
{
  bool IsJumpButtonPressed();
  bool IsJumpButtonHeld();
  float GetAxisHorizontal();
  float GetVerticalAxis();
  bool IsFireButtonPressed();
  bool IsFireButtonHeld();
}