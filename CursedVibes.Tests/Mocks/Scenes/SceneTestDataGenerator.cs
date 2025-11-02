
using CursedVibes.Application.Scenes.Dtos.Scene;

namespace CursedVibes.Tests.Mocks.Scenes
{
    public static class SceneTestDataGenerator
    {
        public static SceneDto CreateSceneDto(string? sceneId = null)
        {
            var dto = new SceneDto
            {
                SceneId = sceneId ?? "scene1",
                SceneType = "Intro",
                Chapter = "Chapter 1",
                SceneAura = new SceneAuraDto
                {
                    Background = "TestBackground",
                    AmbientAudio = "TestAudio",
                    Music = "TestMusic",
                    Sfx = new List<string> { "TestSfx" }
                },
            };

            //add content to the lists with private set
            dto.Narration.Add("It was a quiet morning...");
            dto.Narration.Add("The sun rose slowly over the hills.");

            dto.Choices.Add(new SceneChoicesDto { 
                Text = "Explore the forest", 
                Tone = "Test Tone", Outcome = "Test OutCome", 
                NextScene  = "Test NextScene",
                StatImpact = new StatDeltaDto { CurseLevel = 10 }
            });

            dto.Effects.AddRange(new[] { "Birds chirping", "Sunlight shimmer" });

            return dto;
        }
    }
}
