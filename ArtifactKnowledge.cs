// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using ModShardLauncher;
using ModShardLauncher.Mods;

namespace ArtifactKnowledge;
public class ArtifactKnowledge : Mod
{
    public override string Author => "Altair";
    public override string Name => "Artifact Knowledge";
    public override string Description => "You can now use Verren's traits by asking for his help, which has a 1% chance of revealing the properties of a random unidentified item in your inventory.";
    public override string Version => "1.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        Msl.AddMenu("Artifact Knowledge", new UIComponent(name: "Initial Chance",
            associatedGlobal: "init_chance_identify", UIComponentType.Slider, (1, 30), 1));

        Msl.AddFunction(ModFiles.GetCode("mod_push_identify_dialog.gml"), "mod_push_identify_dialog");
        Msl.AddFunction(ModFiles.GetCode("mod_verren_checkingItem.gml"), "mod_verren_checkingItem");
        Msl.AddFunction(ModFiles.GetCode("mod_verren_identifyItem.gml"), "mod_verren_identifyItem");
        Msl.AddFunction(ModFiles.GetCode("mod_verren_showItem.gml"), "mod_verren_showItem");

        Msl.LoadGML("gml_Object_o_verren_SimpleNPC_Alarm_1")
            .MatchAll()
            .InsertBelow(ModFiles, "verren_lines.gml")
            .Save();

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

        Localization.DialogLinesPatching();
        Localization.ActionLogsPatching();
    }
}
