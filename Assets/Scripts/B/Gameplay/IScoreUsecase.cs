using UniRx;

namespace BAM.B
{
	public interface IScoreUsecase
	{
		// Reactive properties for score and lives
		IReadOnlyReactiveProperty<int> Score { get; }
		// Method to add player score
		void AddScore(int score);
	}
}