namespace Client.Api;

public static class ContractExtension {
	public static float GetTotalScore(this ProblemSubmissionLimit limit) => limit.TotalScore ?? limit.TestCaseScores!.Values.Sum();
}