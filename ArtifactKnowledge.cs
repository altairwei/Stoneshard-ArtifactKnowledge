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
    public override string Version => "1.1.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        Msl.AddMenu("Artifact Knowledge", new UIComponent(name: "Initial Chance",
            associatedGlobal: "init_chance_identify", UIComponentType.Slider, (1, 30), 10));

        Msl.AddFunction(ModFiles.GetCode("mod_artknow_has_unidentified.gml"), "mod_artknow_has_unidentified");
        Msl.AddFunction(ModFiles.GetCode("mod_artknow_identifiy_success.gml"), "mod_artknow_identifiy_success");
        Msl.AddFunction(ModFiles.GetCode("mod_artknow_identifiy_completion.gml"), "mod_artknow_identifiy_completion");
        Msl.AddFunction(ModFiles.GetCode("mod_artknow_get_stash_unidentified.gml"), "mod_artknow_get_stash_unidentified");
        Msl.AddFunction(ModFiles.GetCode("mod_artnow_caravan_check_succeed.gml"), "mod_artnow_caravan_check_succeed");
        Msl.AddFunction(ModFiles.GetCode("mod_artknow_caravan_give_items.gml"), "mod_artknow_caravan_give_items");

        UndertaleGameObject ob = Msl.AddObject("artifact_knowledge_initializer", isPersistent: true);
        Msl.AddNewEvent(ob, ModFiles.GetCode("UpdateArtKnowDialogData.gml"), EventType.Create, 0);
        // initializer in START room
        UndertaleRoom room = Msl.GetRoom("START");
        room.AddGameObject("Instances", ob);

        // DebugPatching();
        Localization.DialogLinesPatching();
        Localization.ActionLogsPatching();
    }

    private void DebugPatching()
    {
        Msl.LoadGML("gml_GlobalScript_debug_print")
            .MatchFromUntil("function debug_print()", "{")
            .InsertBelow(@"
    var _str = ""debug_print: "";
    for (var i = 0; i < argument_count; i ++)
    {
        _str += "" "" + string(argument[i]);
    }
    scr_msl_log(_str)")
            .Save();

        Msl.LoadAssemblyAsString("gml_GlobalScript_neoconsole_api")
            .MatchFromUntil("> gml_Script_print (locals=3, argc=0)", ":[145]")
            .InsertBelow(@"push.s ""print: ""@121330
pop.v.s local._str
pushi.e 0
pop.v.i local.i

:[1002]
pushloc.v local.i
pushbltn.v builtin.argument_count
cmp.v.v LT
bf [1004]

:[1003]
push.v local._str
push.s "" ""@2874
pushi.e -15
pushloc.v local.i
conv.v.i
push.v [array]self.argument
call.i string(argc=1)
add.v.s
add.v.v
pop.v.v local._str
push.v local.i
push.e 1
add.i.v
pop.v.v local.i
b [1002]

:[1004]
pushloc.v local._str
call.i gml_Script_scr_msl_log(argc=1)
popz.v")
            .Save();

        // Msl.LoadGML("gml_GlobalScript_DialogueFlow")
        //     .MatchFrom("_next = dialogue_fragment_flow_next(_text, _execflag_normal);")
        //     .InsertBelow(@"scr_actionsLogUpdate(_text + ""---->"" + _next[0])")
        //     .Save();

        // Delete Me!
        Msl.LoadGML("gml_Object_o_player_KeyPress_115") // F4
            .MatchAll()
            .InsertBelow(ModFiles, "generate_cursed_item.gml")
            .Save();
    }
}
