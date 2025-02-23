function mod_artknow_identifiy_completion()
{
    var unknown_items = []
    with (o_inv_slot)
    {
        if (!ds_map_find_value(data, "identified"))
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
}