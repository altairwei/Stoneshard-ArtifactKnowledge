// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace ArtifactKnowledge;
public class ArtifactKnowledge : Mod
{
    public override string Author => "Altair";
    public override string Name => "Artifact Knowledge";
    public override string Description => "You can now use Verren's traits by asking for his help, which has a 1% chance of revealing the properties of a random unidentified item in your inventory.";
    public override string Version => "1.2.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        Msl.AddMenu(
            "Artifact Knowledge",
            new UIComponent(name: "Initial Chance", associatedGlobal: "init_chance_identify", UIComponentType.Slider, (1, 30), 10),
            new UIComponent(name: "Caravan Chance", associatedGlobal: "caravan_chance_identify", UIComponentType.Slider, (1, 100), 30));

        Msl.AddFunction(ModFiles.GetCode("mod_artknow_has_unidentified.gml"), "mod_artknow_has_unidentified");
        Msl.AddFunction(ModFiles.GetCode("mod_artknow_identifiy_success.gml"), "mod_artknow_identifiy_success");
        Msl.AddFunction(ModFiles.GetCode("mod_artknow_identifiy_completion.gml"), "mod_artknow_identifiy_completion");
        Msl.AddFunction(ModFiles.GetCode("mod_artknow_get_stash_unidentified.gml"), "mod_artknow_get_stash_unidentified");
        Msl.AddFunction(ModFiles.GetCode("mod_artnow_caravan_check_succeed.gml"), "mod_artnow_caravan_check_succeed");
        Msl.AddFunction(ModFiles.GetCode("mod_artknow_caravan_give_items.gml"), "mod_artknow_caravan_give_items");

        UndertaleGameObject ob = Msl.AddObject("artifact_knowledge_initializer", isPersistent: true);
        Msl.AddNewEvent(ob, "", EventType.Create, 0);
        UndertaleRoom room = Msl.GetRoom("START");
        room.AddGameObject("Instances", ob);
        Msl.LoadGML(Msl.EventName("artifact_knowledge_initializer", EventType.Create, 0))
            .MatchAll()
            .InsertBelow(ModFiles, "UpdateArtKnowDialogData.gml")
            .Save();

        Localization.DialogLinesPatching();
        Localization.ActionLogsPatching();
    }
}
