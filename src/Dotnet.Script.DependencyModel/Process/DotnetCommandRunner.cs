using Dotnet.Script.DependencyModel.Environment;
using Dotnet.Script.DependencyModel.Logging;
using System;

namespace Dotnet.Script.DependencyModel.Process
{
    public class DotnetCommandRunner
    {
        private const string DotnetFileName = "";

        private readonly ScriptEnvironment _scriptEnvironment;

        private readonly CommandRunner _commandRunner;
        
        private readonly Lazy<string> _commandPath;

        public DotnetCommandRunner(LogFactory logFactory)
        {
            _scriptEnvironment = ScriptEnvironment.Default;

            _commandRunner = new CommandRunner(logFactory: logFactory);

            _commandPath = new Lazy<string>(() =>
            {
                var dotnetInstallLocation = _scriptEnvironment.GetDotnetInstallLocation(withoutDefaultInstallLocationFallback: true);
                return !string.IsNullOrWhiteSpace(dotnetInstallLocation) ? $@"{dotnetInstallLocation}/dotnet" : "dotnet";
            });
        }

        public int Execute(string arguments)
        {
            return _commandRunner.Execute(commandPath: _commandPath.Value, arguments: arguments);
        }
    }
}
