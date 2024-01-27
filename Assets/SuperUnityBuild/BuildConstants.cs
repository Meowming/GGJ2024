using System;

// This file is auto-generated. Do not modify or move this file.

namespace SuperUnityBuild.Generated
{
    public enum ReleaseType
    {
        None,
        Debug,
        Release,
    }

    public enum Platform
    {
        None,
        PC,
        macOS,
    }

    public enum ScriptingBackend
    {
        None,
        Mono,
    }

    public enum Architecture
    {
        None,
        Windows_x86,
        macOS,
    }

    public enum Distribution
    {
        None,
    }

    public static class BuildConstants
    {
        public static readonly DateTime buildDate = new DateTime(638419884681501460);
        public const string version = "1.0.0.1";
        public const ReleaseType releaseType = ReleaseType.Debug;
        public const Platform platform = Platform.PC;
        public const ScriptingBackend scriptingBackend = ScriptingBackend.Mono;
        public const Architecture architecture = Architecture.Windows_x86;
        public const Distribution distribution = Distribution.None;
    }
}

