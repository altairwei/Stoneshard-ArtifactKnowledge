function mod_verren_identifyItem()
{
    var _story_adress_array = argument0
    var _answer = argument1

    story_text = "askVerrenIdentify_identifyItem"
    _answer = scr_add_answer_to_dialog("askVerrenIdentify_thankVerren")
    _story_adress_array = [argument2]
    var unknown_items = []
    with (o_inv_slot)
    {
        if (!scr_dsMapFindValue(data, "identified", true))
            array_push(unknown_items, id)
    }
    var item = unknown_items[irandom(array_length(unknown_items) - 1)]
    with (item)
    {
        identified = true
        ds_map_replace(data, "identified", identified)
        sh_diss = 200

        scr_actionsLog("verrenIdentifiedItem", [ds_list_find_value(global.prologue_text, 1), scr_actionsLogGetItemColorName(id)])
        audio_play_sound(snd_quest_update, 3, 0)
    }

    return [_story_adress_array, _answer];
}
