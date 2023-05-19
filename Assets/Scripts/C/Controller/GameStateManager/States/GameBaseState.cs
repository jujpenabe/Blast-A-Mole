namespace BAM.C
{
	public abstract class GameBaseState
	{
		public abstract void EnterState(GameStateManager gsm);

		public abstract void UpdateState(GameStateManager gsm);
	}
}
