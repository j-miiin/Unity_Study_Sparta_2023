public interface IState
{
    public void Enter();            // State�� ������ ��
    public void Exit();             // State���� ���� ��
    public void HandleInput();      // State �� �Է� ó��
    public void Update();
    public void PhysicsUpdate();    // ������ ������Ʈ
}