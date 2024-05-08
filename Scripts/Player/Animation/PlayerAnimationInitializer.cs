using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using System.IO;

public class PlayerAnimationInitializer
{
    public static void LoadAnimations(PlayerAnimationController ac)
    {
        string json = File.ReadAllText("./Json/zero_animation.json");

        Dictionary<EPlayerState, Dictionary<EPlayerWeapon, PlayerAnimationPair>> stateAnimationsCollection = JsonConvert.DeserializeObject<Dictionary<EPlayerState, Dictionary<EPlayerWeapon, PlayerAnimationPair>>>(json);

        ac.StateAnimations = stateAnimationsCollection;

        Dictionary<string, PlayerAnimation> animationsMap = new Dictionary<string, PlayerAnimation>();
        foreach (var stateAnimations in stateAnimationsCollection.Values)
        {
            foreach (var pairAnimation in stateAnimations?.Values)
            {
                if (pairAnimation?.normal != null)
                {
                    foreach (var animation in pairAnimation?.normal)
                    {

                        if (!animationsMap.ContainsKey(animation.name))
                        {
                            animationsMap.Add(animation.name, animation);
                        }

                    }
                }


                if (pairAnimation?.special != null)
                {
                    foreach (var animation in pairAnimation?.special)
                    {
                        if (!animationsMap.ContainsKey(animation.name))
                        {
                            animationsMap.Add(animation.name, animation);
                        }
                    }
                }

            }
        }
        ac.AllAnimations = animationsMap;

        // string[] lines = File.ReadAllLines("./Json/test.txt");
        // for (int i = 0; i < lines.Length; i+=4)
        // {
        //     // string text = lines[i] + "\n" + lines[i+1] + "\n" + lines[i+2];


        //     string data = lines[i+2].Trim();

        //     int start = data.IndexOf("(");
        //     string info = data.Substring(start).Replace("(", "").Replace(")", "");

        //     string[] numbers = info.Split(",");
        //     int[] numFormat = new int[4];

        //     numFormat[0] = numbers[0].ToInt() + numbers[0].ToInt()*64/64;
        //     numFormat[1] = numbers[1].ToInt() + numbers[1].ToInt()*64/64;
        //     numFormat[2] = numbers[2].ToInt() + 64;
        //     numFormat[3] = numbers[3].ToInt() + 64;

        //     // GD.Print(numFormat.Join(","));

        //     GD.Print(lines[i].Trim());
        //     GD.Print(lines[i+1].Trim());
        //     // GD.Print(lines[i+2].Trim());
        //     GD.Print("region = Rect2("+numFormat.Join(",")+")");
        //     GD.Print("\n");
        // }
    }
}