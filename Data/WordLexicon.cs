using System.Collections.Generic;
using System.Linq;
using MidtermAppDev.Models;

namespace MidtermAppDev.Data
{
    public static class WordLexicon
    {
        // 1. Syntax Dictionaries
        public static readonly List<string> EasySyntax = new()
        {
            "var", "int", "bool", "byte", "void", "char", "task", "enum", "lock",
            "true", "null", "base", "this", "goto", "case", "for", "try", "new",
            "out", "ref", "in", "is", "as", "if", "else", "do"
        };

        public static readonly List<string> MediumSyntax = new()
        {
            "async", "await", "yield", "query", "object", "struct", "sealed", "lambda",
            "params", "string", "record", "static", "public", "switch", "return", "throw",
            "catch", "class", "event", "const", "typeof", "sizeof", "checked", "unsafe"
        };

        public static readonly List<string> HardSyntax = new()
        {
            "interface", "override", "virtual", "boolean", "internal", "abstract",
            "delegate", "protected", "implicit", "explicit", "readonly", "volatile",
            "continue", "namespace", "middleware", "reflection", "polymorph",
            "dependency", "immutable", "enumerable", "collection", "predicate",
            "stackalloc", "singleton", "transient"
        };

        // 2. Regular English Dictionaries
        public static readonly List<string> EasyEnglish = new()
        {
            "star", "moon", "wolf", "wind", "fire", "rain", "glow", "wave", "iron",
            "gold", "sky", "sun", "bird", "leaf", "rock", "time", "blue", "dark",
            "fast", "jump", "hero", "mind", "echo", "neon", "grid", "ship", "bolt",
            "core", "beam", "path"
        };

        public static readonly List<string> MediumEnglish = new()
        {
            "shadow", "thunder", "galaxy", "whisper", "horizon", "crystal", "eclipse",
            "voyage", "beacon", "falcon", "knight", "matrix", "plasma", "rocket",
            "silver", "dragon", "meteor", "orbital", "shield", "vortex", "gravity",
            "quantum", "circuit", "pulsar", "aurora", "cyber"
        };

        public static readonly List<string> HardEnglish = new()
        {
            "constellation", "luminescence", "phenomenon", "equilibrium", "stratosphere",
            "labyrinth", "cataclysm", "resilience", "metamorphosis", "synchronous",
            "sovereignty", "supercluster", "electromagnet", "astrophysics",
            "hypervelocity", "interstellar", "annihilation", "trajectory"
        };

        public static List<string> GetWords(LexiconMode lexicon, GameDifficulty difficulty)
        {
            return lexicon switch
            {
                LexiconMode.Syntax => difficulty switch
                {
                    GameDifficulty.Easy => EasySyntax.Concat(MediumSyntax.Take(4)).ToList(),
                    GameDifficulty.Hard => HardSyntax.Concat(MediumSyntax.Skip(8)).ToList(),
                    _ => MediumSyntax.Concat(EasySyntax.Take(5)).Concat(HardSyntax.Take(5)).ToList()
                },
                LexiconMode.English => difficulty switch
                {
                    GameDifficulty.Easy => EasyEnglish.Concat(MediumEnglish.Take(4)).ToList(),
                    GameDifficulty.Hard => HardEnglish.Concat(MediumEnglish.Skip(8)).ToList(),
                    _ => MediumEnglish.Concat(EasyEnglish.Take(5)).Concat(HardEnglish.Take(5)).ToList()
                },
                LexiconMode.Mixed => difficulty switch
                {
                    GameDifficulty.Easy => EasySyntax.Concat(EasyEnglish).ToList(),
                    GameDifficulty.Hard => HardSyntax.Concat(HardEnglish).ToList(),
                    _ => MediumSyntax.Concat(MediumEnglish).ToList()
                },
                _ => MediumSyntax.Concat(MediumEnglish).ToList()
            };
        }
    }
}
