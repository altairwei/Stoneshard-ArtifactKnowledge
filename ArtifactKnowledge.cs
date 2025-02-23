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
        Msl.AddFunction(ModFiles.GetCode("UpdateArtKnowDialogData.gml"), "UpdateArtKnowDialogData");

        UndertaleGameObject ob = Msl.AddObject("artifact_knowledge_initializer", isPersistent: true);
        Msl.AddNewEvent(ob, "UpdateArtKnowDialogData()", EventType.Create, 0);
        // initializer in START room
        UndertaleRoom room = Msl.GetRoom("START");
        room.AddGameObject("Instances", ob);

        /*
        Msl.LoadGML("gml_GlobalScript_scr_npc_lines_verren")
            .MatchFrom("    var _story_adress_array = array_create(12, -4)")
            .InsertBelow(@"    var _answer = array_create(0)
    var _action = [""Continue""]")
            .MatchFrom("            _story_adress_array = [25, 23, 2, 4, 7, 8, 9, 10, 11, 15, 17, 18, 19, 21]")
            .InsertBelow(@"
            mod_push_identify_dialog(_story_adress_array, _answer, 28)")
            .MatchFromUntil("        case 27:", "            break")
            .InsertBelow(@"
        case 28:
            var _ret = mod_verren_showItem(_story_adress_array, _answer, 29, 1)
            _story_adress_array = _ret[0]
            _answer = _ret[1]
            break
        case 29:
            var _ret = mod_verren_checkingItem(_story_adress_array, _answer, 30, 31)
            _story_adress_array = _ret[0]
            _answer = _ret[1]
            break
        case 30:
            var _ret = mod_verren_identifyItem(_story_adress_array, _answer, 1)
            _story_adress_array = _ret[0]
            _answer = _ret[1]
            _action = [""Exit""]
            break
        case 31:
            story_text = ""askVerrenIdentify_failure""
            _answer = scr_add_answer_to_dialog(""askVerrenIdentify_goodbye"")
            _story_adress_array = [1]
            break")
            .MatchFrom("    scr_dialog_choose(owner.questions_map, story_text, _answer, 14, _story_adress_array)")
            .ReplaceBy("    scr_dialog_choose(owner.questions_map, story_text, _answer, 14, _story_adress_array, _action)")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_npc_lines_verren_cart")
            .MatchFromUntil("        case 1:", "                _create_exit = 1")
            .InsertBelow(@"                mod_push_identify_dialog(_story_adress_array, _answer, 11)")
            .MatchFromUntil("        case 10:", "            break")
            .InsertBelow(@"
        case 11:
            var _ret = mod_verren_showItem(_story_adress_array, _answer, 12, 1)
            _story_adress_array = _ret[0]
            _answer = _ret[1]
            break
        case 12:
            var _ret = mod_verren_checkingItem(_story_adress_array, _answer, 13, 14)
            _story_adress_array = _ret[0]
            _answer = _ret[1]
            break
        case 13:
            var _ret = mod_verren_identifyItem(_story_adress_array, _answer, 1)
            _story_adress_array = _ret[0]
            _answer = _ret[1]
            _action = [""Exit""]
            break
        case 14:
            story_text = ""askVerrenIdentify_failure""
            _answer = scr_add_answer_to_dialog(""askVerrenIdentify_goodbye"")
            _story_adress_array = [1]
            break")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_npc_lines_verren_brynn")
            .MatchFromUntil(@"                    _answer = scr_add_answer_to_dialog(""verrenAboutBrynn00_pc"")", @"                    _action = [""Continue""]")
            .InsertBelow("                    mod_push_identify_dialog(_story_adress_array, _answer, 15)")
            .MatchFromUntil("        case 14:", "            break")
            .InsertBelow(@"
        case 15:
            var _ret = mod_verren_showItem(_story_adress_array, _answer, 16, 1)
            _story_adress_array = _ret[0]
            _answer = _ret[1]
            break
        case 16:
            var _ret = mod_verren_checkingItem(_story_adress_array, _answer, 17, 18)
            _story_adress_array = _ret[0]
            _answer = _ret[1]
            break
        case 17:
            var _ret = mod_verren_identifyItem(_story_adress_array, _answer, 1)
            _story_adress_array = _ret[0]
            _answer = _ret[1]
            _action = [""Exit""]
            break
        case 18:
            story_text = ""askVerrenIdentify_failure""
            _answer = scr_add_answer_to_dialog(""askVerrenIdentify_goodbye"")
            _story_adress_array = [1]
            break")
            .Save();
        */

        DebugPatching();
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
