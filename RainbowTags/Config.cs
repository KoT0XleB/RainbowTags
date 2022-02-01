using Qurre.API.Addons;
using System.Collections.Generic;
using System.ComponentModel;

namespace RainbowTags
{
    public class Config : IConfig
    {
        [Description("Plugin Name")]
        public string Name { get; set; } = "RainbowTags";
        [Description("Enable the plugin?")]
        public bool IsEnable { get; set; } = true;

        [Description("Only admins and donaters can change the color of the tag?")]
        public bool AdminOnlyEnable { get; set; } = true;
        [Description("Color change delay. Default: 0.5")]
        public float TimeInterval { get; set; } = 0.5f;
        [Description("Colors for tags. You can change it any way you want.")]
        public List<string> Color { get; set; } = new List<string>()
        {
            "pink",
            "red",
            "white",
            //"brown",
            "silver",
            "light_green",
            "crimson",
            "cyan",
            "aqua",
            "deep_pink",
            "tomato",
            "yellow",
            "magenta",
            "blue_green",
            "orange",
            "lime",
            //"green",
            "emerald",
            //"carmine",
            //"nickel",
            "mint",
            //"army_green",
            //"pumpkin"
        };
    }
}
