function mod_artknow_identifiy_success()
{
    scr_allturn()

    var _chance = ds_map_find_value(global.characterDataMap, "chance_of_identification")
    if (is_undefined(_chance))
        _chance = global.init_chance_identify

    if scr_chance_value(_chance)
    {
        if (_chance < 30)
        {
            var _new_chance = _chance + 1
            ds_map_replace(global.characterDataMap, "chance_of_identification", _new_chance)
            scr_actionsLog("artifactKnowledgeImprove", [ds_list_find_value(global.prologue_text, 1), _new_chance])
        }

        return true
    }
    else
        return false
}