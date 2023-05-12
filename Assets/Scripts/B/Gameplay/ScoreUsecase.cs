using UniRx;
using BAM.A;

namespace BAM.B
{
	public class ScoreUsecase : IScoreUsecase
	{
		// Reactive properties for score
		public IReadOnlyReactiveProperty<int> Score => _score;
		private readonly ReactiveProperty<int> _score;

		public ScoreUsecase()
		{
			_score = new ReactiveProperty<int>(0);
			InitScore();
			Player player = new Player(); // Should be passed as a parameter
			_score.Value = player.Score;
		}

		public void AddScore(int score)
		{
			_score.Value += score;
		}
		private void InitScore()
		{
			_score.Value = 0;
		}
	}
}
