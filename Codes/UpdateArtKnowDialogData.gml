var _npc_dialogs = ["verren_intro", "verren_caravan"]

for (var j = 0; j < array_length(_npc_dialogs); j++)
{
    var _npc = _npc_dialogs[j]
    var _data = variable_struct_get(global.DialogueData_val, _npc)

    var _Fragments = variable_struct_get(_data, "Fragments")

    if (_npc == "verren_intro")
    {
        array_insert(_Fragments.hub, 0, "askVerrenIdentify_pc")
        _Fragments.jump_JMP_tohub = "greeting"
    }
    else if (_npc == "verren_caravan")
    {
        array_insert(_Fragments.verren_hub, 0, "askVerrenIdentify_pc")
        _Fragments.jump_JMP_tohub = "return"
    }

    _Fragments.askVerrenIdentify_pc = "askVerrenIdentify_showItem"
    _Fragments.askVerrenIdentify_showItem = ["askVerrenIdentify_showItem_pc", "askVerrenIdentify_showItem_abort"]
    _Fragments.askVerrenIdentify_showItem_abort = "jump_JMP_tohub"
    _Fragments.askVerrenIdentify_showItem_pc = "askVerrenIdentify_checkingItem"

    _Fragments.askVerrenIdentify_checkingItem = "Hub_modartknowchecking"
    _Fragments.Hub_modartknowchecking = "condition_CND_modartknowchecking"
    _Fragments.condition_CND_modartknowchecking = ["mod_artknow_checking_positive", "mod_artknow_checking_negative"]
    _Fragments.mod_artknow_checking_positive = "instruction_INS_modartknowidentifyItem"
    _Fragments.instruction_INS_modartknowidentifyItem = "askVerrenIdentify_identifyItem"
    _Fragments.askVerrenIdentify_identifyItem = ["askVerrenIdentify_thankVerren_pc", "leave"]
    _Fragments.askVerrenIdentify_thankVerren_pc = "@dialogue_end"
    _Fragments.mod_artknow_checking_negative = "askVerrenIdentify_failure"
    _Fragments.askVerrenIdentify_failure = ["askVerrenIdentify_goodbye_pc", "leave"]
    _Fragments.askVerrenIdentify_goodbye_pc = "jump_JMP_tohub"

    var _Scripts = variable_struct_get(_data, "Scripts")
    _Scripts.embedded_askVerrenIdentify_pc = asset_get_index("mod_artknow_has_unidentified")
    _Scripts.condition_CND_modartknowchecking = asset_get_index("mod_artknow_identifiy_success")
    _Scripts.instruction_INS_modartknowidentifyItem = asset_get_index("mod_artknow_identifiy_completion")

    var _Specs = variable_struct_get(_data, "Specs")
    player_options = ["askVerrenIdentify_pc", "askVerrenIdentify_showItem_pc", "askVerrenIdentify_showItem_abort", "askVerrenIdentify_thankVerren_pc", "askVerrenIdentify_goodbye_pc"]
    for (var i = 0; i < array_length(player_options); i++)
        variable_struct_set(_Specs, player_options[i], { technical: true })

    _Specs.instruction_INS_modartknowidentifyItem = { action: true }
    _Specs.mod_artknow_checking_positive = { hub: true }
    _Specs.mod_artknow_checking_negative = { hub: true }
    _Specs.Hub_modartknowchecking = { hub: true }
    _Specs.askVerrenIdentify_pc = { priority: ["-200"] }

    var _Speakers = variable_struct_get(_data, "Speakers")
    array_push(_Speakers.Verren, "askVerrenIdentify_showItem", "askVerrenIdentify_checkingItem", "askVerrenIdentify_identifyItem", "askVerrenIdentify_failure")
    array_concat(_Speakers.Player, player_options)
}

// 参考格纹涅尔过剧情时，维伦是如何主动寻找主角的。参考这个机制。
var _Fragments = variable_struct_get(global.DialogueData_val.verren_caravan, "Fragments")
var _Scripts = variable_struct_get(global.DialogueData_val.verren_caravan, "Scripts")
var _Specs = variable_struct_get(global.DialogueData_val.verren_caravan, "Specs")

_Fragments.verren_caravan = "condition_CND_caravanIdentify"
_Fragments.condition_CND_caravanIdentify = ["mod_artknow_caravanIdentify_positive", "mod_artknow_caravanIdentify_negative"]
_Fragments.mod_artknow_caravanIdentify_negative = "condition_CND_91E692F7"
_Fragments.mod_artknow_caravanIdentify_positive = "caravanVerrenIdentify"
_Fragments.caravanVerrenIdentify = ["caravanVerrenIdentify_thankVerren_pc", "leave"]
_Fragments.caravanVerrenIdentify_thankVerren_pc = "instruction_INS_giveIdentified"
_Fragments.instruction_INS_giveIdentified = "@dialogue_end"

_Scripts.condition_CND_caravanIdentify = asset_get_index("mod_artnow_caravan_check_succeed")
_Scripts.instruction_INS_giveIdentified = asset_get_index("mod_artknow_caravan_give_items")

_Specs.mod_artknow_caravanIdentify_positive = { hub: true }
_Specs.mod_artknow_caravanIdentify_negative = { hub: true }
_Specs.instruction_INS_giveIdentified = { action: true }

