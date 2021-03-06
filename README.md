# LogsAnalyzer
Framework + application for automated log files analysis

LogsAnalyzer is the realization of my vision to have a pluggable log-processing framework that can work on various log entry layouts while providing application-specific logic on analyzing those log entries. Log-related applications usually concern themselves solely on viewing/filtering logs while leaving the analysis part to human intervention. By codifying analysis logic to pluggable components, troubleshooting problems and discovering patterns can be automated.

Remember that production issue that you debugged last week by poring through the log files? You probably found exception log entries that you had to correlate with preceding log entries in order to get to the root cause. Well, the issue seems to have reared its ugly head again this morning, so you have to repeat the troubleshooting process all over. Sure, it may not be a big deal since your memory is still fresh but what if the similar issue showed up months from now? And how many issues can you keep a tab on over time? Wouldn't it be nice if you can write and plug analysis components as you discover issues and their patterns so that the next time around, you can just click a button to troubleshoot? If your answer is yes, then LogsAnalyzer is for you.

## Two ways to leverage this repository
1. Use the Logs Analyzer client (Windows Forms) as-is. In this case, the application is no different from basic log-viewing applications where you can load the log file(s) while filtering the entries using Regular Expressions. While this is the path of least resistance, its main use is for initial investigation and discovery of new issues that have been recorded in your application logs. Once the pattern had been established, it is recommended that you write your own LogsAnalyzer components and plug them into the framework.
2. Use the Logs Analyzer client and create-and-plug-in your own LogsAnalyzer components. This is the recommended usage of this repo as it maximizes the framework's capabilities while providing the most insightful analysis of log entries.

Get started and learn more by reading the [wiki](https://github.com/Jabestrada/LogsAnalyzer/wiki)
