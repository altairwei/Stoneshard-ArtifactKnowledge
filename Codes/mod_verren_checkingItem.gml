function mod_verren_checkingItem()
{
    var _story_adress_array = argument0
    var _answer = argument1

    story_text = "askVerrenIdentify_checkingItem"
    _answer = [scr_player_answer("continue")]
    var _chance = scr_dsMapFindValue(global.characterDataMap, "chance_of_identification", global.init_chance_identify)
    if scr_chance_value(_chance)
    {
        _story_adress_array = [argument2]
        if (_chance <= 30)
        {
            var _new_chance = _chance + 1
            ds_map_replace(global.characterDataMap, "chance_of_identification", _new_chance)
            scr_actionsLog("artifactKnowledgeImprove", [ds_list_find_value(global.prologue_text, 1), _new_chance])
        }
    }
    else
        _story_adress_array = [argument3]

    return [_story_adress_array, _answer];
}