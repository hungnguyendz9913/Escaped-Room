using EscapeRoom.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EscapeRoom.Data
{
    public static class PuzzleRepo
    {
        private static Task? _loadTask;
        private static readonly SemaphoreSlim _gate = new(1, 1);
        public static IReadOnlyList<Puzzle> Puzzles { get; private set; } = Array.Empty<Puzzle>();

        public static Task EnsureLoadedAsync() => _loadTask ??= LoadCoreAsync();

        private static async Task LoadCoreAsync()
        {
            await _gate.WaitAsync();
            try
            {
                if (Puzzles.Count > 0) return;

                var file = await StorageFile.GetFileFromApplicationUriAsync(
                    new Uri("ms-appx:///Assets/PuzzleRepo.yaml"));
                var text = await FileIO.ReadTextAsync(file);

                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build();

                var list = deserializer.Deserialize<List<Puzzle>>(text) ?? new();
                Puzzles = list.AsReadOnly();
            }
            finally
            {
                _gate.Release();
            }
        }
    }
}
