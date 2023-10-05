public interface IState
{
    public void Enter();            // State에 들어왔을 때
    public void Exit();             // State에서 나갈 때
    public void HandleInput();      // State 중 입력 처리
    public void Update();
    public void PhysicsUpdate();    // 물리적 업데이트
}