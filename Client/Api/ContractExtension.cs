namespace Client.Api;

public static class ContractExtension {
	public static float GetTotalScore(this ProblemSubmissionLimit limit) => limit.TotalScore ?? limit.TestCaseScores!.Values.Sum();

	public static string GetDisplayingName(this User user) => user.Username ?? user.Id.ToString();

	public static string GetDisplayingName(this CommentAuthor author) => author.Username ?? author.Id.ToString();

	public static int GetReactionCount(this Comment comment, Reaction reaction)
		=> Enum.GetName(reaction) is { } key && comment.Reactions?.ContainsKey(key) == true ? comment.Reactions[key].Count : 0;

	public static double GetTotalScore(this ScoreSummary score) {
		double result = 0;
		if (score.Judgement is not null)
			result = score.Judgement.Values.Average();
		if (score.Review is not null)
			result += score.Review.Value;
		return result;
	}
}