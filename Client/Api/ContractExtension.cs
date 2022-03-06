namespace Client.Api;

public static class ContractExtension {
	public static float GetTotalScore(this ProblemSubmissionLimit limit) => limit.TotalScore ?? limit.TestCaseScores!.Values.Sum();

	public static string GetDisplayingName(this User user) => user.Username ?? user.Id.ToString();

	public static string GetDisplayingName(this Author author) => author.Username ?? author.Id.ToString();

	public static int GetReactionCount(this Comment comment, Reaction reaction)
		=> Enum.GetName(reaction) is { } key && comment.Reactions?.ContainsKey(key) == true ? comment.Reactions[key].Count : 0;
}