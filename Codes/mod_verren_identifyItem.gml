function mod_verren_identifyItem()
{
    var _story_adress_array = argument0
    var _answer = argument1

    story_text = "askVerrenIdentify_identifyItem"
    _answer = scr_add_answer_to_dialog("askVerrenIdentify_thankVerren")
    _story_adress_array = [1]
    var unknown_items = [argument2]
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
    }
    audio_play_sound(snd_quest_update, 3, 0)

    return [_story_adress_array, _answer];
}
