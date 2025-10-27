using Serilog;
using System.IO;
using Microsoft.Extensions.Logging;

namespace net.vieapps.Components.Utility
{
	public static class SerilogExtensions
	{
		static Serilog.Core.Logger CreateLogger(string logPath, string filename)
			=> new LoggerConfiguration().WriteTo.File(path: Path.Combine(logPath, $"{filename.ToLower()}-.txt"), rollingInterval: RollingInterval.Hour).CreateLogger();

		/// <summary>
		/// Adds a log file (rolling interval: hour).
		/// </summary>
		/// <param name="loggerFactory"></param>
		/// <param name="logPath"></param>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static ILoggerFactory AddFile(this ILoggerFactory loggerFactory, string logPath, string filename)
		{
			logPath = logPath ?? UtilityService.GetAppSetting("Path:Logs");
			if (!string.IsNullOrWhiteSpace(logPath) && Directory.Exists(logPath))
				loggerFactory.AddSerilog(CreateLogger(logPath, filename));
			return loggerFactory;
		}

		/// <summary>
		/// Adds a log file (rolling interval: hour).
		/// </summary>
		/// <param name="loggingBuilder"></param>
		/// <param name="logPath"></param>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static ILoggingBuilder AddFile(this ILoggingBuilder loggingBuilder, string logPath, string filename)
		{
			logPath = logPath ?? UtilityService.GetAppSetting("Path:Logs");
			if (!string.IsNullOrWhiteSpace(logPath) && Directory.Exists(logPath))
				loggingBuilder.AddSerilog(CreateLogger(logPath, filename));
			return loggingBuilder;
		}
	}
}