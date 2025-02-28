function UpdateArtKnowDialogData()
{
    var _Fragments = variable_struct_get(global.DialogueData_val.verren_intro, "Fragments")
    array_insert(_Fragments.hub, 0, "askVerrenIdentify_pc")
    _Fragments.jump_JMP_tohub = "greeting"
    _Fragments.askVerrenIdentify_pc = "askVerrenIdentify_showItem"
    _Fragments.askVerrenIdentify_showItem = ["askVerrenIdentify_showItem_pc", "askVerrenIdentify_showItem_abort"]
    _Fragments.askVerrenIdentify_showItem_abort = "jump_JMP_tohub"
    _Fragments.askVerrenIdentify_showItem_pc = "askVerrenIdentify_checkingItem"
    
    _Fragments.askVerrenIdentify_checkingItem = "Hub_modartknowchecking"
    _Fragments.Hub_modartknowchecking = "condition_CND_modartknowchecking"
    _Fragments.condition_CND_modartknowchecking = ["mod_artknow_checking_positive", "mod_artknow_checking_negative"]
    _Fragments.mod_artknow_checking_positive = "instruction_INS_modartknowidentifyItem"
    _Fragments.instruction_INS_modartknowidentifyItem = "askVerrenIdentify_identifyItem"
    _Fragments.askVerrenIdentify_identifyItem = "askVerrenIdentify_thankVerren_pc"
    _Fragments.askVerrenIdentify_thankVerren_pc = "@dialogue_end"
    _Fragments.mod_artknow_checking_negative = "askVerrenIdentify_failure"
    _Fragments.askVerrenIdentify_failure = "askVerrenIdentify_goodbye_pc"
    _Fragments.askVerrenIdentify_goodbye_pc = "jump_JMP_tohub"

    var _Scripts = variable_struct_get(global.DialogueData_val.verren_intro, "Scripts")
    _Scripts.embedded_askVerrenIdentify_pc = asset_get_index("mod_artknow_has_unidentified")
    _Scripts.condition_CND_modartknowchecking = asset_get_index("mod_artknow_identifiy_success")
    _Scripts.instruction_INS_modartknowidentifyItem = asset_get_index("mod_artknow_identifiy_completion")

    var _Specs = variable_struct_get(global.DialogueData_val.verren_intro, "Specs")
    player_options = ["askVerrenIdentify_pc", "askVerrenIdentify_showItem_pc", "askVerrenIdentify_showItem_abort", "askVerrenIdentify_thankVerren_pc", "askVerrenIdentify_goodbye_pc"]
    for (var i = 0; i < array_length(player_options); i++)
    {
        variable_struct_set(_Specs, player_options[i], {})
        variable_struct_set(variable_struct_get(_Specs, player_options[i]), "technical", true)
    }

    _Specs.instruction_INS_modartknowidentifyItem = {}
    variable_struct_set(_Specs.instruction_INS_modartknowidentifyItem, "action", true)
    _Specs.mod_artknow_checking_positive = {}
    variable_struct_set(_Specs.mod_artknow_checking_positive, "hub", true)
    _Specs.mod_artknow_checking_negative = {}
    variable_struct_set(_Specs.mod_artknow_checking_negative, "hub", true)
    _Specs.Hub_modartknowchecking = {}
    variable_struct_set(_Specs.Hub_modartknowchecking, "hub", true)

    var _Speakers = variable_struct_get(global.DialogueData_val.verren_intro, "Speakers")
    array_push(_Speakers.Verren, "askVerrenIdentify_showItem", "askVerrenIdentify_checkingItem", "askVerrenIdentify_identifyItem", "askVerrenIdentify_failure")
    array_concat(_Speakers.Player, player_options)
}